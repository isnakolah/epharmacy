using EPharmacy.Application.Identity.Provider.Commands.ForgotPassword;
using EPharmacy.Application.Identity.Provider.DTOs;

namespace EPharmacy.Application.Tests.Unit.Identity.Provider.Commands.SendPasswordResetEmail;

public class SendPasswordResetEmailCommandTests
{
    private readonly SendPasswordResetEmailCommandHandler _sut;
    private readonly SendPasswordResetEmailCommand _command;
    private readonly ForgotPasswordDTO _forgotPassDetails;

    private readonly Mock<IIdentityService> _identityServiceMock;
    private readonly Mock<INotificationService> _notificationServiceMock;

    public SendPasswordResetEmailCommandTests()
    {
        _identityServiceMock = new();
        _notificationServiceMock = new();

        _forgotPassDetails = new("testEmail@gmail.com");
        _command = new(_forgotPassDetails);

        _sut = new(_identityServiceMock.Object, _notificationServiceMock.Object);
    }

    [Fact]
    public async Task SendPasswordResetEmailCommand_ShouldReturnSuccess_WhenSuccessfull()
    {
        // Arrange
        var passwordToken = "Averylongtokentobeusedtogetthenewpassword";

        _identityServiceMock
            .Setup(x => x.GetPasswordTokenAsync(_forgotPassDetails.Email))
            .ReturnsAsync(passwordToken);

        _notificationServiceMock
            .Setup(x => x.SendPasswordResetEmail("someone@gmail.com", passwordToken, new()));

        var expectedResult = ServiceResult.Success();

        // Act
        var result = await _sut.Handle(_command, new());

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task SendPasswordResetEmailCommand_ShouldNotThrowException_WhenSuccessfull()
    {
        // Arrange
        var passwordToken = "Averylongtokentobeusedtogetthenewpassword";

        _identityServiceMock
            .Setup(x => x.GetPasswordTokenAsync(_forgotPassDetails.Email))
            .ReturnsAsync(passwordToken);

        _notificationServiceMock
            .Setup(x => x.SendPasswordResetEmail("someone@gmail.com", passwordToken, new()));

        var expectedResult = ServiceResult.Success();

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().NotThrowAsync();
    }

    [Fact]
    public async Task SendPasswordResetEmailCommand_ShouldThrowNotFoundException_WhenIDIsInvalid()
    {
        // Arrange
        var message = $"User with email {_forgotPassDetails.Email}, not found";

        _identityServiceMock
            .Setup(x => x.GetPasswordTokenAsync(_forgotPassDetails.Email))
            .ThrowsAsync(new NotFoundException(message));

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().ThrowExactlyAsync<NotFoundException>(message);
    }
}
