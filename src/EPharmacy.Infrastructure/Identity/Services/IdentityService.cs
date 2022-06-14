using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Common.Interfaces;
using EPharmacy.Application.Identity.Common.Queries.DTOs;
using EPharmacy.Infrastructure.Identity.Extensions;
using EPharmacy.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EPharmacy.Infrastructure.Identity.Services;

internal sealed class IdentityService : IIdentityService
{
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly ITokenService _tokenService;
    private readonly IRoleService _roleService;

    public IdentityService(IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService, IRoleService roleService, ITokenService tokenService)
{
        (_userManager, _userClaimsPrincipalFactory, _authorizationService, _roleService, _tokenService) = (userManager, userClaimsPrincipalFactory, authorizationService, roleService, tokenService);
    }

    public async Task<bool> AuthorizeAsync(string userID, string policyName)
    {
        var user = await _userManager.FindByIdOrErrorAsync(userID);
        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task UpdatePasswordAsync(string userID, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdOrErrorAsync(userID);
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        result.CheckForErrors();
    }

    public async Task ConfirmEmailAsync(string userId, string emailToken)
    {
        var user = await _userManager.FindByIdOrErrorAsync(userId);
        var result = await _userManager.ConfirmEmailAsync(user, emailToken);

        result.CheckForErrors();
    }

    public async Task<(ApplicationUserDTO User, string Token)> LoginUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (!await _userManager.CheckPasswordAsync(user, password))
            throw new InvalidEmailOrPasswordException();

        var token = _tokenService.CreateJwtSecurityToken(user.Id);
        var loginResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(user.Id, token[^30..], user.Id));

        await _userManager.SetAuthenticationTokenAsync(user, user.Id, "JWT", token);

        var role = await _roleService.GetUserMainRoleAsync(user.Id);

        loginResult.CheckForErrors();

        return (user.ToDTO(role), token);
    }

    public async Task LogoutUserAsync(string userID)
    {
        var user = await _userManager.FindByIdOrErrorAsync(userID);

        await _userManager.RemoveLoginAsync(user, user.Id, user.Id);
        await _userManager.RemoveAuthenticationTokenAsync(user, user.Id, user.Id);
    }

    public async Task<string> GetPasswordTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailOrErrorAsync(email);
        var passwordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        return passwordToken;
    }
}