using AutoMapper;

using EPharmacy.Application.Common.Mappings;
using EPharmacy.Application.Patients.Queries.DTOs;
using EPharmacy.Application.Pharmacies.Queries.DTOs;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Ponea.Queries.DTOs;

public class GetQuotationDTO : IMapFrom<Quotation>
{
    public Guid ID { get; set; }

    public DateTime CreatedOn { get; set; }

    public int NoQuoted { get; set; }

    public int NoToQuote { get; set; }

    public string Status { get; set; }

    public double DeliveryFee { get; set; }

    public double Total { get; set; }

    public Guid PrescriptionID { get; set; }

    public GetLessPatientDetailsDTO Patient { get; set; }

    public GetLessPharmacyDetailsDTO Pharmacy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Quotation, GetQuotationDTO>()
            .ForMember(dest => dest.NoQuoted,
                opt => opt.MapFrom(src =>
                    src.PharmaceuticalQuotationItems.Count + src.NonPharmaceuticalQuotationItems.Count))
            .ForMember(dest => dest.NoToQuote,
                opt => opt.MapFrom(src =>
                src.PharmacyPrescription.Prescription.PharmaceuticalItems.Count + src.PharmacyPrescription.Prescription.NonPharmaceuticalItems.Count))
            .ForMember(dest => dest.PrescriptionID,
                opt => opt.MapFrom(src => src.PharmacyPrescription.Prescription.ID))
            .ForMember(dest => dest.Pharmacy,
                opt => opt.MapFrom(src => src.PharmacyPrescription.Pharmacy))
            .ForMember(dest => dest.Patient,
                opt => opt.MapFrom(src => src.PharmacyPrescription.Prescription.Patient));
    }
}