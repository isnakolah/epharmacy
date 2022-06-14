using EPharmacy.Application.Identity.Common.Queries.DTOs;
using EPharmacy.Application.Pharmacies.Queries.DTOs;

namespace EPharmacy.Application.Identity.Ponea.Queries._DTOs;

public record ApplicationUserWithPharmacyDTO(ApplicationUserDTO User, GetPharmacyDTO Pharmacy);