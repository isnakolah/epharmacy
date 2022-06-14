using EPharmacy.Application.WorkOrders.Provider.Commands;
using EPharmacy.Application.WorkOrders.Provider.Queries.DTOs;
using EPharmacy.Application.WorkOrders.Provider.Queries.GetPaginatedWorkOrders;

namespace EPharmacy.RESTApi.Controllers.v1.Provider;

public class WorkOrderController : BaseApiController
{
    [HttpGet]
    [Route(Routes.Provider.WorkOrder.GetAll)]
    public async Task<ActionResult<PaginatedServiceResult<GetWorkOrderDTO>>> GetPaginatedWorkOrders([FromQuery] PaginationFilter paginationFilter)
    {
        return await Mediator.Send(new GetPaginatedWorkOrdersQuery(paginationFilter));
    }

    [HttpPost]
    [Route(Routes.Provider.WorkOrder.Dispatch)]
    public async Task<ActionResult<ServiceResult>> DispatchWorkOrder(Guid id)
    {
        return await Mediator.Send(new DispatchWorkOrderCommand(id));
    }
}