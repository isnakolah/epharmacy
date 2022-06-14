using AutoMapper;
using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Provider.Commands.LoginPharmacyUser;

namespace EPharmacy.Application.Tests.Unit.Identity.Provider.Commands.LoginPharmacyUser;

public class LoginPharmacyUserCommandTests
{
    private readonly LoginPharmacyUserCommandHandler _sut;
    private readonly LoginPharmacyUserCommand _command;
    private readonly LoginRequestDTO _credentials;

    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly Mock<IConfigurationProvider> _mapperConfigMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IRoleService> _roleServiceMock;
    private readonly Mock<IPharmacyUserService> _pharmacyUserServiceMock;

    public LoginPharmacyUserCommandTests()
    {
        _mediatorMock = new();
        _contextMock = new();
        _mapperConfigMock = new();
        _userServiceMock = new();
        _roleServiceMock = new();
        _pharmacyUserServiceMock = new();

        _credentials = new("NewTestPassword123!", "OldTestPassword987?");
        _command = new(_credentials);

        _sut = new(_mediatorMock.Object, _contextMock.Object, _mapperConfigMock.Object, _userServiceMock.Object, _roleServiceMock.Object, _pharmacyUserServiceMock.Object);
    }

    [Fact]
    public async Task LoginPharmacyUserCommand_ShouldReturnSuccessfulResponse_WhenSucceeded()
    {
        // Arrange
        _userServiceMock
            .Setup(x => x.GetUserIdAsync(_credentials.Email))
            .ReturnsAsync(Guid.NewGuid().ToString());


        // Act

        // Assert
    }
}
