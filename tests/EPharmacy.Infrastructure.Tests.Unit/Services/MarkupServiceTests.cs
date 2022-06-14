using FluentAssertions;
using Xunit;

namespace EPharmacy.Infrastructure.Tests.Unit.Services;

public class MarkupServiceTests
{
    private readonly MarkupService _sut;

    public MarkupServiceTests()
    {
        _sut = new();
    }

    [Fact]
    public void CalculateNonPharmaceuticalQuotationItemMarkup_ShouldReturnTwentyPercentMarkup_WhenValidTotalProvided()
    {
        // Arrange
        var total = 200.0;
        var percentageMarkup = 0.20;
        var expectedResult = total * percentageMarkup;

        // Act
        var result = _sut.CalculateNonPharmaceuticalQuotationItemMarkup(total);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void CalculatePharmaceuticalQuotationItemMarkup_ShouldReturnFifteenPercentMarkup_WhenValidTotalProvided()
    {
        // Arrange
        var total = 200.0;
        var percentageMarkup = 0.15;
        var expectedResult = total * percentageMarkup;

        // Act
        var result = _sut.CalculatePharmaceuticalQuotationItemMarkup(total);

        // Assert
        result.Should().Be(expectedResult);
    }
}
