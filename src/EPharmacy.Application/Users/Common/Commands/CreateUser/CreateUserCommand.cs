using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Queries.DTOs;

namespace EPharmacy.Application.Users.Common.Commands.CreateUser;

[Authorize(Roles = CREATE_USER_PERMISSIONS)]
public record CreateUserCommand(CreateUserDTO User) : IRequestWrapper<ApplicationUserDTO>;

public class CreateUserCommandHandler : IRequestHandlerWrapper<CreateUserCommand, ApplicationUserDTO>
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public CreateUserCommandHandler(IUserService userService, IRoleService roleService)
    {
        (_userService, _roleService) = (userService, roleService);
    }

    public async Task<ServiceResult<ApplicationUserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.User;

        var currentUserRole = await _roleService.GetCurrentUserMainRoleAsync();

        if (currentUserRole == PHARMACY_AGENT)
            throw new ForbiddenAccessException();

        var createdUserRole = GetCreatedUserRole(currentUserRole);

        var userID = await _userService.CreateUserAddToRoleAsync(user, createdUserRole);

        var result = ServiceResult.Success(
                new ApplicationUserDTO(userID, user.FullName.Trim(), user.Email.Trim(), user.PhoneNumber.Trim(), user.Gender, createdUserRole));

        return result;
    }

    private static string GetCreatedUserRole(in string currentUserRole)
    {
        return currentUserRole switch
        {
            SYSTEM_ADMIN => CONCIERGE_AGENT,
            CONCIERGE_AGENT => PHARMACY_ADMIN,
            _ => PHARMACY_AGENT,
        };
    }
}