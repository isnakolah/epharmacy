using AutoMapper;
using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Provider.Commands.DTOs.Resolvers;

public class NonPharmaceuticalItemResolver : IValueResolver<CreateNonPharmaceuticalQuotationItemDTO, NonPharmaceuticalQuotationItem, NonPharmaceuticalItem>
{
    private readonly IApplicationDbContext _context;

    public NonPharmaceuticalItemResolver(IApplicationDbContext context)
    {
        _context = context;
    }

    public NonPharmaceuticalItem Resolve(CreateNonPharmaceuticalQuotationItemDTO source, NonPharmaceuticalQuotationItem destination, NonPharmaceuticalItem destMember, ResolutionContext context)
    {
        var nonPharmaceuticalItem = _context.NonPharmaceuticalItems.Find(source.NonPharmaceuticalItemID);

        if (nonPharmaceuticalItem is null)
            throw new NotFoundException(nameof(_context.NonPharmaceuticalItems), source.NonPharmaceuticalItemID);

        return nonPharmaceuticalItem;
    }
}
