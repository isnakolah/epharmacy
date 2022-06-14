using AutoMapper;
using EPharmacy.Application.Common.Models;
using EPharmacy.Infrastructure.Tests.Unit.Services.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace EPharmacy.Application.Tests.Intergration.Services;

public class PaginationServiceTests
{
    private readonly PaginationService _sut;
    private readonly Mock<IDateTime> _dateTimeMock = new();
    private readonly Mock<IUriService> _uriServiceMock = new();

    public PaginationServiceTests()
    {
        var mapperConfiguration = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfileMock());
        });

        var mapper = mapperConfiguration.CreateMapper();

        _sut = new PaginationService(mapper.ConfigurationProvider, _uriServiceMock.Object, _dateTimeMock.Object);
    }

    [Fact(Skip = "Awaitable Queryable issue for morking toArrayAsync")]
    public async Task CreateAsync_ShouldReturnPaginatedResult_WhenDataExists()
    {
        // Arrange
        var testPerson1 = new Person("TestPerson1");

        var queryable = new Person[] { testPerson1 }.OrderBy(person => person.CreatedOn).AsQueryable();

        _dateTimeMock.Setup(x => x.Now).Returns(DateTime.Now);

        _uriServiceMock.Setup(x => x.GetPageUri(new(), new List<QueryParam>())).Returns(new Uri("http://valid.com/uri"));

        // Act 
        var result = await _sut.CreateAsync<Person, GetPersonDTO>(queryable, new(), new());

        // Assert
        result.Data.Should().HaveCount(1);

        result.FirstPage.Should().NotBeNull();
    }
}