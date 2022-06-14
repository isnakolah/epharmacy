using System.Net;
using System.Net.Mail;

namespace EPharmacy.Application.Common.Settings;

public interface ISmptSettings
{
    string Host { get; init; }
    int Port { get; init; }
    int TimeOut { get; init; }
    NetworkCredential Credential { get; }
    string Target { get; init; }
    MailAddress MailAddress { get; }
}
