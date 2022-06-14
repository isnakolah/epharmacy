using EPharmacy.Application.Identity.Common.Queries.DTOs;
using EPharmacy.Application.Pharmacies.Queries.DTOs;

namespace EPharmacy.Application.Identity.Commands.DTOs;

public sealed record class LoginPharmacyUserResponseDTO : LoginResponseDTO
{
    public GetLessPharmacyDetailsDTO Pharmacy { get; set; }

    public LoginPharmacyUserResponseDTO(ApplicationUserDTO user, string token, GetLessPharmacyDetailsDTO pharmacy) : base(user, token)
    {
        Pharmacy = pharmacy;
    }
}