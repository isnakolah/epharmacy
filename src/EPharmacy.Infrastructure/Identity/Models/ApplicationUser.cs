using Microsoft.AspNetCore.Identity;

namespace EPharmacy.Infrastructure.Identity.Models;

public sealed class ApplicationUser : IdentityUser
{
    public string ConciergeID { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;
}