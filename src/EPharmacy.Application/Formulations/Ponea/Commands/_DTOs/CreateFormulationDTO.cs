using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Formulations.Ponea.Commands.DTOs;

public sealed record class CreateFormulationDTO : IMapTo<Formulation>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateFormulationDTO, Formulation>();
    }

    public Formulation MapToEntity(IMapper mapper)
    {
        return mapper.Map<Formulation>(this);
    }
}