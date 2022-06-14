namespace EPharmacy.Application.Common.Interfaces;

public interface INotificationService
{
    ServiceResult SendEmail(Mail email, CancellationToken cancellationToken = new());

    Task SendTextMessage(TextMessage textMessage, CancellationToken cancellationToken = new());

    void SendWelcomeEmail(string to, string password, CancellationToken cancellationToken = new());

    void SendPasswordResetEmail(string to, string passToken, CancellationToken cancellationToken = new());
}