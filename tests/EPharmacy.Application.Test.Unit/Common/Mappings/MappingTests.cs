using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Application.Pharmacies.Queries.DTOs;
using EPharmacy.Domain.Entities;
using NUnit.Framework;
using System.Runtime.Serialization;

namespace EPharmacy.Application.Tests.Unit.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    [TestCase(typeof(Pharmacy), typeof(GetPharmacyDTO))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type);

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}