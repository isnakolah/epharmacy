using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Ponea.Queries.DTOs;

public class GetPharmacyQuotationDTO : IMapFrom<Quotation>
{
    public Guid ID { get; set; }

    public int NoOfItems { get; set; }

    public double Total { get; set; }

    public string Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Quotation, GetPharmacyQuotationDTO>()
            .ForMember(dest => dest.NoOfItems, opt => opt.MapFrom(
                src => src.PharmaceuticalQuotationItems.Count + src.NonPharmaceuticalQuotationItems.Count));
    }
}