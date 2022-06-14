using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Commands.Logout;
using EPharmacy.Application.Identity.Provider.Commands.UpdatePassword;
using EPharmacy.Application.Identity.Provider.Commands.ConfirmEmail;
using EPharmacy.Application.Identity.Provider.Commands.CreatePassword;
using EPharmacy.Application.Identity.Provider.Commands.ForgotPassword;
using EPharmacy.Application.Identity.Provider.Commands.LoginPharmacyUser;
using EPharmacy.Application.Identity.Provider.DTO;
using EPharmacy.Application.Identity.Provider.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace EPharmacy.RESTApi.Controllers.v1.Provider;

public class IdentityController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost]
    [Route(Routes.Provider.Identity.Login)]
    public async Task<ActionResult<ServiceResult<LoginPharmacyUserResponseDTO>>> LoginPharmacyUser([FromBody] LoginRequestDTO credentials)
    {
        return await Mediator.Send(new LoginPharmacyUserCommand(credentials));
    }

    [HttpPost]
    [Route(Routes.Provider.Identity.Logout)]
    public async Task<ActionResult<ServiceResult>> LogoutUser()
    {
        return await Mediator.Send(new LogoutCommand());
    }

    [HttpPost]
    [Route(Routes.Provider.Identity.UpdatePassword)]
    public async Task<ActionResult<ServiceResult>> ChangePassword([FromBody] UpdatePasswordDTO passwords)
    {
        return await Mediator.Send(new UpdatePasswordCommand(passwords));
    }

    [AllowAnonymous]
    [HttpPost]
    [Route(Routes.Provider.Identity.SendPasswordResetEmail)]
    public async Task<ActionResult<ServiceResult>> SendForgotPasswordEmail([FromBody] ForgotPasswordDTO forgotPassword)
    {
        return await Mediator.Send(new SendPasswordResetEmailCommand(forgotPassword));
    }

    [AllowAnonymous]
    [HttpPost]
    [Route(Routes.Provider.Identity.CreatePassword)]
    public async Task<ActionResult<ServiceResult>> CreatePassword([FromBody] CreatePasswordDTO createPassword)
    {
        return await Mediator.Send(new CreatePasswordCommand(createPassword));
    }

    [HttpPost]
    [Route(Routes.Provider.Identity.ConfirmEmail)]
    public async Task<ActionResult<ServiceResult>> ConfirmEmail([FromQuery(Name = "emailToken")] string token)
    {
        return await Mediator.Send(new ConfirmEmailCommand(token));
    }
}