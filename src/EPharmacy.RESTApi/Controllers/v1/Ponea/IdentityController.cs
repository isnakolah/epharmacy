using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Commands.Login;
using EPharmacy.Application.Identity.Common.Commands.Logout;
using Microsoft.AspNetCore.Authorization;

namespace EPharmacy.RESTApi.Controllers.v1.Ponea;

public class IdentityController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost]
    [Route(Routes.Ponea.Identity.Login)]
    public async Task<ActionResult<ServiceResult<LoginResponseDTO>>> LoginConciergeAgent([FromBody] LoginRequestDTO credentials)
    {
        return await Mediator.Send(new LoginCommand(credentials));
    }

    [HttpPost]
    [Route(Routes.Ponea.Identity.Logout)]
    public async Task<ActionResult<ServiceResult>> LogoutUser()
    {
        return await Mediator.Send(new LogoutCommand());
    }
}