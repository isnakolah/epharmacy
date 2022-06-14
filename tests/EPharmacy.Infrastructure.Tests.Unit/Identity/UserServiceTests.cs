using EPharmacy.Application.Common.Interfaces;
using EPharmacy.Infrastructure.Identity.Models;
using EPharmacy.Infrastructure.Identity.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace EPharmacy.Application.Tests.Intergration.Identity;

// Test the UserService
public class UserServiceTests
{
    // Mock user store
    private readonly Mock<IUserStore<ApplicationUser>> _userStore;
    private readonly UserService _sut;
    private readonly Mock<IRoleService> _roleService = new();
    private readonly Mock<INotificationService> _notificationService = new();
    private readonly Mock<IMemoryCache> _memoryCache = new();
    private readonly UserManager<ApplicationUser> _userManager;


    public UserServiceTests()
    {
        _sut = new UserService(_userManager, _roleService.Object, _notificationService.Object, _memoryCache.Object);
    }

    [Fact]
    public async Task GetUserNameAsync_ReturnsCorrectUserName()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var userName = "testUser";


        // Act
        var result = await _sut.GetUserNameAsync(userId);

        // Assert
        result.Should().Be(userName);
    }

    // Text GetUserIdAsync
    [Fact]
    public async Task GetUserIdAsync_ReturnsCorrectUserId()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var userName = "testUser";


        // Act
        var result = await _sut.GetUserIdAsync(userName);

        // Assert
        result.Should().Be(userId);
    }
}