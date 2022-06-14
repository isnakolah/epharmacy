using AutoMapper;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs.Resolvers;

internal class CategoryResolver : IValueResolver<CreateNonPharmaceuticalItemDTO, NonPharmaceuticalItem, Category>
{
    private readonly IApplicationDbContext _context;

    public CategoryResolver(IApplicationDbContext context)
    {
        _context = context;
    }

    public Category Resolve(CreateNonPharmaceuticalItemDTO source, NonPharmaceuticalItem destination, Category destMember, ResolutionContext context)
    {
        var category = _context.Categories.FindOrError(source.CategoryID);

        return category;
    }
}
