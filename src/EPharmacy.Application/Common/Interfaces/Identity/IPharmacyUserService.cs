using EPharmacy.Application.Identity.Common.Queries.DTOs;

namespace EPharmacy.Application.Common.Interfaces;

public interface IPharmacyUserService
{
    Task<Guid> GetUserPharmacyIDAsync(string userID);

    Task<ApplicationUserDTO[]> GetAllPharmacyAdminUsersAsync();

    Task<ApplicationUserDTO[]> GetPharmacyUsersAsync(Guid pharmacyID);

    Task<ApplicationUserDTO> GetSinglePharmacyUserAsync(Guid id);
}