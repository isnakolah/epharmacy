using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Application.Quotations.Ponea.Queries.DTOs;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Ponea.Queries.DTOs;

/// <summary>
/// Create a map for the Pharmacy prescription
/// </summary>
public class GetPrescriptionPharmacyDTO : IMapFrom<PharmacyPrescription>
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public DateTime Expiry { get; set; }

    public string PrescriptionStatus { get; set; }

    public GetPharmacyQuotationDTO Quotation { get; set; }

    /// <summary>
    /// Mapping profile 
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PharmacyPrescription, GetPrescriptionPharmacyDTO>()
            .ForMember(dest => dest.ID,
                opt => opt.MapFrom(src => src.Pharmacy.ID))
            // Check if prescription is expired
            .ForMember(dest => dest.PrescriptionStatus,
                opt => opt.MapFrom(src =>
                    src.Quotation != null
                    ? src.Quotation.Status.ToString()
                    : src.Quotation == null
                        && src.Expiry < DateTime.UtcNow.AddHours(3)
                        && src.Prescription.Status != Domain.Enums.PrescriptionStatus.CANCELLED
                    ? "EXPIRED"
                    : src.Quotation == null
                        && src.Expiry > DateTime.UtcNow.AddHours(3)
                        && src.Prescription.Status != Domain.Enums.PrescriptionStatus.CANCELLED
                    ? Domain.Enums.PrescriptionStatus.QUOTING.ToString()
                    : src.Prescription.Status.ToString()))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Pharmacy.Name));
    }
}
