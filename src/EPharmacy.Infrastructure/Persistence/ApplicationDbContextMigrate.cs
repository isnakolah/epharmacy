using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EPharmacy.Infrastructure.Persistence;

public static class ApplicationDbContextMigrate
{
    /// <summary>
    /// Migrates the database from the migration scripts
    /// </summary>
    /// <param name="builder">Application builder interface to extend</param>
    public static void UseDatabaseMigrations(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();

        var serviceProvider = scope.ServiceProvider;

        try
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Database.IsSqlServer())
                context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<IApplicationDbContext>>();

            logger.LogError(ex, "An error occurred while migrating the database.");

            throw;
        }
    }
}
