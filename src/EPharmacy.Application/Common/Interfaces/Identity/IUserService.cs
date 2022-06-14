using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Queries.DTOs;

namespace EPharmacy.Application.Common.Interfaces;

public interface IUserService
{
    Task<string> GetUserNameAsync(string userId);

    Task<string> CreateUserAsync(CreateUserDTO newUser);

    Task<string> GetUserIdAsync(string email);

    Task UpdateUserAsync(CreateUserDTO updatedUser, string userID);

    Task DeleteUserAsync(string userId);

    Task<string> CreateUserAddToRoleAsync(CreateUserDTO newUser, string role);

    Task<ApplicationUserDTO> GetUserByID(Guid pharmacyAdminID);
}