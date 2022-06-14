using EPharmacy.Application.WorkOrders.Ponea.Commands.CreateWorkOrder;
using EPharmacy.Application.WorkOrders.Ponea.Commands.DTOs;

namespace EPharmacy.RESTApi.Controllers.v1.Ponea;

public class WorkOrderController : BaseApiController
{
    [HttpPost]
    [Route(Routes.Ponea.WorkOrder.Create)]
    public async Task<ActionResult<ServiceResult>> CreateWorkOrder(CreateWorkOrderDTO newWorkOrder)
    {
        return await Mediator.Send(new CreateWorkOrderCommand(newWorkOrder));
    }
}