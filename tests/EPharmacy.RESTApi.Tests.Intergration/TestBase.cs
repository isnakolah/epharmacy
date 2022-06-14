using EPharmacy.Application.Common.Models;
using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EPharmacy.Application.Tests.Intergration;

public class TestBase
{
    protected readonly HttpClient TestClient;

    public TestBase()
    {
        var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var dbService = services.FirstOrDefault(desc => desc.ServiceType == typeof(ApplicationDbContext));
                
                if (dbService is not null)
                    services.Remove(dbService);

                // Add an inmemory db
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });
            });

        });
        TestClient = appFactory.CreateClient();
    }

    protected async Task AuthenticateAsync()
    {
        TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
    }

    private async Task<string> GetJwtAsync()
    {
        var credentials = new LoginRequestDTO("systemAdmin@gmail.com", "WelcomeToPonea123!");

        var response = await TestClient.PostAsJsonAsync("signup", credentials);

        if (response.IsSuccessStatusCode)
            return (await response.Content.ReadFromJsonAsync<ServiceResult<LoginResponseDTO>>()).Data.Token;

        return string.Empty;
    }
}