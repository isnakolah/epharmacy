using EPharmacy.Domain.Entities;
using EPharmacy.Infrastructure.Identity.Constants;
using EPharmacy.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EPharmacy.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    /// <summary>
    /// Seed the database with default data
    /// </summary>
    /// <param name="builder">IApplicationBuilder interface to extend</param>
    public static void AddDatabaseSeed(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();

        var serviceProvider = scope.ServiceProvider;

        try
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            SeedDefaultUserAsync(userManager, roleManager).GetAwaiter().GetResult();
            SeedCategoriesAsync(context).GetAwaiter().GetResult();
            SeedFormulationsAsync(context).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<IApplicationDbContext>>();

            logger.LogError(ex, "An error occurred while seeding the database.");

            throw;
        }
    }

    /// <summary>
    /// Seed the db with default roles and users
    /// </summary>
    /// <param name="userManager">The userManager service</param>
    /// <param name="roleManager">Role Manager service</param>
    /// <returns></returns>
    private static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var roles = new List<IdentityRole> {
            IdentityRoles.SystemAdmin,
            IdentityRoles.ConciergeAgent,
            IdentityRoles.PharmacyAdmin,
            IdentityRoles.PharmacyAgentAdmin,
            IdentityRoles.PharmacyAgent
        };

        foreach (var role in roles.Where(role => roleManager.Roles.All(r => r.Name != role.Name)))
        {
            await roleManager.CreateAsync(role);
        }

        var administrator = new ApplicationUser
        {
            UserName = "SystemAdmin",
            Email = "systemAdmin@poneahealth.com"
        };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "systemAdmin@123!");
            await userManager.AddToRolesAsync(administrator, new[] { IdentityRoles.SystemAdmin.Name });
        }
    }

    private static async Task SeedCategoriesAsync(IApplicationDbContext context)
    {
        if (context.Categories.Any())
            return;

        // Seed if there're no items
        context.Categories.AddRange(
            new() { Name = "Point of care equipment" },
            new() { Name = "Self test equipment" },
            new() { Name = "Hiring equipment" },
            new() { Name = "Orthopedic equipment" },
            new() { Name = "Fumigation" },
            new() { Name = "Wearables" },
            new() { Name = "Consumables" });

        await context.SaveChangesAsync(new());
    }

    private static async Task SeedFormulationsAsync(IApplicationDbContext context)
    {
        var drugFormulations = new List<Formulation>
        {
            new() { Name = "Tablets" },
            new() { Name = "Capsules" },
            new() { Name = "Lozenges" },
            new() { Name = "Suppositories" },
            new() { Name = "Emulsion" },
            new() { Name = "Suspension" },
            new() { Name = "Dispersions" },
            new() { Name = "Enemas" },
            new() { Name = "Implants" },
            new() { Name = "Injection" },
            new() { Name = "Infusion" },
            new() { Name = "Powder" },
            new() { Name = "Cream" },
            new() { Name = "Solution" },
            new() { Name = "Inhalers" },
            new() { Name = "Gel" },
            new() { Name = "Liniments" },
            new() { Name = "Ointments" },
            new() { Name = "Pessaries" },
            new() { Name = "Nasal Drops" },
            new() { Name = "Ear drops" },
            new() { Name = "Eye Drops" },
            new() { Name = "Douche" },
            new() { Name = "Mouthwash/Gargles" },
            new() { Name = "Tincture" },
            new() { Name = "Syrups" },
            new() { Name = "Linctus" },
            new() { Name = "Elixir" },
            new() { Name = "Sprays" },

        };

        drugFormulations.ForEach(formulation =>
        {
            if (!context.Formulations.Any(x => x.Name == formulation.Name))
                context.Formulations.Add(formulation);
        });

        await context.SaveChangesAsync(new());
    }
}
