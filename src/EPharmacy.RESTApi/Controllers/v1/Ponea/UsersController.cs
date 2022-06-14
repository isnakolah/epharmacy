using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Queries.DTOs;
using EPharmacy.Application.Identity.Ponea.Queries._DTOs;
using EPharmacy.Application.Identity.Ponea.Queries.GetAllPharmacyAdminsForPonea;
using EPharmacy.Application.Users.Common.Commands.UpdateUser;
using EPharmacy.Application.Users.Ponea.Commands.CreatePharmacyAdmin;
using EPharmacy.Application.Users.Ponea.Commands.DeletePharmacyAdmin;
using EPharmacy.Application.Users.Ponea.Queries.GetSinglePharmacyAdmin;

namespace EPharmacy.RESTApi.Controllers.v1.Ponea;

public class UsersController : BaseApiController
{
    [HttpPost]
    [Route(Routes.Ponea.Users.Create)]
    public async Task<ActionResult<ServiceResult<ApplicationUserDTO>>> CreatePharmacyAdmin([FromBody] CreatePharmacyAdminDTO credentials)
    {
        return await Mediator.Send(new CreatePharmacyAdminCommand(credentials));
    }

    [HttpPut]
    [Route(Routes.Ponea.Users.Update)]
    public async Task<ActionResult<ServiceResult>> UpdatePharmacyAdmin([FromRoute] Guid pharmacyAdminID, [FromBody] CreateUserDTO updatedUser)
    {
        return await Mediator.Send(new UpdateUserCommand(updatedUser, pharmacyAdminID));
    }

    [HttpDelete]
    [Route(Routes.Ponea.Users.Delete)]
    public async Task<ActionResult<ServiceResult>> DeletePharmacyID([FromRoute] Guid pharmacyAdminID)
    {
        return await Mediator.Send(new DeletePharmacyAdminCommand(pharmacyAdminID));
    }
    [HttpGet]
    [Route(Routes.Ponea.Users.GetAll)]
    public async Task<ActionResult<ServiceResult<List<ApplicationUserWithPharmacyDTO>>>> GetAllPharmacyAdminUsersForPonea()
    {
        return await Mediator.Send(new GetAllPharmacyAdminsForPoneaQuery());
    }

    [HttpGet]
    [Route(Routes.Ponea.Users.GetSingle)]
    public async Task<ActionResult<ServiceResult<ApplicationUserWithPharmacyDTO>>> GetSinglePharmacyAdminUserForPonea([FromRoute] Guid pharmacyAdminID)
    {
        return await Mediator.Send(new GetSinglePharmacyAdminQuery(pharmacyAdminID));
    }
}