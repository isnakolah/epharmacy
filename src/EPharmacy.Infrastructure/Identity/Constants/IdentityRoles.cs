using Microsoft.AspNetCore.Identity;

namespace EPharmacy.Infrastructure.Identity.Constants;

internal static class IdentityRoles
{
    public static readonly IdentityRole SystemAdmin = new(SYSTEM_ADMIN);

    public static readonly IdentityRole ConciergeAgent = new(CONCIERGE_AGENT);

    public static readonly IdentityRole PharmacyAdmin = new(PHARMACY_ADMIN);

    public static readonly IdentityRole PharmacyAgentAdmin = new(PHARMACY_AGENT_ADMIN);

    public static readonly IdentityRole PharmacyAgent = new(PHARMACY_AGENT);
}