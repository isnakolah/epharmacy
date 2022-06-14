using EPharmacy.Application.Identity.Common.Queries.DTOs;
using EPharmacy.Infrastructure.Identity.Models;

namespace EPharmacy.Infrastructure.Identity.Extensions;

internal static class ApplicationUserExtensions
{
    public static ApplicationUserDTO ToDTO(this ApplicationUser user, string role)
    {
        return new ApplicationUserDTO(user.Id, user.FullName, user.Email, user.PhoneNumber, user.Gender, role, user.EmailConfirmed, user.PhoneNumberConfirmed);
    }
}