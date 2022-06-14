namespace EPharmacy.Application.Identity.Common.Queries.GetCurrentUserRoles;

public sealed record class GetCurrentUserRolesQuery : IRequestWrapper<List<string>>;

public sealed class GetCurrentUserRolesQueryHandler : IRequestHandlerWrapper<GetCurrentUserRolesQuery, List<string>>
{
    private readonly IRoleService _roleService;

    public GetCurrentUserRolesQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<ServiceResult<List<string>>> Handle(GetCurrentUserRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetCurrentUserRolesAsync();

        return ServiceResult.Success(roles);
    }
}