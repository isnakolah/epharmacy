namespace EPharmacy.Application.Common.Interfaces;

public interface IRoleService
{
    Task<List<string>> GetCurrentUserRolesAsync();

    Task<string> GetCurrentUserMainRoleAsync();

    Task<string> GetUserMainRoleAsync(string userID);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task AddToRoleAsync(string userID, string role);

    Task RemoveFromRoleAsync(string userID, string role);
}