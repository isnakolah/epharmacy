using EPharmacy.Application.Identity.Common.Queries.GetCurrentUserRoles;

namespace EPharmacy.RESTApi.Controllers.v1.Common;

public class IdentityController : BaseApiController
{

    [HttpGet]
    [Route(Routes.Common.Users.Roles)]
    public async Task<ActionResult<ServiceResult<List<string>>>> GetCurrentUserRoles()
    {
        return await Mediator.Send(new GetCurrentUserRolesQuery());
    }
}