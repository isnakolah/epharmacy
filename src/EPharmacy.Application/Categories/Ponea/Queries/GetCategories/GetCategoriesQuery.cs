using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Categories.Ponea.Queries.DTOs;
using EPharmacy.Application.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EPharmacy.Application.Categories.Ponea.Queries.GetCategories;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record GetCategoriesQuery : IRequestWrapper<GetCategoryDTO[]>;

public class GetCategoriesQueryHandler : IRequestHandlerWrapper<GetCategoriesQuery, GetCategoryDTO[]>
{
    private readonly IConfigurationProvider _mapperConfig;
    private readonly IApplicationDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public GetCategoriesQueryHandler(IApplicationDbContext context, IConfigurationProvider mapperConfig, IMemoryCache memoryCache)
    {
        (_context, _mapperConfig, _memoryCache) = (context, mapperConfig, memoryCache);
    }

    public async Task<ServiceResult<GetCategoryDTO[]>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue(CacheKeys.CATEGORIES, out GetCategoryDTO[] cachedCategories))
            return ServiceResult.Success(cachedCategories);

        var categories = await _context.Categories
            .ProjectTo<GetCategoryDTO>(_mapperConfig)
            .ToArrayAsync(cancellationToken);

        return ServiceResult.Success(categories);
    }
}