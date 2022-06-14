namespace EPharmacy.Application.Common.Settings;

public interface IConciergeSettings
{
    string Uri { get; init; }
    string ProdURI { get; init; }
    string LocalURI { get; init; }
    string Username { get; init; }
    string Password { get; init; }
    string TextEndpoint { get; init; }
    string AuthEndpoint { get; init; }
    string ToggleServiceStockedStatusEndpoint { get; init; }
    string ServiceCatalogueEndpoint { get; init; }
}