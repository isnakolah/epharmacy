using EPharmacy.Application.Common.Extensions;

namespace EPharmacy.Infrastructure.Services;

internal sealed class UriService : IUriService
{
    private readonly Uri _baseUri;

    public UriService(Uri baseUri)
    {
        _baseUri = baseUri;
    }

    public Uri GetPageUri(PaginationFilter paginationFilter, QueryParam queryParam = default!)
    {
        var uri = GetPageUri(paginationFilter, new List<QueryParam> { queryParam });

        return uri;
    }

    public Uri GetPageUri(PaginationFilter paginationFilter, List<QueryParam> queryParams = default!)
    {
        var uri = _baseUri;

        var paginationQueryParams = new List<QueryParam>
        {
            new(nameof(paginationFilter.PageNumber), paginationFilter.PageNumber.ToString()),
            new(nameof(paginationFilter.PageSize), paginationFilter.PageSize.ToString()),
            new(nameof(paginationFilter.From), paginationFilter.From.ToString()),
            new(nameof(paginationFilter.To), paginationFilter.To.ToString()),
            new(nameof(paginationFilter.DateOnly), paginationFilter.DateOnly.ToString().ToLower())
        };

        AddOptionalQueryParameters(queryParams, paginationQueryParams);

        uri = uri.AddQueryParams(paginationQueryParams.ToArray());

        return uri;
    }

    private static void AddOptionalQueryParameters(List<QueryParam> queryParams, List<QueryParam> paginationQueryParams)
    {
        if (queryParams is not null)
            paginationQueryParams.AddRange(queryParams);
    }

    public Uri GetUri(QueryParam queryParam)
    {
        var uri = _baseUri.AddQueryParameter(queryParam);

        return uri;
    }

    public Uri GetIDUri(Guid ID)
    {
        var uri = _baseUri.Add(ID);

        return uri;
    }
}