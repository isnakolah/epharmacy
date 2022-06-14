using System.Net.Http.Headers;

namespace EPharmacy.Application.Common.Interfaces;

public interface IApiAuthService
{
    Task<AuthenticationHeaderValue> GetDrugIndexAccessTokenAsync();

    Task<AuthenticationHeaderValue> GetConciergeAccessTokenAsync();
}
