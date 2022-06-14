using AutoMapper;

using EPharmacy.Application.Common.Mappings;
using EPharmacy.Application.WorkOrders.Ponea.Commands.DTOs.Resolvers;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.WorkOrders.Ponea.Commands.DTOs;

public class CreateWorkOrderDTO : IMapTo<WorkOrder>
{
    public string ConciergeAppointmentID { get; set; }

    public Guid QuotationID { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateWorkOrderDTO, WorkOrder>()
            .ForMember(dest => dest.Quotation, opt => opt.MapFrom<QuotationResolver>());
    }
}