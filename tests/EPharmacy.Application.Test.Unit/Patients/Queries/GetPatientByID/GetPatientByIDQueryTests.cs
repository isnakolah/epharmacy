using EPharmacy.Application.Patients.Queries.GetPatientByID;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Tests.Unit.Patients.Queries.GetPatientByID;

public class GetPatientByIDQueryTests
{
    private readonly Guid _patientID;
    private readonly GetPatientByIDQuery _query;
    private readonly GetPatientByIDQueryHandler _handler;
    private readonly Mock<IApplicationDbContext> _context;

    public GetPatientByIDQueryTests()
    {
        _context = new();
        _patientID = Guid.NewGuid();
        _query = new(_patientID);
        _handler = new(_context.Object);
    }

    [Fact]
    public async Task GetPatientByIDQuery_ReturnsPatient_WhenPatientWithProvidedIDExists()
    {
        // Arrange
        var patient = new Patient { ID = _patientID, ConciergeID = "Very strong ID" };

        _context.Setup(x => x.Patients.FindAsync(_patientID)).ReturnsAsync(patient);

        var expectedResult = ServiceResult.Success(patient);

        // Act
        var result = await _handler.Handle(_query, new());

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task GetPatientByIDQuery_ThrowsNotFoundException_WhenNoPatientWithProvidedIDExists()
    {
        // Arrange
        _context.Setup(x => x.Patients.FindAsync(_patientID)).ReturnsAsync(() => null);

        // Assert
        var result = () => _handler.Handle(_query, new());

        // Act
        await result.Should().ThrowExactlyAsync<NotFoundException>("Patients", _patientID);
    }
}
