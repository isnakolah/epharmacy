using AutoMapper;
using EPharmacy.Application.Categories.Ponea.Commands.DTOs;
using EPharmacy.Application.Common.Constants;
using EPharmacy.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace EPharmacy.Application.Categories.Ponea.Commands.CreateCategory;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record CreateCategoryCommand(CreateCategoryDTO Category) : IRequest<Result>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IApplicationDbContext context, IMapper mapper, IMemoryCache memoryCache)
    {
        (_context, _mapper, _memoryCache) = (context, mapper, memoryCache);
    }

    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = _mapper.Map<Category>(request.Category);

        _context.Categories.Add(newCategory);

        await _context.SaveChangesAsync(cancellationToken);

        _memoryCache.Remove(CacheKeys.CATEGORIES);

        return Result.Success();
    }
}
