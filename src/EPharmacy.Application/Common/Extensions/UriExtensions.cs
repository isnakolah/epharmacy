using Microsoft.AspNetCore.WebUtilities;

namespace EPharmacy.Application.Common.Extensions;

public static class UriExtensions
{
    [Obsolete($"This extension is obsolete. Use {nameof(AddQueryParams)} instead")]
    public static Uri AddQueryParameters(this Uri uri, List<QueryParam> queryParams)
    {
        var tempUri = uri.ToString();

        queryParams.ForEach(queryParam =>
            tempUri = QueryHelpers.AddQueryString(tempUri, queryParam.QueryKey, queryParam.QueryValue));

        uri = new(tempUri);

        return uri;
    }

    public static Uri AddQueryParams(this Uri uri, params QueryParam[] queryParams)
    {
        var tempUri = uri.ToString();

        foreach (var queryParam in queryParams)
            tempUri = QueryHelpers.AddQueryString(tempUri, queryParam.QueryKey, queryParam.QueryValue);

        uri = new(tempUri);

        return uri;
    }

    public static Uri AddQueryParameter(this Uri uri, QueryParam queryParam)
    {
        return uri.AddQueryParams(queryParam);
    }

    public static Uri Add(this Uri uri, params object[] addition)
    {
        foreach (var obj in addition)
            uri = new Uri($"{uri}{obj}");

        return uri;
    }
}
