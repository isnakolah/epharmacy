using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace EPharmacy.Infrastructure.Identity.Extensions;

internal static class UserManagerExtensions
{
    public static async Task<ApplicationUser> FindByIdOrErrorAsync(this UserManager<ApplicationUser> userManager, string id)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user is null)
            throw new NotFoundException("User", id);

        return user;
    }

    public static async Task<ApplicationUser> FindByEmailOrErrorAsync(this UserManager<ApplicationUser> userManager, string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
            throw new NotFoundException($"User with email {email}, not found");

        return user;
    }
}