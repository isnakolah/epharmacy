using AutoMapper;

using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Provider.Queries.DTOs;

public class GetNonPharmaceuticalItemDTO : IMapFrom<NonPharmaceuticalItem>
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public int Quantity { get; set; }

    public string Category { get; set; }

    public string Notes { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<NonPharmaceuticalItem, GetNonPharmaceuticalItemDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
    }
}