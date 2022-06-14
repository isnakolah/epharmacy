using FluentAssertions;
using Moq;
using Xunit;

namespace EPharmacy.Infrastructure.Tests.Unit.Services;

public class ExpiryServiceTests
{
    private readonly ExpiryService _sut;
    private readonly Mock<IDateTime> _dateTimeMock = new();

    public ExpiryServiceTests()
    {
        _sut = new(_dateTimeMock.Object);
    }

    [Fact]
    public void GetQuotationExpiry_ShouldReturnFifteenAddedMinutes()
    {
        // Arrange
        var now = new DateTime(2000, 5, 5, 5, 5, 5);

        _dateTimeMock.Setup(x => x.Now).Returns(now);

        var expectedResult = now.AddMinutes(15);

        // Act
        var result = _sut.GetQuotationExpiry;

        // Assert
        result.Should().Be(expectedResult);
    }
}
