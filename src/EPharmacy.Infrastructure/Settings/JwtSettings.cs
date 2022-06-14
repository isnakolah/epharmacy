namespace EPharmacy.Infrastructure.Settings;

internal sealed class JwtSettings : IJwtSettings
{
    public string Secret { get; init; }
    public string ValidIssuer { get; init; }
    public string ValidAudience { get; init; }
}