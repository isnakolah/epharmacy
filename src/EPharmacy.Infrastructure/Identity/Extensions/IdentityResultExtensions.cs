using EPharmacy.Application.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace EPharmacy.Infrastructure.Identity.Extensions;

internal static class IdentityResultExtensions
{
    public static void ToApplicationResult(this IdentityResult result, string error)
    {
        if (!result.Succeeded)
            throw new CustomException(error);
    }

    public static void CheckForErrors(this IdentityResult result)
    {
        var errors = string.Join("\n", result.Errors.Select(e => e.Description).ToArray());

        ToApplicationResult(result, errors);
    }
}