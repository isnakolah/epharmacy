using AutoMapper;
using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Quotations.Provider.Commands.DTOs;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Provider.Commands.CreateQuotation;

[Authorize(Roles = PHARMACY_AGENT_PERMISSIONS)]
public record CreateQuotationCommand(CreateQuotationDTO Quotation) : IRequestWrapper;

public class CreateQuotationQueryHandler : IRequestHandlerWrapper<CreateQuotationCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMarkupService _markupService;
    private readonly IDateTime _datetime;
    private readonly IMapper _mapper;

    public CreateQuotationQueryHandler(IApplicationDbContext context, IMarkupService markupService, IDateTime datetime, IMapper mapper)
    {
        (_mapper, _context, _datetime, _markupService) = (mapper, context, datetime, markupService);
    }

    public async Task<ServiceResult> Handle(CreateQuotationCommand request, CancellationToken cancellationToken)
    {
        var quotation = request.Quotation.MapToEntity(_mapper);

        if (quotation.PharmacyPrescription.IsQuoted)
            throw new PrescriptionAlreadyQuotedException();

        if (PrescriptionExpired(quotation.PharmacyPrescription))
            throw new PrescriptionAlreadyQuotedException();

        if (quotation.PharmacyPrescription.Prescription.IsQuoting)
            quotation.PharmacyPrescription.Prescription.Pend();

        CalculateTotalAndMarkup(quotation);

        _context.Quotations.Add(quotation);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }

    private void CalculateTotalAndMarkup(Quotation quotation)
    {
        quotation.Total += quotation.DeliveryFee;

        quotation.PharmaceuticalQuotationItems.ToList()
            .ForEach(pharmQuoteItem =>
            {
                pharmQuoteItem.GenericDrug = pharmQuoteItem.Stocked ? null : pharmQuoteItem.GenericDrug;
                pharmQuoteItem.Stocked = !string.IsNullOrWhiteSpace(pharmQuoteItem.GenericDrug);

                // TODO: Validation exception
                if (!pharmQuoteItem.Stocked && pharmQuoteItem.GenericDrug is null)
                    return;

                var total = pharmQuoteItem.UnitPrice * pharmQuoteItem.Quantity;

                pharmQuoteItem.Markup = _markupService.CalculatePharmaceuticalQuotationItemMarkup(total);
                total += pharmQuoteItem.Markup;

                quotation.Total += total;
                quotation.Markup += pharmQuoteItem.Markup;
            });

        quotation.NonPharmaceuticalQuotationItems.ToList()
            .ForEach(nonPharmQuoteItem =>
            {
                var total = nonPharmQuoteItem.Quantity * nonPharmQuoteItem.UnitPrice;

                nonPharmQuoteItem.Markup = _markupService.CalculateNonPharmaceuticalQuotationItemMarkup(total);
                total += nonPharmQuoteItem.Markup;

                quotation.Total += total;
                quotation.Markup += nonPharmQuoteItem.Markup;
            });
    }

    private bool PrescriptionExpired(PharmacyPrescription pharmPresc)
    {
        return pharmPresc.Expiry < _datetime.Now;
    }
}