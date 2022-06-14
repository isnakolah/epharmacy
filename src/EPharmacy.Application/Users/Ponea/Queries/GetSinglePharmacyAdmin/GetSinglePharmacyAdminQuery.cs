using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Identity.Ponea.Queries._DTOs;
using EPharmacy.Application.Pharmacies.Queries.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Users.Ponea.Queries.GetSinglePharmacyAdmin;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record GetSinglePharmacyAdminQuery(Guid PharmacyAdminID) : IRequestWrapper<ApplicationUserWithPharmacyDTO>;

public class GetSinglePharmacyAdminQueryHandler : IRequestHandlerWrapper<GetSinglePharmacyAdminQuery, ApplicationUserWithPharmacyDTO>
{
    private readonly IConfigurationProvider _mapperConfig;
    private readonly IApplicationDbContext _context;
    private readonly IUserService _userService;

    public GetSinglePharmacyAdminQueryHandler(IUserService userService, IApplicationDbContext context, IConfigurationProvider mapperConfig)
    {
        (_userService, _context, _mapperConfig) = (userService, context, mapperConfig);
    }

    public async Task<ServiceResult<ApplicationUserWithPharmacyDTO>> Handle(GetSinglePharmacyAdminQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByID(request.PharmacyAdminID);

        var pharmacy = await _context.Pharmacies
            .Where(pharm => pharm.Users.Any(x => x.ID == Guid.Parse(user.ID)))
            .ProjectTo<GetPharmacyDTO>(_mapperConfig)
            .FirstOrDefaultAsync(cancellationToken);

        var response = new ApplicationUserWithPharmacyDTO(user, pharmacy);

        return ServiceResult.Success(response);
    }
}