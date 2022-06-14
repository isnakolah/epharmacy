using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Quotations.Ponea.Commands.RejectQuotation;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record RejectQuotationCommand(Guid QuotationID) : IRequestWrapper;

public class RejectQuotationCommandHandler : IRequestHandlerWrapper<RejectQuotationCommand>
{
    private readonly IApplicationDbContext _context;

    public RejectQuotationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult> Handle(RejectQuotationCommand request, CancellationToken cancellationToken)
    {
        var prescriptionID = await _context.Quotations
            .Where(quote => quote.ID == request.QuotationID)
            .Select(quote => quote.PharmacyPrescription.Prescription.ID)
            .FirstOrDefaultAsync(cancellationToken);

        var prescription = await _context.Prescriptions
            .Include(presc => presc.PharmacyPrescriptions)
                .ThenInclude(pharmPresc => pharmPresc.Quotation)
            .Where(presc => presc.ID == prescriptionID)
            .FirstOrDefaultAsync(cancellationToken);

        var quotation = await _context.Quotations.FindOrErrorAsync(request.QuotationID);

        if (quotation.IsApproved)
            throw new QuotationAlreadyApprovedException();

        prescription.PharmacyPrescriptions.First(x => x.Quotation is not null && x.Quotation.ID.Equals(request.QuotationID)).Quotation.Reject();

        if (prescription.AllQuotationsRejected || !prescription.HasPendingQuotations)
            prescription.Cancel();

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }
}