using System.Text.Json.Serialization;

namespace EPharmacy.Infrastructure.Services.Models;

internal sealed record class DrugIndexRequestBody
{
    public DrugIndexRequestBody(string grantType, string clientID, string clientSecret)
    {
        (GrantType, ClientID, ClientSecret) = (grantType, clientID, clientSecret);
    }

    [JsonPropertyName("grant_type")]
    public string GrantType { get; init; }

    [JsonPropertyName("client_id")]
    public string ClientID { get; init; }

    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; init; }
}

internal sealed record class DrugIndexResponseBody
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }
}