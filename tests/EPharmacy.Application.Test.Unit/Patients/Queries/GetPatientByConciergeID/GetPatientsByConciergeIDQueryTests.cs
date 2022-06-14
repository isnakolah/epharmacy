using EPharmacy.Application.Patients.Queries.GetConciergePatient;
using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Tests.Unit.Patients.Queries.GetPatientByConciergeID;

public class GetPatientsByConciergeIDQueryTests
{
    private readonly string _patientConciergeID;
    private readonly GetPatientByConciergeIDQuery _query;
    private readonly GetPatientByConciergeIDQueryHandler _handler;
    private readonly Mock<IApplicationDbContext> _context;

    public GetPatientsByConciergeIDQueryTests()
    {
        _context = new();
        _patientConciergeID = Guid.NewGuid().ToString();
        _query = new(_patientConciergeID);
        _handler = new(_context.Object);
    }

    [Fact(Skip = "Error mocking FirstOrDefaultAsync extension method")]
    public async Task GetPatientByConciergeIDQuery_ShouldReturnPatient_WhenPatientWithProvidedConciergeIDExists()
    {
        // Arrange
        var patient = new Patient { ID = Guid.NewGuid(), ConciergeID = _patientConciergeID };

        _context
            .Setup(x => x.Patients.FirstOrDefaultAsync(x => x.ConciergeID == _patientConciergeID, new()))
            .ReturnsAsync(patient);

        var expectedResult = ServiceResult.Success(patient);

        // Act
        var result = await _handler.Handle(_query, new());

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
