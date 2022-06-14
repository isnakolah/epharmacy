using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Provider.Commands.UpdatePassword;

namespace EPharmacy.Application.Tests.Unit.Identity.Provider.Commands.ChangePassword;

public class UpdatePasswordCommandTests
{
    private readonly UpdatePasswordCommandHandler _sut;
    private readonly UpdatePasswordCommand _command;
    private readonly UpdatePasswordDTO _passwords;
    private readonly string _userID = Guid.NewGuid().ToString();

    private readonly Mock<IIdentityService> _identityServiceMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;

    public UpdatePasswordCommandTests()
    {
        _identityServiceMock = new();
        _currentUserServiceMock = new();
        _currentUserServiceMock.Setup(x => x.UserId).Returns(_userID);

        _passwords = new("NewTestPassword123!", "OldTestPassword987?");
        _command = new(_passwords);

        _sut = new(_identityServiceMock.Object, _currentUserServiceMock.Object);
    }

    [Fact]
    public async Task UpdatePasswordCommand_ShouldReturnSuccess_WhenUpdatePasswordSuccess()
    {
        // Arrange
        _currentUserServiceMock.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        _identityServiceMock
            .Setup(x => x.UpdatePasswordAsync(_currentUserServiceMock.Object.UserId, _passwords.CurrentPassword, _passwords.NewPassword))
            .Returns(Task.CompletedTask);


        var expectedResult = ServiceResult.Success();

        // Act
        var result = await _sut.Handle(_command, new());

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task UpdatePasswordCommand_ShouldNotThrowException_WhenUpdatePasswordSuccess()
    {
        // Arrange
        _identityServiceMock
            .Setup(x => x.UpdatePasswordAsync(_currentUserServiceMock.Object.UserId, _passwords.CurrentPassword, _passwords.NewPassword))
            .Returns(Task.CompletedTask);

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().NotThrowAsync();
    }

    [Fact]
    public async Task UpdatePasswordCommand_ShouldThrowCustomException_ErrorOccurs()
    {
        // Arrange
        var errorMessage = "Logging out error has occurred";

        _identityServiceMock
            .Setup(x => x.UpdatePasswordAsync(_currentUserServiceMock.Object.UserId, _passwords.CurrentPassword, _passwords.NewPassword))
            .ThrowsAsync(new CustomException(errorMessage));

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().ThrowExactlyAsync<CustomException>(errorMessage);
    }

    [Fact]
    public async Task UpdatePasswordCommand_ShouldThrowNotFoundException_WhenUserWithSpecifiedIDDoesNotExists()
    {
        // Arrange
        _identityServiceMock
            .Setup(x => x.UpdatePasswordAsync(_currentUserServiceMock.Object.UserId, _passwords.CurrentPassword, _passwords.NewPassword))
            .ThrowsAsync(new NotFoundException("User", _currentUserServiceMock.Object.UserId));

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().ThrowExactlyAsync<NotFoundException>("User", _currentUserServiceMock.Object.UserId);
    }
}
