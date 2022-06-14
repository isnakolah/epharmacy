using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Quotations.Ponea.Commands.ApproveQuotation;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record ApproveQuotationCommand(Guid QuotationID) : IRequestWrapper;

public class ApproveQuotationCommandHandler : IRequestHandlerWrapper<ApproveQuotationCommand>
{
    private readonly IApplicationDbContext _context;

    public ApproveQuotationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult> Handle(ApproveQuotationCommand request, CancellationToken cancellationToken)
    {
        var quotation = await _context.Quotations
            .Include(quote => quote.PharmacyPrescription)
                .ThenInclude(pharmPresc => pharmPresc.Prescription)
            .Where(quote => quote.ID == request.QuotationID)
            .FirstOrErrorAsync(request.QuotationID, cancellationToken);

        var prescriptionID = quotation.PharmacyPrescription.Prescription.ID;

        var prescription = await _context.Prescriptions
            .Include(presc => presc.PharmacyPrescriptions)
                .ThenInclude(pharmPresc => pharmPresc.Quotation)
            .Where(presc => presc.ID == prescriptionID)
            .FirstOrErrorAsync(prescriptionID, cancellationToken);

        if (quotation.IsApproved)
            throw new QuotationAlreadyApprovedException();

        if (prescription.IsApproved)
            throw new PrescriptionAlreadyApprovedException();

        if (quotation.IsCancelled)
            throw new QuotationIsCancelledException();

        if (prescription.IsPending || prescription.IsCancelled)
            prescription.Approve();

        foreach (var pharmPrescQuote in prescription.PharmacyPrescriptions.Select(x => x.Quotation))
        {
            if (pharmPrescQuote is null)
                continue;
            if (pharmPrescQuote.ID.Equals(quotation.ID))
                pharmPrescQuote.Approve();
            else
                pharmPrescQuote.Reject();
        }

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }
}