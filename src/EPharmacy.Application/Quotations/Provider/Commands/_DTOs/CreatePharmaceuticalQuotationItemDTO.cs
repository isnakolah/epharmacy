using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Application.Quotations.Provider.Commands.DTOs.Resolvers;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Provider.Commands.DTOs;

public class CreatePharmaceuticalQuotationItemDTO : IMapTo<PharmaceuticalQuotationItem>
{
    public Guid PharmaceuticalItemID { get; set; }

    public bool Stocked { get; set; }

    public string GenericDrug { get; set; }

    public double Amount { get; set; }

    public int Quantity { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePharmaceuticalQuotationItemDTO, PharmaceuticalQuotationItem>()
            .ForMember(dest => dest.UnitPrice,
                opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.PharmaceuticalItem,
                opt => opt.MapFrom<PharmaceuticalItemResolver>());
    }

    public PharmaceuticalQuotationItem MapToEntity(IMapper mapper)
    {
        return mapper.Map<PharmaceuticalQuotationItem>(this);
    }
}