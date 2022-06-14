using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.ExternalServices.Ponea.Commands.SendDispatchMessageToPatient;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.WorkOrders.Provider.Commands;

[Authorize(Roles = PHARMACY_AGENT_PERMISSIONS)]
public record DispatchWorkOrderCommand(Guid ID) : IRequestWrapper;

public class DispatchWorkOrderCommandHandler : IRequestHandlerWrapper<DispatchWorkOrderCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public DispatchWorkOrderCommandHandler(IApplicationDbContext context, IMediator mediator)
    {
        (_context, _mediator) = (context, mediator);
    }

    public async Task<ServiceResult> Handle(DispatchWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await _context.WorkOrders.FindOrErrorAsync(request.ID);

        if (workOrder.IsPending)
            throw new WorkOrderCannotBeDispatchedException();

        workOrder.Dispatch();

        await _context.SaveChangesAsync(cancellationToken);

        var patientID = await _context.WorkOrders
            .Where(workOrder => workOrder.ID == workOrder.ID)
            .Select(workOrder => workOrder.Quotation.PharmacyPrescription.Prescription.Patient.ID)
            .FirstOrDefaultAsync(cancellationToken);

        await _mediator.Send(new SendDispatchMessageToPatientCommand(patientID), cancellationToken);

        return ServiceResult.Success();
    }
}