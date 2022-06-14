using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Dashboards.Provider.Queries.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Dashboards.Provider.Queries.GetSummary;

[Authorize(Roles = PHARMACY_AGENT_PERMISSIONS)]
public record GetSummaryQuery : IRequestWrapper<GetSummaryDto>;

public class GetSummaryQueryHandler : IRequestHandlerWrapper<GetSummaryQuery, GetSummaryDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfigurationProvider _config;
    private readonly ICurrentUserPharmacy _currentUserPharmacy;

    public GetSummaryQueryHandler(IApplicationDbContext context, IConfigurationProvider config, ICurrentUserPharmacy currentUserPharmacy)
    {
        (_context, _config, _currentUserPharmacy) = (context, config, currentUserPharmacy);
    }

    public async Task<ServiceResult<GetSummaryDto>> Handle(GetSummaryQuery request, CancellationToken cancellationToken)
    {
        var pharmacyID = await _currentUserPharmacy.GetIDAsync();

        var summary = await _context.Pharmacies
            .Where(pharm => pharm.ID == pharmacyID)
            .ProjectTo<GetSummaryDto>(_config)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return ServiceResult.Success(summary);
    }
}