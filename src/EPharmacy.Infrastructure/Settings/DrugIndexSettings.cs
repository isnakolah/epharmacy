namespace EPharmacy.Infrastructure.Settings;

internal sealed class DrugIndexSettings : IDrugIndexSettings
{
    public string GrantType { get; init; }
    public string ClientID { get; init; }
    public string ClientSecret { get; init; }
    public string AuthURI { get; init; }
    public string SearchURI { get; init; }

    public void Deconstruct(out string grantType, out string clientID, out string clientSecret, out string authURI)
    {
        (grantType, clientID, clientSecret, authURI) = (GrantType, ClientID, ClientSecret, AuthURI);
    }
}
