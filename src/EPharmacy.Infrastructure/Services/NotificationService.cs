using static EPharmacy.Application.Common.Templates.Templates;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using EPharmacy.Application.Common.Constants;

namespace EPharmacy.Infrastructure.Services;

internal sealed class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IApiAuthService _apiAuthService;
    private readonly ISmptSettings _smptSettings;
    private readonly IConciergeSettings _conciergeSettings;
    private readonly IHttpClientHandler _client;
    private readonly IEPharmacySettings _epharmacySettings;

    public NotificationService(ILogger<NotificationService> logger, IApiAuthService apiAuthService, IHttpClientHandler client, ISmptSettings smptSetting, IConciergeSettings conciergeSettings, IEPharmacySettings epharmacySettings)
    {
        (_logger, _client, _smptSettings, _apiAuthService, _conciergeSettings, _epharmacySettings) = (logger, client, smptSetting, apiAuthService, conciergeSettings, epharmacySettings);
    }

    public ServiceResult SendEmail(Mail email, CancellationToken cancellationToken = new())
    {
        var smptClient = SetupSmptClient();

        var message = new MailMessage
        {
            From = new(_smptSettings.MailAddress.Address, _smptSettings.MailAddress.DisplayName),
            Subject = new(email.Subject),
            Body = new(email.Body),
            IsBodyHtml = true,
            Priority = MailPriority.Normal
        };
        message.To.Add(new(email.To));
        message.ReplyToList.Add(_epharmacySettings.Email);

        _logger.LogInformation("Email send to {To}", email.To);

        smptClient.SendAsync(message, cancellationToken);

        return ServiceResult.Success();
    }

    public void SendWelcomeEmail(string to, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            var (p, e) = (password, to);

            var (clientUri, _, loginEndpoint) = _epharmacySettings;

            var clientBaseUri = clientUri.Add(loginEndpoint).AddQueryParams(new(nameof(p), p), new(nameof(e), e));

            var message = Emails.Welcome(clientBaseUri);

            var email = new Mail(to, EmailSubjects.WelcomeEmail, message);

            SendEmail(email, cancellationToken);

            _logger.LogInformation("Welcome email sent to {To}", to);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending welcome email {To} with message {Message}", to, ex.Message);
        }
    }

    public async Task SendTextMessage(TextMessage textMessage, CancellationToken cancellationToken = new())
    {
        var conciergeBaseUri = _conciergeSettings.ProdURI + _conciergeSettings.TextEndpoint;

        var authenticationHeaderValue = await _apiAuthService.GetConciergeAccessTokenAsync();

        await _client.GenericRequest<TextMessage, string>(
            nameof(SendTextMessage), conciergeBaseUri, authenticationHeaderValue, cancellationToken, MethodType.Post, textMessage);
    }

    public void SendPasswordResetEmail(string to, string passToken, CancellationToken cancellationToken = default)
    {
        try
        {
            var (pt, e) = (passToken, to);

            var (clientUri, resetPasswordEndpoint, _) = _epharmacySettings;

            var clientBaseUri = clientUri.Add(resetPasswordEndpoint).AddQueryParams(new(nameof(pt), pt), new(nameof(e), e));

            var message = Emails.ForgotPassword(clientBaseUri);

            var email = new Mail(to, EmailSubjects.PasswordResetEmail, message);

            SendEmail(email, cancellationToken);

            _logger.LogInformation("Password reset email sent to {To}", to);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error sending password reset email {To} with message {Message}", to, ex.Message);
        }
    }

    private SmtpClient SetupSmptClient()
    {
        return new()
        {
            Host = _smptSettings.Host,
            Port = _smptSettings.Port,
            Timeout = _smptSettings.TimeOut,
            UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = _smptSettings.Credential,
            TargetName = _smptSettings.Target,
            EnableSsl = true
        };
    }
}