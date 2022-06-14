using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Patients.Queries.DTOs;

public class GetLessPatientDetailsDTO : IMapFrom<Patient>
{
    public Guid ID { get; set; }

    public string ConciergeID { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Patient, GetLessPatientDetailsDTO>();
    }
}