using EPharmacy.Application.Identity.Common.Queries.DTOs;

namespace EPharmacy.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<bool> AuthorizeAsync(string userID, string policyName);

    Task UpdatePasswordAsync(string userID, string currentPassword, string newPassword);

    Task ConfirmEmailAsync(string userId, string emailToken);

    Task<(ApplicationUserDTO User, string Token)> LoginUserAsync(string email, string password);

    Task LogoutUserAsync(string userID);

    Task<string> GetPasswordTokenAsync(string email);
}