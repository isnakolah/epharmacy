using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.Pharmacies.Queries.DTOs;

namespace EPharmacy.Application.Pharmacies.Queries.GetPharmacyByID;

/// <summary>
/// GetPharamcyByIDQuery to get a pharmacy by ID from uri
/// </summary>
[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record GetPharmacyByIDQuery(Guid ID) : IRequestWrapper<GetPharmacyDTO>;

public class GetPharmacyByIDQueryHandler : IRequestHandlerWrapper<GetPharmacyByIDQuery, GetPharmacyDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfigurationProvider _mapperConfig;

    public GetPharmacyByIDQueryHandler(IApplicationDbContext context, IConfigurationProvider mapperConfig)
    {
        (_mapperConfig, _context) = (mapperConfig, context);
    }

    public async Task<ServiceResult<GetPharmacyDTO>> Handle(GetPharmacyByIDQuery request, CancellationToken cancellationToken)
    {
        var pharmacy = await _context.Pharmacies
            .ProjectTo<GetPharmacyDTO>(_mapperConfig)
            .Where(x => x.ID == request.ID)
            .FirstOrErrorAsync(request.ID, cancellationToken);

        return ServiceResult.Success(pharmacy);
    }
}