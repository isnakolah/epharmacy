using EPharmacy.Application.Patients.Commands.CreateOrFindConciergePatient;
using EPharmacy.Application.Patients.Queries.GetConciergePatient;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Tests.Unit.Patients.Commands.CreateOrFindConciergePatient;

public class CreateOrFindConciergePatientQueryTests
{
    private readonly CreateOrFindPatientFromConciergeCommandHandler _sut;
    private readonly CreateOrFindConciergePatientCommand _command;
    private readonly Patient _patient;

    private readonly Mock<IApplicationDbContext> _context;
    private readonly Mock<IMediator> _mediator;

    public CreateOrFindConciergePatientQueryTests()
    {
        _context = new();
        _mediator = new();

        _patient = new()
        {
            ID = Guid.NewGuid(),
            ConciergeID = Guid.NewGuid().ToString()[..7],
            Prescriptions = new List<Prescription>()
            {
                new() { ID = Guid.NewGuid() }
            }
        };
        _command = new(_patient);

        _sut = new(_context.Object, _mediator.Object);
    }

    [Fact]
    public async Task CreateOrFindConciergePatientCommand_ShouldReturnPatient_WhenPatientAlreadyExists()
    {
        // Arrange
        _mediator
            .Setup(x => x.Send(new GetPatientByConciergeIDQuery(_patient.ConciergeID), new()))
            .ReturnsAsync(ServiceResult.Success(_patient));

        var expectedResult = ServiceResult.Success(_patient);

        // Act
        var result = await _sut.Handle(_command, new());

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task CreateOrFindConciergePatientCommand_ShouldNotThrowError_WhenPatientAlreadyExists()
    {
        // Arrange
        _mediator
            .Setup(x => x.Send(new GetPatientByConciergeIDQuery(_patient.ConciergeID), new()))
            .ReturnsAsync(ServiceResult.Success(_patient));

        var expectedResult = ServiceResult.Success(_patient);

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().NotThrowAsync();
    }

    [Fact]
    public async Task CreateOrFindConciergePatientCommand_ShouldSetPrescriptionsToNull_WhenPrescriptionsArePresent()
    {
        // Arrange
        _mediator
            .Setup(x => x.Send(new GetPatientByConciergeIDQuery(_patient.ConciergeID), new()))
            .ReturnsAsync(ServiceResult.Success(_patient));

        var expectedResult = ServiceResult.Success(_patient);

        // Act
        var result = await _sut.Handle(_command, new());

        // Assert
        result.Data.Prescriptions.Should().BeNull();
    }

    [Fact(Skip = "Error mocking dbSet.Add() method")]
    public async Task CreateOrFindConciergePatientCommand_ShouldCreatePatient_WhenNoSuchPatientIsPresentInContext()
    {
        // Arrange
        var patient = new Patient()
        {
            ID = Guid.NewGuid(),
            ConciergeID = _patient.ConciergeID,
            Prescriptions = _patient.Prescriptions
        };

        _mediator
            .Setup(x => x.Send(new GetPatientByConciergeIDQuery(_patient.ConciergeID), new()))
            .ThrowsAsync(new NotFoundException("Patient", _patient.ConciergeID));

        _context.Setup(x => x.Patients.Add(_patient)).Returns(() => patient);

        var expectedResult = ServiceResult.Success(patient);

        // Act
        var result = await _sut.Handle(_command, new());

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task CreateOrFindConciergePatientCommand_ShouldNotThrow_WhenNoSuchPatientIsPresentInContext()
    {
        // Arrange
        var patient = new Patient()
        {
            ID = Guid.NewGuid(),
            ConciergeID = _patient.ConciergeID,
            Prescriptions = _patient.Prescriptions
        };

        _mediator
            .Setup(x => x.Send(new GetPatientByConciergeIDQuery(_patient.ConciergeID), new()))
            .ThrowsAsync(new NotFoundException("Patient", _patient.ConciergeID));

        _context.Setup(x => x.Patients.Add(_patient)).Returns(() => default!);

        // Act
        var result = () => _sut.Handle(_command, new());

        // Assert
        await result.Should().NotThrowAsync();
    }
}