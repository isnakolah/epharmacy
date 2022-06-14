using EPharmacy.Application.Identity.Common.Queries.DTOs;

namespace EPharmacy.Application.Identity.Provider.Queries.GetSinglePharmacyUser;

[Authorize(Roles = PHARMACY_AGENT_ADMIN_PERMISSIONS)]
public record GetSinglePharmacyUserQuery(Guid ID) : IRequestWrapper<ApplicationUserDTO>;

public class GetSinglePharmacyUserQueryHandler : IRequestHandlerWrapper<GetSinglePharmacyUserQuery, ApplicationUserDTO>
{
    private readonly IPharmacyUserService _pharmacyUserService;

    public GetSinglePharmacyUserQueryHandler(IPharmacyUserService pharmacyUserService)
    {
        _pharmacyUserService = pharmacyUserService;
    }

    public async Task<ServiceResult<ApplicationUserDTO>> Handle(GetSinglePharmacyUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _pharmacyUserService.GetSinglePharmacyUserAsync(request.ID);

        return ServiceResult.Success(user);
    }
}