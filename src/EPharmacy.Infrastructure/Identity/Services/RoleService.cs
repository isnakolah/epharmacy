using EPharmacy.Application.Common.Constants;
using EPharmacy.Infrastructure.Identity.Extensions;
using EPharmacy.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace EPharmacy.Infrastructure.Identity.Services;

internal sealed class RoleService : IRoleService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUserService _currentUser;
    private readonly IMemoryCache _memoryCache;

    public RoleService(UserManager<ApplicationUser> userManager, ICurrentUserService currentUser, IMemoryCache memoryCache)
    {
        (_userManager, _currentUser, _memoryCache) = (userManager, currentUser, memoryCache);
    }

    public async Task AddToRoleAsync(string userID, string role)
    {
        var user = await _userManager.FindByIdOrErrorAsync(userID);
        var result = await _userManager.AddToRoleAsync(user, role);

        result.CheckForErrors();

        _memoryCache.Remove(CacheKeys.ROLE + role + userID);
    }

    public async Task<string> GetCurrentUserMainRoleAsync()
    {
        return await GetUserMainRoleAsync(_currentUser.UserId);
    }

    public async Task<List<string>> GetCurrentUserRolesAsync()
    {
        try
        {
            return await GetUserRolesAsync(_currentUser.UserId);
        }
        catch
        {
            throw new UnauthorizedAccessException();
        }
    }

    public async Task<string> GetUserMainRoleAsync(string userID)
    {
        var roles = await GetUserRolesAsync(userID);

        if (roles.Contains(CONCIERGE_AGENT))
            return CONCIERGE_AGENT;

        if (roles.Contains(PHARMACY_ADMIN))
            return PHARMACY_ADMIN;

        if (roles.Contains(PHARMACY_AGENT_ADMIN))
            return PHARMACY_AGENT_ADMIN;

        return roles[0];
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var roleKey = CacheKeys.ROLE + role + userId;

        if (_memoryCache.TryGetValue(roleKey, out bool cachedIsInRole))
            return cachedIsInRole;

        var user = await _userManager.FindByIdOrErrorAsync(userId);
        var isInRole = await _userManager.IsInRoleAsync(user, role);

        return _memoryCache.Set(roleKey, isInRole);
    }

    public async Task RemoveFromRoleAsync(string userID, string role)
    {
        var user = await _userManager.FindByIdOrErrorAsync(userID);

        var result = await _userManager.RemoveFromRoleAsync(user, role);

        result.CheckForErrors();

        _memoryCache.Remove(CacheKeys.ROLE + role + userID);
    }

    private async Task<List<string>> GetUserRolesAsync(string userId)
    {
        var userRolesKey = CacheKeys.USER_ROLES + userId;

        if (_memoryCache.TryGetValue(userRolesKey, out List<string> cachedRoles))
            return cachedRoles;

        var user = await _userManager.FindByIdOrErrorAsync(userId);

        var roles = await _userManager.GetRolesAsync(user);

        return roles.ToList();
    }
}