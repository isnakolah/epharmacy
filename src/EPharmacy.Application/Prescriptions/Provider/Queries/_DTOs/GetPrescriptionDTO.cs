using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Provider.Queries.DTOs;

public record GetPrescriptionDTO : IMapFrom<PharmacyPrescription>
{
    public Guid ID { get; init; }
    public DateTime Expiry { get; init; }
    public int NoOfItems { get; init; }
    public string DeliveryLocation { get; init; } = string.Empty;
    public IEnumerable<string> FileUrls { get; init; } = Array.Empty<string>();
    public IEnumerable<GetPharmaceuticalItemDTO> PharmaceuticalItems { get; init; } = Array.Empty<GetPharmaceuticalItemDTO>();

    public IEnumerable<GetNonPharmaceuticalItemDTO> NonPharmaceuticalItems { get; init; } = Array.Empty<GetNonPharmaceuticalItemDTO>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PharmacyPrescription, GetPrescriptionDTO>()
            .ForMember(src => src.ID,
                opt => opt.MapFrom(dest => dest.Prescription.ID))
            .ForMember(src => src.NoOfItems,
                opt => opt.MapFrom(dest =>
                    dest.Prescription.PharmaceuticalItems.Count + dest.Prescription.NonPharmaceuticalItems!.Count))
            .ForMember(src => src.DeliveryLocation,
                opt => opt.MapFrom(dest => dest.Prescription.DeliveryLocation))
            .ForMember(src => src.PharmaceuticalItems,
                opt => opt.MapFrom(dest => dest.Prescription.PharmaceuticalItems))
            .ForMember(src => src.NonPharmaceuticalItems,
                opt => opt.MapFrom(dest => dest.Prescription.NonPharmaceuticalItems))
            .ForMember(dest => dest.FileUrls, opt => opt.MapFrom(src => src.Prescription.FileUrls));
    }
}
