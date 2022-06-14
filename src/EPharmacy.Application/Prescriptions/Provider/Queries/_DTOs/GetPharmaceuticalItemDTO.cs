using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Provider.Queries.DTOs;

/// <summary>
/// GetPrescriptionItemDTO model class for retrieval of the PrescriptionItem
/// </summary>
public class GetPharmaceuticalItemDTO : IMapFrom<PharmaceuticalItem>
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public string Dosage { get; set; }

    public string Frequency { get; set; }

    public string Duration { get; set; }

    public string Formulation { get; set; }

    public string Notes { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PharmaceuticalItem, GetPharmaceuticalItemDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Drug.Name))
            .ForMember(dest => dest.Formulation, opt => opt.MapFrom(src => src.Formulation.Name));
    }
}