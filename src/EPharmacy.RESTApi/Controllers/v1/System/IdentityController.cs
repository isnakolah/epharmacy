using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Commands.Login;
using EPharmacy.Application.Identity.Common.Commands.Logout;
using Microsoft.AspNetCore.Authorization;

namespace EPharmacy.RESTApi.Controllers.v1.System;

public class IdentityController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost]
    [Route(Routes.System.Identity.Login)]
    public async Task<ActionResult<ServiceResult<LoginResponseDTO>>> LoginUser([FromBody] LoginRequestDTO credentials)
    {
        return await Mediator.Send(new LoginCommand(credentials));
    }

    [HttpPost]
    [Route(Routes.System.Identity.Logout)]
    public async Task<ActionResult<ServiceResult>> LogoutUser()
    {
        return await Mediator.Send(new LogoutCommand());
    }
}