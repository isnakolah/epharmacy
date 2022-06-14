namespace EPharmacy.Application.Common.Settings;

public interface IEPharmacySettings
{
    Uri ClientURI { get; init; }
    string LocalURI { get; init; }
    string Email { get; init; }
    string ResetPasswordEndpoint { get; init; }
    string LoginEndpoint { get; init; }

    void Deconstruct(out Uri clientURI, out string resetPasswordEndpoint, out string loginEndpoint);
}
