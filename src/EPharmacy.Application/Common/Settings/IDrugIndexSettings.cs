namespace EPharmacy.Application.Common.Settings;

public interface IDrugIndexSettings
{
    string GrantType { get; init; }
    string ClientID { get; init; }
    string ClientSecret { get; init; }
    string AuthURI { get; init; }
    string SearchURI { get; init; }

    void Deconstruct(out string grantType, out string clientID, out string clientSecret, out string authURI);
}