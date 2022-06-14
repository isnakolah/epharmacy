using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.Common.Settings;
using EPharmacy.Application.ServiceCatalogue.Provider.Queries.DTOs;

namespace EPharmacy.Application.ExternalServices.Ponea.Queries.GetPharmacyServiceCatalogueFromConcierge;

public sealed record class GetPharmacyServiceCatalogueFromConciergeQuery(string ConciergeID) : IRequestWrapper<GetPharmacyServiceCatalogueDTO[]>;

public sealed class GetPharmacyServiceCatalogueFromConciergeQueryHandler : IRequestHandlerWrapper<GetPharmacyServiceCatalogueFromConciergeQuery, GetPharmacyServiceCatalogueDTO[]>
{
    private readonly IConciergeSettings _conciergeSettings;
    private readonly IApiAuthService _authService;
    private readonly IHttpClientHandler _client;

    public GetPharmacyServiceCatalogueFromConciergeQueryHandler(IApiAuthService authService, IHttpClientHandler client, IConciergeSettings conciergeSettings)
    {
        (_authService, _client, _conciergeSettings) = (authService, client, conciergeSettings);
    }

    public async Task<ServiceResult<GetPharmacyServiceCatalogueDTO[]>> Handle(GetPharmacyServiceCatalogueFromConciergeQuery request, CancellationToken cancellationToken)
    {
        var uri = new Uri(_conciergeSettings.Uri)
            .Add(_conciergeSettings.ServiceCatalogueEndpoint)
            .AddQueryParams(new QueryParam("id", request.ConciergeID));

        var authHeader = await _authService.GetConciergeAccessTokenAsync();

        var serviceCatalogue = await _client.GenericRequest<object, ServiceResult<GetPharmacyServiceCatalogueDTO[]>>(
            nameof(GetPharmacyServiceCatalogueFromConciergeQuery)[..^5], uri.ToString(), authHeader, cancellationToken);

        return serviceCatalogue.Data;
    }
}
