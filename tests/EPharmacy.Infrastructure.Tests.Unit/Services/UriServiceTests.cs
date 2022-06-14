using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.Common.Models;
using FluentAssertions;
using Xunit;

namespace EPharmacy.Infrastructure.Tests.Unit.Services;

public class UriServiceTests
{
    private static readonly Uri _baseUri = new("https://www.test.com/");

    private readonly UriService _sut;

    public UriServiceTests()
    {
        _sut = new(_baseUri);
    }

    [Fact]
    public void GetPageUri_ShouldReturnUri_WhenSingleQueryParamProvided()
    {
        // Arrange
        var PageNumber = 1;
        var PageSize = 10;
        var From = new DateTime(2020, 12, 12);
        var To = new DateTime(2020, 12, 20);
        var DateOnly = false;
        var filter = new PaginationFilter(PageNumber, PageSize, From, To, DateOnly);
        var queryParam = new QueryParam("search", "test");

        var expectedResult = _baseUri.AddQueryParameters(new()
        {
            new(nameof(PageNumber), PageNumber.ToString()),
            new(nameof(PageSize), PageSize.ToString()),
            new(nameof(From), From.ToString()),
            new(nameof(To), To.ToString()),
            new(nameof(DateOnly), DateOnly.ToString().ToLower()),
            new("search", "test")
        });

        // Act
        var result = _sut.GetPageUri(filter, queryParam);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void GetPageUri_ShouldReturnUri_WhenSingleQueryParamProvidedInQueryList()
    {
        // Arrange
        var PageNumber = 1;
        var PageSize = 10;
        var From = new DateTime(2020, 12, 12);
        var To = new DateTime(2020, 12, 20);
        var DateOnly = false;
        var filter = new PaginationFilter(PageNumber, PageSize, From, To, DateOnly);
        var queryParam = new QueryParam("search", "test");

        var expectedResult = _baseUri.AddQueryParameters(new()
        {
            new(nameof(PageNumber), PageNumber.ToString()),
            new(nameof(PageSize), PageSize.ToString()),
            new(nameof(From), From.ToString()),
            new(nameof(To), To.ToString()),
            new(nameof(DateOnly), DateOnly.ToString().ToLower()),
            new("search", "test")
        });

        // Act
        var result = _sut.GetPageUri(filter, new List<QueryParam>() { queryParam });

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void GetPageUri_ShouldReturnUri_WhenMultipleQueryParamProvidedInQueryList()
    {
        // Arrange
        var PageNumber = 1;
        var PageSize = 10;
        var From = new DateTime(2020, 12, 12);
        var To = new DateTime(2020, 12, 20);
        var DateOnly = false;
        var filter = new PaginationFilter(PageNumber, PageSize, From, To, DateOnly);
        var searchQueryParam = new QueryParam("search", "test");
        var noLimitQueryParam = new QueryParam("noLimit", "true");

        var expectedResult = _baseUri.AddQueryParameters(new()
        {
            new(nameof(PageNumber), PageNumber.ToString()),
            new(nameof(PageSize), PageSize.ToString()),
            new(nameof(From), From.ToString()),
            new(nameof(To), To.ToString()),
            new(nameof(DateOnly), DateOnly.ToString().ToLower()),
            new("search", "test"),
            new("noLimit", "true")
        });

        // Act
        var result = _sut.GetPageUri(filter, new List<QueryParam>() { searchQueryParam, noLimitQueryParam });

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void GetPageUri_ShouldReturnUri_WhenQueryListIsNull()
    {
        // Arrange
        var PageNumber = 1;
        var PageSize = 10;
        var From = new DateTime(2020, 12, 12);
        var To = new DateTime(2020, 12, 20);
        var DateOnly = false;
        var filter = new PaginationFilter(PageNumber, PageSize, From, To, DateOnly);

        var expectedResult = _baseUri.AddQueryParameters(new()
        {
            new(nameof(PageNumber), PageNumber.ToString()),
            new(nameof(PageSize), PageSize.ToString()),
            new(nameof(From), From.ToString()),
            new(nameof(To), To.ToString()),
            new(nameof(DateOnly), DateOnly.ToString().ToLower())
        });

        // Act
        var result = _sut.GetPageUri(filter, new List<QueryParam>());

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void GetUri_ShouldReturnUri_WhenSingleQueryParam()
    {
        // Arrange
        var queryParam = new QueryParam("search", "test");
        var expectedResult = new Uri(_baseUri.ToString() + "?search=test");

        // Act
        var result = _sut.GetUri(queryParam);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void GetIDURI_ShouldReturnUri_WhenIDProvided()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedResult = new Uri(_baseUri.ToString() + id.ToString());

        // Act
        var result = _sut.GetIDUri(id);

        // Assert
        result.Should().Be(expectedResult);
    }
}
