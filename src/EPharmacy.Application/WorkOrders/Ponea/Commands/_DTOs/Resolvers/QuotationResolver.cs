using AutoMapper;

using EPharmacy.Application.Common.Extensions;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.WorkOrders.Ponea.Commands.DTOs.Resolvers;

public class QuotationResolver : IValueResolver<CreateWorkOrderDTO, WorkOrder, Quotation>
{
    private readonly IApplicationDbContext _context;

    public QuotationResolver(IApplicationDbContext context)
    {
        _context = context;
    }

    public Quotation Resolve(CreateWorkOrderDTO source, WorkOrder destination, Quotation destMember, ResolutionContext context)
    {
        var quotation = _context.Quotations.FindOrError(source.QuotationID);

        return quotation;
    }
}