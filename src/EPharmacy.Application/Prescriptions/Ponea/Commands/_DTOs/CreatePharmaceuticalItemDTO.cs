using AutoMapper;

using EPharmacy.Application.Common.Mappings;
using EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs.Resolvers;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs;

/// <summary>
/// The DTO for creating a PrescriptionItem
/// </summary>
public record class CreatePharmaceuticalItemDTO : IMapTo<PharmaceuticalItem>
{
    public CreateDrugDTO Drug { get; init; }

    public string Dosage { get; init; }

    public Guid FormulationID { get; init; }

    public string Frequency { get; init; }

    public string Duration { get; init; }

    public string Notes { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePharmaceuticalItemDTO, PharmaceuticalItem>()
            .ForMember(dest => dest.Formulation,
                opt => opt.MapFrom<FormulationResolver>())
            .ForMember(dest => dest.Drug,
                opt => opt.MapFrom<DrugResolver>());
    }

    public PharmaceuticalItem MapToEntity(IMapper mapper)
    {
        return mapper.Map<PharmaceuticalItem>(this);
    }
}