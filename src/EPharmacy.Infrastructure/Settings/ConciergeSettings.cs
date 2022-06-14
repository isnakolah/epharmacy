namespace EPharmacy.Infrastructure.Settings;

public sealed class ConciergeSettings : IConciergeSettings
{
    public string Uri { get; init; }
    public string ProdURI { get; init; }
    public string LocalURI { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public string TextEndpoint { get; init; }
    public string AuthEndpoint { get; init; }
    public string ToggleServiceStockedStatusEndpoint { get; init; }
    public string ServiceCatalogueEndpoint { get; init; }
}
