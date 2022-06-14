using EPharmacy.Application.Dashboards.Provider.Queries.DTOs;
using EPharmacy.Application.Dashboards.Provider.Queries.GetSummary;

namespace EPharmacy.RESTApi.Controllers.v1.Provider;

public class DashboardController : BaseApiController
{
    [HttpGet]
    [Route(Routes.Provider.Dashboard.Summary)]
    public async Task<ActionResult<ServiceResult<GetSummaryDto>>> GetSummary()
    {
        return await Mediator.Send(new GetSummaryQuery());
    }
}