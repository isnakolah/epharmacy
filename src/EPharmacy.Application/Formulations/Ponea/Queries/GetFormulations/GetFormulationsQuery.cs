using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Common.Constants;
using EPharmacy.Application.Formulations.Ponea.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EPharmacy.Application.Formulations.Ponea.Queries.GetFormulations;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record GetFormulationsQuery : IRequestWrapper<IEnumerable<GetFormulationDTO>>;

public class GetFormulationsQueryHandler : IRequestHandlerWrapper<GetFormulationsQuery, IEnumerable<GetFormulationDTO>>
{
    public readonly IConfigurationProvider _mapperConfig;
    public readonly IApplicationDbContext _context;
    public readonly IMemoryCache _memoryCache;

    public GetFormulationsQueryHandler(IApplicationDbContext context, IConfigurationProvider mapperConfig, IMemoryCache memoryCache)
    {
        (_context, _mapperConfig, _memoryCache) = (context, mapperConfig, memoryCache);
    }

    public async Task<ServiceResult<IEnumerable<GetFormulationDTO>>> Handle(GetFormulationsQuery request, CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue(CacheKeys.FORMULATIONS, out IEnumerable<GetFormulationDTO> cachedFormulations))
            return ServiceResult.Success(cachedFormulations);

        var formulations = _context.Formulations
            .ProjectTo<GetFormulationDTO>(_mapperConfig)
            .AsNoTracking()
            .AsEnumerable();

        return await Task.FromResult(ServiceResult.Success(formulations));
    }
}
