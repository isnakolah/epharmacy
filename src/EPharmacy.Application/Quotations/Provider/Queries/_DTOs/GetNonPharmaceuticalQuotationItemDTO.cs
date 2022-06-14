using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Provider.Queries.DTOs;

public class GetNonPharmaceuticalQuotationItemDTO : IMapFrom<NonPharmaceuticalQuotationItem>
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public double UnitPrice { get; set; }

    public int Quantity { get; set; }

    public double Total { get; private set; }

    public string Notes { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<NonPharmaceuticalQuotationItem, GetNonPharmaceuticalQuotationItemDTO>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.NonPharmaceuticalItem.Name))
            .ForMember(dest => dest.Total,
                opt => opt.MapFrom(src => src.Total - src.Markup));
    }
}