using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Patients.Commands.DTOs;

public class CreateConciergePatientDTO : IMapTo<Patient>
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Gender { get; set; }

    public DateTime DOB { get; set; }

    public string ConciergeID { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateConciergePatientDTO, Patient>();
    }

    public Patient MapToEntity(IMapper mapper)
    {
        return mapper.Map<Patient>(this);
    }
}