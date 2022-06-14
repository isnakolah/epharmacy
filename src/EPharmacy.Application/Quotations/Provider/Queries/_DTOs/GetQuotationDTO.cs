using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;
using EPharmacy.Domain.Enums;

namespace EPharmacy.Application.Quotations.Provider.Queries.DTOs;

public class GetQuotationDTO : IMapFrom<Quotation>
{
    public Guid ID { get; set; }

    public string CreatedOn { get; set; }

    public int NoToQuote { get; set; }

    public int NoQuoted { get; set; }

    public string Status { get; set; }

    public double DeliveryFee { get; set; }

    public string DeliveryLocation { get; set; }

    public double Total { get; set; }

    public string WorkOrderID { get; set; }

    public Guid PrescriptionID { get; set; }

    public List<GetPharmaceuticalQuotationItemDTO> PharmaceuticalQuotationItems { get; set; }

    public List<GetNonPharmaceuticalQuotationItemDTO> NonPharmaceuticalQuotationItems { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Quotation, GetQuotationDTO>()
            .ForMember(dest => dest.NoToQuote,
                opt => opt.MapFrom(
                    src => src.PharmacyPrescription.Prescription.PharmaceuticalItems.Count + src.PharmacyPrescription.Prescription.NonPharmaceuticalItems.Count))
            .ForMember(dest => dest.NoQuoted,
                opt => opt.MapFrom(src => src.PharmaceuticalQuotationItems.Count + src.NonPharmaceuticalQuotationItems.Count))
            .ForMember(dest => dest.PrescriptionID,
                opt => opt.MapFrom(src => src.PharmacyPrescription.Prescription.ID))
            .ForMember(dest => dest.CreatedOn,
                opt => opt.MapFrom(src => src.CreatedOn.ToShortDateString()))
            .ForMember(dest => dest.Total,
                opt => opt.MapFrom(src => src.Total - src.Markup))
            .ForMember(dest => dest.DeliveryLocation,
                opt => opt.MapFrom(src => src.PharmacyPrescription.Prescription.DeliveryLocation))
            .ForMember(dest => dest.WorkOrderID,
                opt => opt.MapFrom(src => src.WorkOrder != null ? src.WorkOrder.ID.ToString() : string.Empty))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src =>
                    src.Status == QuotationStatus.APPROVED ? "BID WON"
                    : src.Status == QuotationStatus.REJECTED ? "BID LOST"
                    : src.Status.ToString()));
    }
}