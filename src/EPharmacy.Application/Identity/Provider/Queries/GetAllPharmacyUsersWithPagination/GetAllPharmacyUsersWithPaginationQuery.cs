using EPharmacy.Application.Identity.Common.Queries.DTOs;

namespace EPharmacy.Application.Identity.Provider.Queries.GetAllPharmacyUsersWithPagination;

[Authorize(Roles = PHARMACY_AGENT_ADMIN_PERMISSIONS)]
public record GetAllPharmacyUsersWithPaginationQuery : IRequestWrapper<ApplicationUserDTO[]>;

public class GetAllPharmacyUsersWithPaginationQueryHandler : IRequestHandlerWrapper<GetAllPharmacyUsersWithPaginationQuery, ApplicationUserDTO[]>
{
    private readonly ICurrentUserPharmacy _currentUserPharmacy;
    private readonly IPharmacyUserService _pharmacyUserService;

    public GetAllPharmacyUsersWithPaginationQueryHandler(ICurrentUserPharmacy currentUserPharmacy, IPharmacyUserService pharmacyUserService)
    {
        (_currentUserPharmacy, _pharmacyUserService) = (currentUserPharmacy, pharmacyUserService);
    }

    public async Task<ServiceResult<ApplicationUserDTO[]>> Handle(GetAllPharmacyUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var users = await _pharmacyUserService.GetPharmacyUsersAsync(await _currentUserPharmacy.GetIDAsync());

        return ServiceResult.Success(users);
    }
}
