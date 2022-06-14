using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.Common.Settings;
using EPharmacy.Domain.Enums;

namespace EPharmacy.Application.ExternalServices.Ponea.Commands.ToggleServiceStockedStatu;

[Authorize(Roles = PHARMACY_AGENT_PERMISSIONS)]
public record class ToggleServiceStockedStatusCommand(long ServiceID, bool Stocked) : IRequestWrapper;

public class ToggleServiceStockedStatusCommandHandler : IRequestHandlerWrapper<ToggleServiceStockedStatusCommand>
{
    private readonly IConciergeSettings _conciergeSettings;
    private readonly IApiAuthService _authService;
    private readonly IHttpClientHandler _client;

    public ToggleServiceStockedStatusCommandHandler(IApiAuthService authService, IHttpClientHandler client, IConciergeSettings conciergeSettings)
    {
        (_authService, _client, _conciergeSettings) = (authService, client, conciergeSettings);
    }

    public async Task<ServiceResult> Handle(ToggleServiceStockedStatusCommand request, CancellationToken cancellationToken)
    {
        var uri = new Uri(_conciergeSettings.Uri)
            .Add(_conciergeSettings.ToggleServiceStockedStatusEndpoint, request.ServiceID)
            .AddQueryParams(new QueryParam(nameof(request.Stocked), request.Stocked.ToString()));

        var authHeader = await _authService.GetConciergeAccessTokenAsync();

        await _client.GenericRequest<object, ServiceResult>(
            nameof(ToggleServiceStockedStatusCommand)[..^7], uri.ToString(), authHeader, cancellationToken, MethodType.Post);

        return ServiceResult.Success();
    }
}
