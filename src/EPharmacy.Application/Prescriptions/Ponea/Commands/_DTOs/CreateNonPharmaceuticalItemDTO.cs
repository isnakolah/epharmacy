using AutoMapper;

using EPharmacy.Application.Common.Mappings;
using EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs.Resolvers;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs;

public record class CreateNonPharmaceuticalItemDTO : IMapTo<NonPharmaceuticalItem>
{
    public string Name { get; init; } = string.Empty;

    public int Quantity { get; init; }

    public string? Notes { get; init; }

    public Guid CategoryID { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateNonPharmaceuticalItemDTO, NonPharmaceuticalItem>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom<CategoryResolver>());
    }

    public NonPharmaceuticalItem MapToEntity(IMapper mapper)
    {
        return mapper.Map<NonPharmaceuticalItem>(this);
    }
}