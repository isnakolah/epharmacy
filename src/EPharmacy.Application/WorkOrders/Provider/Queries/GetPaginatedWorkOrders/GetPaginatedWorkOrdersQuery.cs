using EPharmacy.Application.WorkOrders.Provider.Queries.DTOs;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.WorkOrders.Provider.Queries.GetPaginatedWorkOrders;

[Authorize(Roles = PHARMACY_AGENT_PERMISSIONS)]
public record GetPaginatedWorkOrdersQuery(PaginationFilter PaginationFilter) : IRequestPaginatedWrapper<GetWorkOrderDTO>;

public class GetPaginatedWorkOrderQueryHandler : IRequestHandlerPaginatedWrapper<GetPaginatedWorkOrdersQuery, GetWorkOrderDTO>
{
    private readonly ICurrentUserPharmacy _currentUserPharmacy;
    private readonly IApplicationDbContext _context;
    private readonly IPaginate _paginate;

    public GetPaginatedWorkOrderQueryHandler(IApplicationDbContext context, IPaginate paginate, ICurrentUserPharmacy currentUserPharmacy)
    {
        (_context, _paginate, _currentUserPharmacy) = (context, paginate, currentUserPharmacy);
    }

    public async Task<PaginatedServiceResult<GetWorkOrderDTO>> Handle(GetPaginatedWorkOrdersQuery request, CancellationToken cancellationToken)
    {
        var pharmacyID = await _currentUserPharmacy.GetIDAsync();

        var workOrderQueryable = _context.WorkOrders
            .OrderByDescending(workOrder => workOrder.CreatedOn)
            .Where(workOrder => workOrder.Quotation.PharmacyPrescription.Pharmacy.ID == pharmacyID);

        var response = await _paginate.CreateAsync<WorkOrder, GetWorkOrderDTO>(workOrderQueryable, request.PaginationFilter, cancellationToken);

        return response;
    }
}