using EPharmacy.Application.Common.Models;
using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Tests.Intergration;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using EPharmacy.RESTApi.Controllers.v1;
using Xunit;

namespace EPharmacy.RESTApi.Tests.Intergration.Identity;

public class LoginTests : TestBase
{
    [Fact]
    public async Task POST_logs_in_systemAdmin_success()
    {
        var email = "systemAdmin@poneahealth.com";

        var password = "systemAdmin@123!";

        var credentials = new LoginRequestDTO(email, password);

        var response = await TestClient.PostAsJsonAsync(Routes.System.Identity.Login, credentials);

        var responseContent = await response.Content.ReadFromJsonAsync<ServiceResult<LoginResponseDTO>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        responseContent?.Data.Token.Should().NotBeNullOrEmpty();

        responseContent?.Data.User.Role.Should().Be(Roles.SYSTEM_ADMIN);

        responseContent?.Data.User.Email.Should().Be(email);
    }

    [Fact]
    public async Task POST_logs_in_systemAdmin_fail()
    {
        var email = "systemAdmin@poneahealth.com";

        var password = "wrongPassword";

        var credentials = new LoginRequestDTO(email, password);

        var response = await TestClient.PostAsJsonAsync(Routes.System.Identity.Login, credentials);

        var responseContent = await response.Content.ReadFromJsonAsync<ServiceResult<LoginResponseDTO>>();

        response.IsSuccessStatusCode.Should().BeFalse();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        responseContent?.Error.Message.Should().Be("Invalid email or password");

        responseContent?.Data.Should().BeNull();

        responseContent?.Succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task POST_creates_a_user()
    {
        var newUser = new CreateUserDTO("Test User", "TestUser", "Male", "0700000000", "testUser@test.com");

        var response = await TestClient.PostAsJsonAsync(Routes.System.Users.Create, newUser);

        response.IsSuccessStatusCode.Should().BeFalse();

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}