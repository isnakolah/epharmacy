using AutoMapper;
using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Provider.Commands.DTOs.Resolvers;

public class PharmaceuticalItemResolver : IValueResolver<CreatePharmaceuticalQuotationItemDTO, PharmaceuticalQuotationItem, PharmaceuticalItem>
{
    private readonly IApplicationDbContext _context;

    public PharmaceuticalItemResolver(IApplicationDbContext context)
    {
        _context = context;
    }

    public PharmaceuticalItem Resolve(CreatePharmaceuticalQuotationItemDTO source, PharmaceuticalQuotationItem destination, PharmaceuticalItem destMember, ResolutionContext context)
    {
        var pharmaceuticalItem = _context.PharmaceuticalItems.Find(source.PharmaceuticalItemID);

        if (pharmaceuticalItem is null)
            throw new NotFoundException(nameof(_context.PharmaceuticalItems), source.PharmaceuticalItemID);

        return pharmaceuticalItem;
    }
}
