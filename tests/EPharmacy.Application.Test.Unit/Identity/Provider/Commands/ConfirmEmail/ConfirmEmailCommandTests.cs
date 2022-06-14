using EPharmacy.Application.Identity.Provider.Commands.ConfirmEmail;

namespace EPharmacy.Application.Tests.Unit.Identity.Provider.Commands.ConfirmEmail;

public class ConfirmEmailCommandTests
{
    private readonly ConfirmEmailCommandHandler _sut;
    private readonly ConfirmEmailCommand _command;
    private readonly string _emailToken;

    private readonly Mock<IIdentityService> _identityServiceMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;

    public ConfirmEmailCommandTests()
    {
        _identityServiceMock = new();
        _currentUserServiceMock = new();
        _currentUserServiceMock.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        _emailToken = $"{Guid.NewGuid()}{Guid.NewGuid()}";
        _command = new(_emailToken);

        _sut = new(_identityServiceMock.Object, _currentUserServiceMock.Object);
    }

    [Fact]
    public async Task ConfirmEmailCommand_ShouldReturnSuccess_WhenEmailTokenValid()
    {
        // Arrange
        _identityServiceMock
            .Setup(x => x.ConfirmEmailAsync(_currentUserServiceMock.Object.UserId, _emailToken))
            .Returns(Task.CompletedTask);

        var expectedResult = ServiceResult.Success();

        // Act
        var result = await _sut.Handle(_command, new());

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task ConfirmEmailCommand_ShouldNotThrowException_WhenEmailTokenValid()
    {
        // Arrange
        _identityServiceMock
            .Setup(x => x.ConfirmEmailAsync(_currentUserServiceMock.Object.UserId, _emailToken))
            .Returns(Task.CompletedTask);

        var expectedResult = ServiceResult.Success();

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ConfirmEmailCommand_ShouldThrowNotFoundException_WhenUserWithIDNotFound()
    {
        // Arrange
        _identityServiceMock
            .Setup(x => x.ConfirmEmailAsync(_currentUserServiceMock.Object.UserId, _emailToken))
            .ThrowsAsync(new NotFoundException("User", _currentUserServiceMock.Object.UserId));

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().ThrowExactlyAsync<NotFoundException>("User", _currentUserServiceMock.Object.UserId);
    }

    [Fact]
    public async Task ConfirmEmailCommand_ShouldThrowCustomException_WhenErrorOccurs()
    {
        // Arrange
        var errorMessage = "Error confirming email";

        _identityServiceMock
            .Setup(x => x.ConfirmEmailAsync(_currentUserServiceMock.Object.UserId, _emailToken))
            .ThrowsAsync(new CustomException(errorMessage));

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().ThrowExactlyAsync<CustomException>(errorMessage);
    }
}
