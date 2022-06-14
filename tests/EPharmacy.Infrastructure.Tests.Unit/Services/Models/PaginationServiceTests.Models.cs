using AutoMapper;
using EPharmacy.Domain.Common;
using System;

namespace EPharmacy.Infrastructure.Tests.Unit.Services.Models;

public record class Person : AuditableEntity
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public Person()
    {
    }

    public Person(string name)
    {
        ID = Guid.NewGuid();
        Name = name;
        CreatedOn = DateTime.Now;
    }
}

public class GetPersonDTO
{
    public Guid ID { get; set; }

    public string Name { get; set; }
}

public class MappingProfileMock : Profile
{
    public MappingProfileMock()
    {
        CreateMap<Person, GetPersonDTO>();
    }
}