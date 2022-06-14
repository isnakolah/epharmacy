using EPharmacy.Application.Pharmacies.Commands.DTOs;

namespace EPharmacy.Application.Identity.Commands.DTOs;

public class CreatePharmacyAdminDTO
{
    public CreateUserDTO User { get; set; }

    public CreatePharmacyDTO Pharmacy { get; set; }
}