using EPharmacy.Application.Identity.Commands.DTOs;
using MediatR;

namespace EPharmacy.Application.Users.Common.Commands.UpdateUser;

[Authorize(Roles = CREATE_USER_PERMISSIONS)]
public record UpdateUserCommand(CreateUserDTO UpdatedUser, Guid UserID) : IRequest<ServiceResult>;

public class UpdatedUserCommandHandler : IRequestHandler<UpdateUserCommand, ServiceResult>
{
    private readonly IUserService _userService;

    public UpdatedUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ServiceResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _userService.UpdateUserAsync(request.UpdatedUser, request.UserID.ToString());

        return ServiceResult.Success();
    }
}