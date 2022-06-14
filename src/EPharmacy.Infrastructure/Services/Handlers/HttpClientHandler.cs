using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Domain.Enums;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EPharmacy.Infrastructure.Services.Handlers;

internal class HttpClientHandler : IHttpClientHandler
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<IHttpClientHandler> _logger;

    public HttpClientHandler(IHttpClientFactory httpClientFactory, ILogger<IHttpClientHandler> logger)
    {
        (_httpClientFactory, _logger) = (httpClientFactory, logger);
    }

    public async Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(
        string apiName,
        string url,
        AuthenticationHeaderValue authenticationHeader = default!,
        CancellationToken cancellationToken = default!,
        MethodType method = MethodType.Get,
        TRequest? requestEntity = default!,
        bool disableSSL = false)
        where TRequest : class
        where TResult : class
    {
        var httpClient = _httpClientFactory.CreateClient(apiName);

        if (disableSSL)
        {
            var handler = new System.Net.Http.HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, ssl) => true
            };

            httpClient = new HttpClient(handler);
        }

        httpClient.Timeout = TimeSpan.FromSeconds(10);
        httpClient.DefaultRequestHeaders.Authorization = authenticationHeader;

        var requestName = typeof(TRequest).Name;

        try
        {
            _logger.LogInformation("HttpClient Request: {RequestName} {@Request} hitting {Uri}", requestName, requestEntity, url);

            var response = method switch
            {
                MethodType.Get => await httpClient.GetAsync(url, cancellationToken),
                MethodType.Post => await httpClient.PostAsJsonAsync(url, requestEntity, cancellationToken),
                _ => null
            };

            if (response is not null && response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<TResult>(cancellationToken: cancellationToken);

                return ServiceResult.Success(data!);
            }

            if (response is null)
                throw new HttpException(ServiceError.ServiceProvider.Message);

            var message = await response.Content.ReadAsStringAsync(cancellationToken);

            throw new HttpException(message, (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HttpClient Request: Unhandled Exception for Request {RequestName} {@Request}", requestName, requestEntity);

            throw new HttpException(ex.ToString());
        }
    }
}