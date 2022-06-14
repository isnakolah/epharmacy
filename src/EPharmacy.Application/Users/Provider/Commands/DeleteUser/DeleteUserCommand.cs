namespace EPharmacy.Application.Users.Provider.Commands.DeleteUser;

[Authorize(Roles = PHARMACY_AGENT_ADMIN_PERMISSIONS)]
public record DeleteUserCommand(Guid ApplicationUserID) : IRequestWrapper;

public class DeleteUserCommandHandler : IRequestHandlerWrapper<DeleteUserCommand>
{
    private readonly IUserService _userService;
    private readonly IApplicationDbContext _context;

    public DeleteUserCommandHandler(IUserService userService, IApplicationDbContext context)
    {
        (_userService, _context) = (userService, context);
    }

    public async Task<ServiceResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userService.DeleteUserAsync(request.ApplicationUserID.ToString());

        var user = await _context.PharmacyUsers.FindAsync(request.ApplicationUserID);

        _context.PharmacyUsers.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }
}