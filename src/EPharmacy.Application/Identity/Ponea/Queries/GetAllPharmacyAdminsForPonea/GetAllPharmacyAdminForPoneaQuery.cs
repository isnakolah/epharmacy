using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Identity.Ponea.Queries._DTOs;
using EPharmacy.Application.Pharmacies.Queries.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Identity.Ponea.Queries.GetAllPharmacyAdminsForPonea;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record GetAllPharmacyAdminsForPoneaQuery : IRequestWrapper<List<ApplicationUserWithPharmacyDTO>>;

public class GetAllPharmacyAdminsForPoneaQueryHandler : IRequestHandlerWrapper<GetAllPharmacyAdminsForPoneaQuery, List<ApplicationUserWithPharmacyDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPharmacyUserService _pharmacyUserService;
    private readonly IConfigurationProvider _mapperConfig;

    public GetAllPharmacyAdminsForPoneaQueryHandler(IApplicationDbContext context, IPharmacyUserService pharmacyUserService, IConfigurationProvider mapperConfig)
    {
        (_context, _pharmacyUserService, _mapperConfig) = (context, pharmacyUserService, mapperConfig);
    }

    public async Task<ServiceResult<List<ApplicationUserWithPharmacyDTO>>> Handle(GetAllPharmacyAdminsForPoneaQuery request, CancellationToken cancellationToken)
    {
        var response = new List<ApplicationUserWithPharmacyDTO>();

        var users = await _pharmacyUserService.GetAllPharmacyAdminUsersAsync();

        foreach (var user in users)
        {
            var pharmacy = await _context.Pharmacies
                .Where(pharm => pharm.Users.Any(x => x.ID == Guid.Parse(user.ID)))
                .ProjectTo<GetPharmacyDTO>(_mapperConfig)
                .FirstOrDefaultAsync(cancellationToken);

            response.Add(new ApplicationUserWithPharmacyDTO(user, pharmacy));
        }

        return ServiceResult.Success(response);
    }
}