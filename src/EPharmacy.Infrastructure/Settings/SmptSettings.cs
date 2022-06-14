using System.Net.Mail;
using System.Net;
using EPharmacy.Application.Common.Extensions;

namespace EPharmacy.Infrastructure.Settings;

internal sealed class SmptSettings : ISmptSettings
{
    public string Host { get; init; }
    public int Port { get; init; }
    public int TimeOut { get; init; }
    public string Target { get; init; }
    #region Properties to map to configuration
    public string DisplayName { get; init; }
    public string Address { get; init; }
    public string Password { get; init; }
    #endregion
    public NetworkCredential Credential => new(Address, Password.Decrypt());
    public MailAddress MailAddress => new(Address, DisplayName);
}
