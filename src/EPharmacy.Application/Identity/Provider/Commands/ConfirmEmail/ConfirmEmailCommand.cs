namespace EPharmacy.Application.Identity.Provider.Commands.ConfirmEmail;

[Authorize(Roles = PHARMACY_AGENT_PERMISSIONS)]
public sealed record class ConfirmEmailCommand(string EmailToken) : IRequestWrapper;

public sealed class ConfirmEmailCommandHandler : IRequestHandlerWrapper<ConfirmEmailCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUser;

    public ConfirmEmailCommandHandler(IIdentityService identityService, ICurrentUserService currentUser)
    {
        (_identityService, _currentUser) = (identityService, currentUser);
    }

    public async Task<ServiceResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        await _identityService.ConfirmEmailAsync(_currentUser.UserId, request.EmailToken);

        return ServiceResult.Success();
    }
}
