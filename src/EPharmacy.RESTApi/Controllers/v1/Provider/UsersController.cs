using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Queries.DTOs;
using EPharmacy.Application.Identity.Provider.Queries.GetAllPharmacyUsersWithPagination;
using EPharmacy.Application.Identity.Provider.Queries.GetSinglePharmacyUser;
using EPharmacy.Application.Users.Common.Commands.UpdateUser;
using EPharmacy.Application.Users.Provider.Commands.CreateAgent;
using EPharmacy.Application.Users.Provider.Commands.DeleteUser;
using EPharmacy.Application.Users.Provider.Commands.MakeAgentAdmin;

namespace EPharmacy.RESTApi.Controllers.v1.Provider;
public class UsersController : BaseApiController
{
    [HttpPost]
    [Route(Routes.Provider.Users.Create)]
    public async Task<ActionResult<ServiceResult<ApplicationUserDTO>>> CreatePharmacyUser([FromBody] CreateUserDTO newUser)
    {
        return await Mediator.Send(new CreateAgentCommand(newUser));
    }

    [HttpPut]
    [Route(Routes.Provider.Users.Update)]
    public async Task<ActionResult<ServiceResult>> UpdatePharmacyUser([FromRoute] Guid userID, [FromBody] CreateUserDTO updatedUser)
    {
        return await Mediator.Send(new UpdateUserCommand(updatedUser, userID));
    }

    [HttpGet]
    [Route(Routes.Provider.Users.GetAll)]
    public async Task<ActionResult<ServiceResult<ApplicationUserDTO[]>>> GetAllPharmacyUsersWithPagination()
    {
        return await Mediator.Send(new GetAllPharmacyUsersWithPaginationQuery());
    }

    [HttpGet]
    [Route(Routes.Provider.Users.GetSingleByID)]
    public async Task<ActionResult<ServiceResult<ApplicationUserDTO>>> GetSingleUser(Guid id)
    {
        return await Mediator.Send(new GetSinglePharmacyUserQuery(id));
    }

    [HttpPost]
    [Route(Routes.Provider.Users.MakeAdmin)]
    public async Task<ActionResult<ServiceResult>> MakeAPharmacyAgentAdmin([FromRoute] Guid id, [FromQuery(Name = "admin")] bool admin)
    {
        return await Mediator.Send(new MakeAgentAdminCommand(id, admin));
    }

    [HttpDelete]
    [Route(Routes.Provider.Users.Delete)]
    public async Task<ActionResult<ServiceResult>> DeleteUser([FromRoute] Guid id)
    {
        return await Mediator.Send(new DeleteUserCommand(id));
    }
}