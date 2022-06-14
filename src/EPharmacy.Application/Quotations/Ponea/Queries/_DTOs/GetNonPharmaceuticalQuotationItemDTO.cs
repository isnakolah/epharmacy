using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Ponea.Queries.DTOs;

public class GetNonPharmaceuticalQuotationItemDTO : IMapFrom<NonPharmaceuticalQuotationItem>
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public string Category { get; set; }

    public double UnitPrice { get; set; }

    public int Quantity { get; set; }

    public double Markup { get; set; }

    public double Total { get; private set; }

    public string Notes { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<NonPharmaceuticalQuotationItem, GetNonPharmaceuticalQuotationItemDTO>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.NonPharmaceuticalItem.Name))
            .ForMember(src => src.Category,
                opt => opt.MapFrom(src => src.NonPharmaceuticalItem.Category.Name));
    }
}