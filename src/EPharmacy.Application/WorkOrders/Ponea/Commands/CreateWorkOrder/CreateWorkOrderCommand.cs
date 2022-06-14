using AutoMapper;
using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.WorkOrders.Ponea.Commands.DTOs;
using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.WorkOrders.Ponea.Commands.CreateWorkOrder;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record CreateWorkOrderCommand(CreateWorkOrderDTO WorkOrder) : IRequestWrapper;

public class CreateWorkOrderCommandHandler : IRequestHandlerWrapper<CreateWorkOrderCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateWorkOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        (_context, _mapper) = (context, mapper);
    }

    public async Task<ServiceResult> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var quotation = await _context.Quotations.FindOrErrorAsync(request.WorkOrder.QuotationID);

        await CheckIfWorkOrderExists(request, quotation);

        if (!quotation.IsApproved)
            throw new QuotationNeedsApprovalException();

        var workOrder = _mapper.Map<WorkOrder>(request.WorkOrder);

        _context.WorkOrders.Add(workOrder);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }

    // If workorder with similar appointment id exists, don't create a new one
    private async Task CheckIfWorkOrderExists(CreateWorkOrderCommand request, Quotation quotation)
    {
        var workOrderExists = await _context.WorkOrders
            .Where(workOrder => workOrder.ConciergeAppointmentID == request.WorkOrder.ConciergeAppointmentID || workOrder.Quotation == quotation)
            .AnyAsync();

        if (workOrderExists)
            throw new WorkOrderExistsException();
    }
}