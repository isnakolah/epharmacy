namespace EPharmacy.Infrastructure.Settings;

public sealed class EPharmacySettings : IEPharmacySettings
{
    public Uri ClientURI { get; init; }
    public string LocalURI { get; init; }
    public string Email { get; init; }
    public string ResetPasswordEndpoint { get; init; }
    public string LoginEndpoint { get; init; }

    public void Deconstruct(out Uri clientURI, out string resetPasswordEndpoint, out string loginEndpoint)
    {
        (clientURI, resetPasswordEndpoint, loginEndpoint) = (ClientURI, ResetPasswordEndpoint, LoginEndpoint);
    }
}