namespace EPharmacy.Application.Common.Models;

public interface IJwtSettings
{
    string Secret { get; init; }
    string ValidIssuer { get; init; }
    string ValidAudience { get; init; }
}