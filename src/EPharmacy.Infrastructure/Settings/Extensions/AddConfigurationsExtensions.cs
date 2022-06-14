using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EPharmacy.Infrastructure.Settings.Extensions;

public static class AddConfigurationsExtensions
{
    /// <summary>
    /// Adds the configurations
    /// </summary>
    internal static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingletonConfiguration<ISmptSettings, SmptSettings>(configuration);
        services.AddSingletonConfiguration<IJwtSettings, JwtSettings>(configuration);
        services.AddSingletonConfiguration<IConciergeSettings, ConciergeSettings>(configuration);
        services.AddSingletonConfiguration<IEPharmacySettings, EPharmacySettings>(configuration);
        services.AddSingletonConfiguration<IDrugIndexSettings, DrugIndexSettings>(configuration);

        return services;
    }

    public static T BindSection<T>(this IConfiguration configuration)
    {
        var settings = configuration.GetSection(typeof(T).Name).Get<T>();

        return settings;
    }

    public static (T1, T2) BindSection<T1, T2>(this IConfiguration configuration)
    {
        return (configuration.BindSection<T1>(), configuration.BindSection<T2>());
    }

    /// <summary>
    /// Add a configuration as a singleton service
    /// </summary>
    /// <typeparam name="TService">Interface for the configuration</typeparam>
    /// <typeparam name="TImplementation">Implementation for the TService</typeparam>
    /// <param name="configuration">IConfiguration to get the sections</param>
    /// <returns>Service collection</returns>
    private static IServiceCollection AddSingletonConfiguration<TService, TImplementation>(this IServiceCollection services, IConfiguration configuration)
        where TImplementation : class, TService
        where TService : class
    {
        services.Configure<TImplementation>(configuration.GetSection(typeof(TImplementation).Name));
        services.AddSingleton<TService>(provider => provider.GetRequiredService<IOptions<TImplementation>>().Value);

        return services;
    }
}
