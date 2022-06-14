using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Domain.Enums;
using EPharmacy.Infrastructure.Services.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;

namespace EPharmacy.Infrastructure.Services;

internal sealed class ApiAuthService : IApiAuthService
{
    private const string DRUG_INDEX_ACCESS_TOKEN = nameof(DRUG_INDEX_ACCESS_TOKEN);
    private const string CONCIERGE_ACCESS_TOKEN = nameof(CONCIERGE_ACCESS_TOKEN);
    private const string BEARER_AUTH_SCHEME = JwtBearerDefaults.AuthenticationScheme;

    private readonly IHttpClientHandler _client;
    private readonly IMemoryCache _memoryCache;
    private readonly IConciergeSettings _conciergeSettings;
    private readonly IDrugIndexSettings _drugIndexSettings;

    public ApiAuthService(IHttpClientHandler client, IMemoryCache memoryCache, IConciergeSettings conciergeSettings, IDrugIndexSettings drugIndexSettings)
    {
        (_client, _memoryCache, _conciergeSettings, _drugIndexSettings) = (client, memoryCache, conciergeSettings, drugIndexSettings);
    }

    public async Task<AuthenticationHeaderValue> GetDrugIndexAccessTokenAsync()
    {
        if (_memoryCache.TryGetValue(DRUG_INDEX_ACCESS_TOKEN, out string token))
            return new(BEARER_AUTH_SCHEME, token);

        return new(BEARER_AUTH_SCHEME, await LoginToDrugIndexAsync());
    }

    public async Task<AuthenticationHeaderValue> GetConciergeAccessTokenAsync()
    {
        if (_memoryCache.TryGetValue(CONCIERGE_ACCESS_TOKEN, out string token))
            return new(BEARER_AUTH_SCHEME, token);

        return new(BEARER_AUTH_SCHEME, await LoginToConciergeAsync());
    }

    private async Task<string> LoginToConciergeAsync()
    {
        var (username, password) = (_conciergeSettings.Username, _conciergeSettings.Password);

        var body = new ConciergeAuthRequestBody(username, password);

        var apiUri = _conciergeSettings.ProdURI + _conciergeSettings.AuthEndpoint;

        var token = await _client.GenericRequest<ConciergeAuthRequestBody, string>(
            nameof(LoginToConciergeAsync), apiUri, method: MethodType.Post, requestEntity: body);

        return _memoryCache.Set(CONCIERGE_ACCESS_TOKEN, token.Data, DateTimeOffset.UtcNow.AddDays(10));
    }

    private async Task<string> LoginToDrugIndexAsync()
    {
        var (grantType, clientID, clientSecret, apiAuthUri) = _drugIndexSettings;

        var body = new DrugIndexRequestBody(grantType, clientID, clientSecret);

        var response = await _client.GenericRequest<DrugIndexRequestBody, DrugIndexResponseBody>(
            nameof(LoginToDrugIndexAsync), apiAuthUri, method: MethodType.Post, requestEntity: body, disableSSL: true);

        if (!response.Succeeded)
            throw new CustomException(response.Error.Message);

        var data = response.Data;

        return _memoryCache.Set(DRUG_INDEX_ACCESS_TOKEN, data.AccessToken, DateTimeOffset.UtcNow.AddSeconds(data.ExpiresIn));
    }
}