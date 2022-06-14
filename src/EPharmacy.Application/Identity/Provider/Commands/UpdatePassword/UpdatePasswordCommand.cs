using EPharmacy.Application.Identity.Commands.DTOs;

namespace EPharmacy.Application.Identity.Provider.Commands.UpdatePassword;

public sealed record class UpdatePasswordCommand(UpdatePasswordDTO Passwords) : IRequestWrapper;

public sealed record UpdatePasswordCommandHandler : IRequestHandlerWrapper<UpdatePasswordCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePasswordCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
    {
        (_identityService, _currentUserService) = (identityService, currentUserService);
    }

    public async Task<ServiceResult> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        await _identityService.UpdatePasswordAsync(_currentUserService.UserId, request.Passwords.CurrentPassword, request.Passwords.NewPassword);

        return ServiceResult.Success();
    }
}
