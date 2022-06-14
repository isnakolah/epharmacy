using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Commands.Login;
using EPharmacy.Application.Identity.Common.Queries.DTOs;

namespace EPharmacy.Application.Tests.Unit.Identity.Common.Commands.Login;

public class LoginCommandTest
{
    private readonly LoginCommandHandler _sut;
    private readonly LoginCommand _command;
    private readonly LoginRequestDTO _credentials;

    private readonly Mock<IIdentityService> _identityServiceMock;

    public LoginCommandTest()
    {
        _identityServiceMock = new();

        _credentials = new("testEmail", "VeryStrongTestPassword");
        _command = new(_credentials);

        _sut = new(_identityServiceMock.Object);
    }

    [Fact]
    public async Task LoginCommand_ShouldReturnTokenAndUser_WhenCredentialsAreValid()
    {
        // Arrange
        var user = new ApplicationUserDTO(Guid.NewGuid().ToString(), "Test User", "testUser@email.com", "070000000", "Male", "TestRole");
        var token = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

        _identityServiceMock
            .Setup(x => x.LoginUserAsync(_credentials.Email, _credentials.Password))
            .ReturnsAsync(() => (user, token));

        var expectedResult = ServiceResult.Success(new LoginResponseDTO(user, token));

        // Act
        var result = await _sut.Handle(_command, new());

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task LoginCommand_ShouldNotThrow_WhenCredentialsAreValid()
    {
        // Arrange
        var user = new ApplicationUserDTO(Guid.NewGuid().ToString(), "Test User", "testUser@email.com", "070000000", "Male", "TestRole");
        var token = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

        _identityServiceMock
            .Setup(x => x.LoginUserAsync(_credentials.Email, _credentials.Password))
            .ReturnsAsync(() => (user, token));

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().NotThrowAsync();
    }

    [Fact]
    public async Task LoginCommand_ShouldThrowInvalidEmailOrPasswordException_WhenCredentialsAreInvalid()
    {
        // Arrange
        _identityServiceMock
            .Setup(x => x.LoginUserAsync(_credentials.Email, _credentials.Password))
            .ThrowsAsync(new InvalidEmailOrPasswordException());

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().ThrowExactlyAsync<InvalidEmailOrPasswordException>();
    }
}
