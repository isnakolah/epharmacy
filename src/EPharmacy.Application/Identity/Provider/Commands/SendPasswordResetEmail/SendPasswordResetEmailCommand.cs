using EPharmacy.Application.Identity.Provider.DTOs;

namespace EPharmacy.Application.Identity.Provider.Commands.ForgotPassword;

public sealed record class SendPasswordResetEmailCommand(ForgotPasswordDTO ForgotPassword) : IRequestWrapper;

public sealed class SendPasswordResetEmailCommandHandler : IRequestHandlerWrapper<SendPasswordResetEmailCommand>
{
    private readonly IIdentityService _identityService;
    private readonly INotificationService _notificationService;

    public SendPasswordResetEmailCommandHandler(IIdentityService identityService, INotificationService notificationService)
    {
        (_identityService, _notificationService) = (identityService, notificationService);
    }

    public async Task<ServiceResult> Handle(SendPasswordResetEmailCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityService.GetPasswordTokenAsync(request.ForgotPassword.Email);

        _notificationService.SendPasswordResetEmail(request.ForgotPassword.Email, token, cancellationToken);

        return ServiceResult.Success();
    }
}
