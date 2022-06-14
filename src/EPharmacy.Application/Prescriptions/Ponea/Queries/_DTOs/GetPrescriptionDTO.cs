using AutoMapper;

using EPharmacy.Application.Common.Mappings;
using EPharmacy.Application.Patients.Queries.DTOs;
using EPharmacy.Domain.Entities;
using EPharmacy.Domain.Enums;

namespace EPharmacy.Application.Prescriptions.Ponea.Queries.DTOs;

/// <summary>
/// GetPrescriptionDTO is the dto model class for Prescription model
/// </summary>
public class GetPrescriptionDTO : IMapFrom<Prescription>
{
    public Guid ID { get; set; }
    public string Status { get; set; }

    public string CreatedOn { get; set; }
    public int NoOfItems { get; set; }

    public string DeliveryLocation { get; set; }

    public GetLessPatientDetailsDTO Patient { get; set; }
    public List<GetPrescriptionPharmacyDTO> Pharmacies { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Prescription, GetPrescriptionDTO>()
            .ForMember(dest => dest.NoOfItems,
                opt => opt.MapFrom(src => src.PharmaceuticalItems.Count + src.NonPharmaceuticalItems.Count))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src =>
                    src.Status == PrescriptionStatus.QUOTING && src.PharmacyPrescriptions
                        .Where(pharmPresc => pharmPresc.Expiry < DateTime.UtcNow.AddHours(3) && pharmPresc.Quotation == null).Count() == src.PharmacyPrescriptions.Count
                    ? "EXPIRED"
                    : src.Status.ToString()))
            .ForMember(dest => dest.Pharmacies,
                opt => opt.MapFrom(src => src.PharmacyPrescriptions));
    }
}
