using EPharmacy.Application.Identity.Common.Commands.Logout;

namespace EPharmacy.Application.Tests.Unit.Identity.Common.Commands.Logout;

public class LogoutCommandTests
{
    private readonly LogoutCommandHandler _sut;
    private readonly LogoutCommand _command;

    private readonly Mock<IIdentityService> _identityServiceMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;

    public LogoutCommandTests()
    {
        _identityServiceMock = new();
        _currentUserServiceMock = new();
        _currentUserServiceMock.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        _command = new();

        _sut = new(_identityServiceMock.Object, _currentUserServiceMock.Object);
    }

    [Fact]
    public async Task LogoutCommand_ShouldReturnSuccess_WhenLogoutSucceeded()
    {
        // Arrange
        _identityServiceMock
            .Setup(x => x.LogoutUserAsync(_currentUserServiceMock.Object.UserId))
            .Returns(Task.CompletedTask);

        var expectedResult = ServiceResult.Success();

        // Act
        var result = await _sut.Handle(_command, new());

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task LogoutCommand_ShouldNotThrowException_WhenLogoutSucceeded()
    {
        // Arrange
        _identityServiceMock
            .Setup(x => x.LogoutUserAsync(_currentUserServiceMock.Object.UserId))
            .Returns(Task.CompletedTask);

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().NotThrowAsync();
    }
}
