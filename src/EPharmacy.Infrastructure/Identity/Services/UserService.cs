using EPharmacy.Application.Common.Constants;
using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Queries.DTOs;
using EPharmacy.Infrastructure.Identity.Extensions;
using EPharmacy.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace EPharmacy.Infrastructure.Identity.Services;

internal sealed class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRoleService _roleService;
    private readonly INotificationService _notificationService;
    private readonly IMemoryCache _memoryCache;

    public UserService(UserManager<ApplicationUser> userManager, IRoleService roleService, INotificationService notificationService, IMemoryCache memoryCache)
    {
        (_userManager, _roleService, _notificationService, _memoryCache) = (userManager, roleService, notificationService, memoryCache);
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var userNameKey = CacheKeys.USERNAME + userId;

        if (_memoryCache.TryGetValue(userNameKey, out string cachedName))
            return cachedName;

        var user = await _userManager.FindByIdOrErrorAsync(userId);

        return _memoryCache.Set(userNameKey, user.UserName);
    }

    public async Task<string> CreateUserAsync(CreateUserDTO newUser)
    {
        if (await _userManager.FindByEmailAsync(newUser.Email) is not null)
            throw new SimilarEmailExistsException();

        var user = new ApplicationUser
        {
            UserName = newUser.Email.Trim(),
            Email = newUser.Email.Trim(),
            FullName = newUser.FullName.Trim(),
            PhoneNumber = newUser.PhoneNumber.Trim(),
            Gender = newUser.Gender.Trim(),
        };
        var password = "WelcomeToPonea123!";
        var result = await _userManager.CreateAsync(user, password);

        result.CheckForErrors();

        if (await _roleService.GetCurrentUserMainRoleAsync() is not SYSTEM_ADMIN)
            _notificationService.SendWelcomeEmail(user.Email, password);

        return user.Id;
    }

    public async Task<string> GetUserIdAsync(string email)
    {
        var user = await _userManager.FindByEmailOrErrorAsync(email);

        return user.Id;
    }

    public async Task UpdateUserAsync(CreateUserDTO updatedUser, string userID)
    {
        var user = await _userManager.FindByIdOrErrorAsync(userID);

        if (await _userManager.FindByEmailAsync(updatedUser.Email) is not null && !user.Email.Equals(updatedUser.Email))
            throw new SimilarEmailExistsException();

        user.FullName = updatedUser.FullName;
        user.PhoneNumber = updatedUser.PhoneNumber;
        user.Gender = updatedUser.Gender;
        user.Email = updatedUser.Email;

        var result = await _userManager.UpdateAsync(user);

        result.CheckForErrors();
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdOrErrorAsync(userId);
        var result = await _userManager.DeleteAsync(user);

        result.CheckForErrors();
    }

    public async Task<string> CreateUserAddToRoleAsync(CreateUserDTO newUser, string role)
    {
        var userID = await CreateUserAsync(newUser);

        await _roleService.AddToRoleAsync(userID, role);

        return userID;
    }

    public async Task<ApplicationUserDTO> GetUserByID(Guid pharmacyAdminID)
    {
        var user = await _userManager.FindByIdOrErrorAsync(pharmacyAdminID.ToString());
        var role = await _roleService.GetUserMainRoleAsync(user.Id);

        return user.ToDTO(role);
    }
}