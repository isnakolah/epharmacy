using MediatR;

namespace EPharmacy.Application.Users.Provider.Commands.MakeAgentAdmin;

[Authorize(Roles = PHARMACY_ADMIN_PERMISSIONS)]
public record MakeAgentAdminCommand(Guid ID, bool MakeAdmin) : IRequest<ServiceResult>;

public class MakeAgentAdminCommandHandler : IRequestHandler<MakeAgentAdminCommand, ServiceResult>
{
    private readonly IRoleService _roleService;

    public MakeAgentAdminCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<ServiceResult> Handle(MakeAgentAdminCommand request, CancellationToken cancellationToken)
    {
        if (!request.MakeAdmin)
        {
            await _roleService.RemoveFromRoleAsync(request.ID.ToString(), PHARMACY_AGENT_ADMIN);

            return ServiceResult.Success();
        }

        await _roleService.AddToRoleAsync(request.ID.ToString(), PHARMACY_AGENT_ADMIN);

        return ServiceResult.Success();
    }
}
