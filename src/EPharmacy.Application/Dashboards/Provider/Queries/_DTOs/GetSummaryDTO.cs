using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;
using EPharmacy.Domain.Enums;

namespace EPharmacy.Application.Dashboards.Provider.Queries.DTOs;

public record class GetSummaryDto : IMapFrom<Pharmacy>
{
    public int NoOfPrescriptions { get; init; }

    public int NoOfQuotations { get; init; }

    public int NoOfWonQuotations { get; init; }

    public int NoOfLostQuotations { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Pharmacy, GetSummaryDto>()
            .ForMember(dest => dest.NoOfPrescriptions,
                opt => opt.MapFrom(src =>
                    src.PharmacyPrescriptions
                        .Count(pharmPresc =>
                            pharmPresc.Expiry > DateTime.UtcNow.AddHours(3)
                            && pharmPresc.Prescription.Status != PrescriptionStatus.CANCELLED
                            && pharmPresc.Quotation == null)))
            .ForMember(dest => dest.NoOfQuotations,
                opt => opt.MapFrom(src =>
                    src.PharmacyPrescriptions
                        .Count(pharmPresc => pharmPresc.Quotation != null)))
            .ForMember(dest => dest.NoOfWonQuotations,
                opt => opt.MapFrom(src =>
                    src.PharmacyPrescriptions
                        .Count(pharmPresc =>
                            pharmPresc.Quotation != null
                            && pharmPresc.Quotation.Status == QuotationStatus.APPROVED)))
            .ForMember(dest => dest.NoOfLostQuotations,
                opt => opt.MapFrom(src =>
                    src.PharmacyPrescriptions
                        .Count(pharmPresc =>
                                pharmPresc.Quotation != null
                            && pharmPresc.Quotation.Status == QuotationStatus.REJECTED)));
    }
}