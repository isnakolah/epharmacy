using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Application.Quotations.Provider.Commands.DTOs.Resolvers;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Provider.Commands.DTOs;

public class CreateQuotationDTO : IMapTo<Quotation>
{
    public Guid PrescriptionID { get; set; }

    public List<CreatePharmaceuticalQuotationItemDTO> PharmaceuticalQuotationItems { get; set; }

    public List<CreateNonPharmaceuticalQuotationItemDTO> NonPharmaceuticalQuotationItems { get; set; }

    public decimal DeliveryFee { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateQuotationDTO, Quotation>()
            .ForMember(dest => dest.PharmacyPrescription,
                opt => opt.MapFrom<PharmacyPrescriptionResolver>())
            .ForMember(dest => dest.NoQuoted, opt =>
                opt.MapFrom(src =>
                    src.NonPharmaceuticalQuotationItems.Count + src.PharmaceuticalQuotationItems.Count));
    }

    public Quotation MapToEntity(IMapper mapper)
    {
        return mapper.Map<Quotation>(this);
    }
}