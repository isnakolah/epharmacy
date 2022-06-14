using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Formulations.Ponea.Queries.DTOs;

public record class GetFormulationDTO() : IMapFrom<Formulation>
{
    public Guid ID { get; init; }

    public string Name { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Formulation, GetFormulationDTO>();
    }
}
