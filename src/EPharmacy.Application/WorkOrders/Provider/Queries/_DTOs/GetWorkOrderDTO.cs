using AutoMapper;

using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.WorkOrders.Provider.Queries.DTOs;

public class GetWorkOrderDTO : IMapFrom<WorkOrder>
{
    public Guid ID { get; set; }

    public string PatientName { get; set; }

    public Guid PrescriptionID { get; set; }

    public string Status { get; set; }

    public string DeliveryDate { get; set; }

    public string DeliveryTime { get; set; }

    public double DeliveryFee { get; set; }

    public double Total { get; set; }

    public int NoOfDeliveryItems { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<WorkOrder, GetWorkOrderDTO>()
            .ForMember(dest => dest.PrescriptionID,
                opt => opt.MapFrom(src => src.Quotation.PharmacyPrescription.Prescription.ID))
            .ForMember(dest => dest.DeliveryFee,
                opt => opt.MapFrom(src => src.Quotation.DeliveryFee))
            .ForMember(dest => dest.Total,
                opt => opt.MapFrom(src => src.Quotation.Total - src.Quotation.DeliveryFee - src.Quotation.Markup))
            .ForMember(dest => dest.DeliveryDate,
                opt => opt.MapFrom(src => src.Quotation.PharmacyPrescription.CreatedOn.ToShortDateString()))
            .ForMember(dest => dest.DeliveryTime,
                opt => opt.MapFrom(src => src.Quotation.PharmacyPrescription.CreatedOn.ToShortTimeString()))
            .ForMember(dest => dest.NoOfDeliveryItems,
                opt => opt.MapFrom(src => src.Quotation.PharmaceuticalQuotationItems.Count + src.Quotation.NonPharmaceuticalQuotationItems.Count))
            .ForMember(dest => dest.PatientName,
                opt => opt.MapFrom(src => src.Quotation.PharmacyPrescription.Prescription.Patient.Name));
    }
}