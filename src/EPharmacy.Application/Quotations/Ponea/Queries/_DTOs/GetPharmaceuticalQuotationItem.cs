using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Ponea.Queries.DTOs;

public class GetPharmaceuticalQuotationItemDTO : IMapFrom<PharmaceuticalQuotationItem>
{
    public Guid ID { get; set; }

    public string Name { get; init; }

    public string Dosage { get; set; }

    public string Frequency { get; set; }

    public string Duration { get; set; }

    public bool Stocked { get; set; }

    public string GenericDrug { get; set; }

    public string Formulation { get; set; }

    public double UnitPrice { get; set; }

    public int Quantity { get; set; }

    public double Markup { get; set; }

    public double Total { get; set; }

    public string Notes { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PharmaceuticalQuotationItem, GetPharmaceuticalQuotationItemDTO>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.PharmaceuticalItem.Drug.Name))
            .ForMember(dest => dest.Dosage,
                opt => opt.MapFrom(src => src.PharmaceuticalItem.Dosage))
            .ForMember(dest => dest.Frequency,
                opt => opt.MapFrom(src => src.PharmaceuticalItem.Frequency))
            .ForMember(dest => dest.Formulation,
                opt => opt.MapFrom(src => src.PharmaceuticalItem.Formulation.Name))
            .ForMember(dest => dest.Duration,
                opt => opt.MapFrom(src => src.PharmaceuticalItem.Duration))
            .ForMember(dest => dest.Notes,
                opt => opt.MapFrom(src => src.PharmaceuticalItem.Notes));
    }
}