namespace EPharmacy.Application.Common.Models;

public static class Roles
{
    public const string SYSTEM_ADMIN = "SystemAdmin";

    public const string CONCIERGE_AGENT = "ConciergeAgent";

    public const string PHARMACY_ADMIN = "PharmacyAdmin";

    public const string PHARMACY_AGENT_ADMIN = "PharmacyAgentAdmin";

    public const string PHARMACY_AGENT = "PharmacyAgent";

    public const string SYSTEM_ADMIN_PERMISSIONS = SYSTEM_ADMIN;

    public const string PHARMACY_ADMIN_PERMISSIONS = PHARMACY_ADMIN;

    public const string PHARMACY_AGENT_ADMIN_PERMISSIONS = $"{PHARMACY_ADMIN}, {PHARMACY_AGENT_ADMIN}";

    public const string PHARMACY_AGENT_PERMISSIONS = $"{PHARMACY_ADMIN_PERMISSIONS}, {PHARMACY_AGENT}, {PHARMACY_AGENT_ADMIN_PERMISSIONS}";

    public const string CONCIERGE_AGENT_PERMISSIONS = CONCIERGE_AGENT;

    public const string CREATE_USER_PERMISSIONS = $"{SYSTEM_ADMIN}, {CONCIERGE_AGENT}, {PHARMACY_ADMIN}, {PHARMACY_AGENT_ADMIN}";

    public const string CREATE_PHARMACY_USER_PERMISSIONS = $"{PHARMACY_ADMIN}, {PHARMACY_AGENT_ADMIN}";
}

