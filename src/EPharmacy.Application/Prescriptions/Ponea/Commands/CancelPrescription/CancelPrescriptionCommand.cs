using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Common.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.CancelPrescription;

/// <summary>
/// Cancels a prescription if it is not already cancelled
/// </summary>
/// <param name="ID">ID of the prescription to be cancelled</param>
/// <returns>Handler returns a ServiceResult instance</returns>
/// <exception cref="CustomException">If prescription is already</exception>
[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record CancelPrescriptionCommand(Guid ID) : IRequest<ServiceResult>;

public class CancelPrescriptionCommandHandler : IRequestHandler<CancelPrescriptionCommand, ServiceResult>
{
    private readonly IApplicationDbContext _context;

    public CancelPrescriptionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult> Handle(CancelPrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await _context.Prescriptions
            .Include(presc => presc.PharmacyPrescriptions)
                .ThenInclude(pharmPresc => pharmPresc.Quotation)
            .Where(presc => presc.ID == request.ID)
            .FirstOrErrorAsync(request.ID, cancellationToken);

        if (prescription.IsCancelled)
            throw new PrescriptionAlreadyCancelledException();

        prescription.Cancel();

        prescription.CancelQuotations();

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }
}