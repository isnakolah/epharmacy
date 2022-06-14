using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Pharmacies.Queries.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Pharmacies.Queries.GetAllPharmacyIDs;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record GetAllPharmacyIDsQuery : IRequestWrapper<List<string>>;

public class GetAllPharmacyIDsQueryHandler : IRequestHandlerWrapper<GetAllPharmacyIDsQuery, List<string>>
{
    public readonly IApplicationDbContext _context;
    public readonly IConfigurationProvider _mapperConfig;

    public GetAllPharmacyIDsQueryHandler(IApplicationDbContext context, IConfigurationProvider mapperConfig)
    {
        (_context, _mapperConfig) = (context, mapperConfig);
    }

    public async Task<ServiceResult<List<string>>> Handle(GetAllPharmacyIDsQuery request, CancellationToken cancellationToken)
    {
        var pharmacyIDs = await _context.Pharmacies
            .ProjectTo<GetPharmacyIDDTO>(_mapperConfig)
            .ToListAsync(cancellationToken);

        var arrayPharmacyIDs = new List<string>(pharmacyIDs.Count);

        pharmacyIDs.ForEach(pharm => arrayPharmacyIDs.Add(pharm.ConciergeID));

        return ServiceResult.Success(arrayPharmacyIDs);
    }
}
