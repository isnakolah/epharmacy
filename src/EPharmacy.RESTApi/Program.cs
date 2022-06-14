using EPharmacy.Application;
using EPharmacy.Application.Common.Interfaces;
using EPharmacy.Infrastructure;
using EPharmacy.Infrastructure.Persistence;
using EPharmacy.Infrastructure.Settings;
using EPharmacy.Infrastructure.Settings.Extensions;
using EPharmacy.RESTApi.Filters;
using EPharmacy.RESTApi.Services;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
    options.Filters.Add<APIResultFilterAttribute>();
}).AddFluentValidation();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EPharmacy Application", Version = "v1" });
    c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        Description = "Type into the textbox: Bearer {your JWT token}."
    });
});

builder.Services.AddCors(options =>
{
    var (conciergeSettings, epharmacySettings) = builder.Configuration.BindSection<ConciergeSettings, EPharmacySettings>();

    options.AddPolicy("LivePolicy", policyBuilder =>
    {
        policyBuilder.WithOrigins(conciergeSettings.Uri).AllowAnyHeader().AllowAnyMethod();
    });
    options.AddPolicy("LocalPolicy", policyBuilder =>
    {
        policyBuilder.WithOrigins(epharmacySettings.LocalURI, conciergeSettings.LocalURI).AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/api/v1/Error");
    app.UseHsts();
}
app.UseCors("LocalPolicy");
app.UseCors("LivePolicy");
app.UseHealthChecks("/api/v1/health");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EPharmacy Application v1"));
app.UseRouting();
app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}");
    endpoints.MapRazorPages();
});
app.UseDatabaseMigrations();
app.AddDatabaseSeed();

app.Run();