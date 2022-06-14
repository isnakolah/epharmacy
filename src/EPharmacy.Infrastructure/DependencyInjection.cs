using EPharmacy.Infrastructure.Identity.Models;
using EPharmacy.Infrastructure.Identity.Services;
using EPharmacy.Infrastructure.Persistence;
using EPharmacy.Infrastructure.Services;
using EPharmacy.Infrastructure.Settings;
using EPharmacy.Infrastructure.Settings.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("EPharmacy.Infrastructure.Tests.Unit")]
namespace EPharmacy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("EPharmacyDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddConfigurations(configuration);

        services.AddSingleton<IMarkupService, MarkupService>();
        services.AddSingleton<IApiAuthService, ApiAuthService>();

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddScoped<IPaginate, PaginationService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IUriService>(o =>
        {
            var accessor = o.GetRequiredService<IHttpContextAccessor>();
            var request = accessor?.HttpContext?.Request;
            var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
            return new UriService(new(uri + request?.Path.Value));
        });

        services.AddTransient<IHttpClientHandler, Services.Handlers.HttpClientHandler>();
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IPharmacyUserService, PharmacyUserService>();
        services.AddTransient<IExpiryService, ExpiryService>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<ICurrentUserPharmacy, CurrentUserPharmacy>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.BindSection<JwtSettings>();

                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            })
            .AddIdentityServerJwt();

        services.AddAuthorization(options => options.AddPolicy("CanPurge", policy => policy.RequireRole(SYSTEM_ADMIN)));

        return services;
    }
}