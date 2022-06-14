using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Queries.DTOs;
using EPharmacy.Application.Users.Common.Commands.CreateUser;
using EPharmacy.Domain.Entities;
using MediatR;

namespace EPharmacy.Application.Users.Provider.Commands.CreateAgent;

[Authorize(Roles = CREATE_PHARMACY_USER_PERMISSIONS)]
public record CreateAgentCommand(CreateUserDTO User) : IRequestWrapper<ApplicationUserDTO>;

public class CreateAgentCommandHandler : IRequestHandlerWrapper<CreateAgentCommand, ApplicationUserDTO>
{
    private readonly ICurrentUserPharmacy _currentUserPharmacy;
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public CreateAgentCommandHandler(IMediator mediator, IApplicationDbContext context, ICurrentUserPharmacy currentUserPharmacy)
    {
        (_mediator, _context, _currentUserPharmacy) = (mediator, context, currentUserPharmacy);
    }

    public async Task<ServiceResult<ApplicationUserDTO>> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
    {
        var pharmacy = await _context.Pharmacies.FindAsync(await _currentUserPharmacy.GetIDAsync());

        var userData = await _mediator.Send(new CreateUserCommand(request.User), cancellationToken);

        _context.PharmacyUsers.Add(new() { ID = Guid.Parse(userData.Data.ID), Pharmacy = pharmacy });

        await _context.SaveChangesAsync(cancellationToken);

        return userData;
    }
}