using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Application.Quotations.Provider.Commands.DTOs.Resolvers;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Provider.Commands.DTOs;

public class CreateNonPharmaceuticalQuotationItemDTO : IMapTo<NonPharmaceuticalQuotationItem>
{
    public Guid NonPharmaceuticalItemID { get; set; }

    public double Amount { get; set; }

    public int Quantity { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateNonPharmaceuticalQuotationItemDTO, NonPharmaceuticalQuotationItem>()
            .ForMember(dest => dest.UnitPrice,
                opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.NonPharmaceuticalItem,
                opt => opt.MapFrom<NonPharmaceuticalItemResolver>());
    }

    public NonPharmaceuticalQuotationItem MapToEntity(IMapper mapper)
    {
        return mapper.Map<NonPharmaceuticalQuotationItem>(this);
    }
}