using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.Common.Settings;
using EPharmacy.Application.ExternalServices.Ponea.Queries.DTOs;

namespace EPharmacy.Application.ExternalServices.Ponea.Queries.GetDrugsFromDrugIndex;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record SearchDrugsInDrugIndexQuery(string Name) : IRequestWrapper<GetDrugFromDrugIndexDto[]>;

public class SearchDrugsInDrugIndexQueryHandler : IRequestHandlerWrapper<SearchDrugsInDrugIndexQuery, GetDrugFromDrugIndexDto[]>
{
    private readonly IDrugIndexSettings _drugIndexSettings;
    private readonly IApiAuthService _apiAuthService;
    private readonly IHttpClientHandler _client;

    public SearchDrugsInDrugIndexQueryHandler(IApiAuthService apiAuthService, IHttpClientHandler client, IDrugIndexSettings drugIndexSettings)
    {
        (_client, _apiAuthService, _drugIndexSettings) = (client, apiAuthService, drugIndexSettings);
    }

    public async Task<ServiceResult<GetDrugFromDrugIndexDto[]>> Handle(SearchDrugsInDrugIndexQuery request, CancellationToken cancellationToken)
    {
        var uriAddress = new Uri(_drugIndexSettings.SearchURI);

        if (request.Name is string name && !string.IsNullOrWhiteSpace(name))
            uriAddress = uriAddress.AddQueryParameter(new(nameof(name), name));

        var authHeader = await _apiAuthService.GetDrugIndexAccessTokenAsync();

        var result = await _client.GenericRequest<object, GetDrugFromDrugIndexDto[]>(
            nameof(SearchDrugsInDrugIndexQuery)[..^5], uriAddress.ToString(), authHeader, cancellationToken, disableSSL: true);

        return result;
    }
}