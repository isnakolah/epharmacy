using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Patients.Queries.DTOs;

public class GetPatientDTO : IMapFrom<Patient>
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime DOB { get; set; }

    public int Age { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Patient, GetPatientDTO>();
    }
}