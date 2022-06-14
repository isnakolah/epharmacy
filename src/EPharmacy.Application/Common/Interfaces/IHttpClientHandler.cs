using EPharmacy.Domain.Enums;
using System.Net.Http.Headers;

namespace EPharmacy.Application.Common.Interfaces;

/// <summary>
/// IHttpClientHandler interface
/// </summary>
public interface IHttpClientHandler
{
    /// <summary>
    /// Create a Request
    /// </summary>
    /// <typeparam name="TRequest">The type of request</typeparam>
    /// <typeparam name="TResult">The type of the result</typeparam>
    /// <param name="apiName">The name for the api</param>
    /// <param name="url">The endpoint of the application</param>
    /// <param name="authenticationHeader">Authentication headers</param>
    /// <param name="cancellationToken">The cancellation token for the async process</param>
    /// <param name="method">Optional method type (Get/Post) default set to Get</param>
    /// <param name="requestEntity">The request entity</param>
    /// <returns></returns>
    Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(
        string apiName,
        string url,
        AuthenticationHeaderValue authenticationHeader = default!,
        CancellationToken cancellationToken = default!,
        MethodType method = MethodType.Get,
        TRequest requestEntity = default!,
        bool disableSSL = default!
        )
        where TResult : class where TRequest : class;
}