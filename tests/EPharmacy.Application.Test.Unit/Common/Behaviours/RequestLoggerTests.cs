using EPharmacy.Application.Common.Behaviours;
using EPharmacy.Application.Pharmacies.Commands.CreatePharmacy;
using EPharmacy.Application.Pharmacies.Commands.DTOs;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace EPharmacy.Application.Tests.Unit.Common.Behaviours;

public class RequestLoggerTests
{
    private readonly Mock<ILogger<CreatePharmacyCommand>> _logger;
    private readonly Mock<ICurrentUserService> _currentUserService;
    private readonly Mock<IUserService> _userService;


    public RequestLoggerTests()
    {
        _logger = new Mock<ILogger<CreatePharmacyCommand>>();

        _currentUserService = new Mock<ICurrentUserService>();

        _userService = new Mock<IUserService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns("Administrator");

        var requestLogger = new LoggingBehaviour<CreatePharmacyCommand>(
            _logger.Object, _currentUserService.Object, _userService.Object);

        await requestLogger.Process(new CreatePharmacyCommand(
            new CreatePharmacyDTO
            {
                Name = "Test",
                Location = "TestLocation",
                Description = "Test description",
                ConciergeID = "123testid"
            }), new CancellationToken());

        _userService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreatePharmacyCommand>(
            _logger.Object, _currentUserService.Object, _userService.Object);

        await requestLogger.Process(new CreatePharmacyCommand(
            new CreatePharmacyDTO
            {
                Name = "Test2",
                Location = "TestLocation2",
                Description = "Test description2",
                ConciergeID = "123testid2"
            }), new CancellationToken());

        _userService.Verify(i => i.GetUserNameAsync(null), Times.Never);
    }
};