using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Queries.DTOs;
using EPharmacy.Application.Users.Common.Commands.CreateUser;

namespace EPharmacy.RESTApi.Controllers.v1.System;

public class UsersController : BaseApiController
{
    [HttpPost]
    [Route(Routes.System.Users.Create)]
    public async Task<ActionResult<ServiceResult<ApplicationUserDTO>>> CreateUser([FromBody] CreateUserDTO credentials)
    {
        return await Mediator.Send(new CreateUserCommand(credentials));
    }
}