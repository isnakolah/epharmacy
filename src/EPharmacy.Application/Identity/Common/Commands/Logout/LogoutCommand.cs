namespace EPharmacy.Application.Identity.Common.Commands.Logout;

public sealed record class LogoutCommand() : IRequestWrapper;

public sealed class LogoutCommandHandler : IRequestHandlerWrapper<LogoutCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public LogoutCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
    {
        (_identityService, _currentUserService) = (identityService, currentUserService);
    }

    public async Task<ServiceResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _identityService.LogoutUserAsync(_currentUserService.UserId);

        return ServiceResult.Success();
    }
}