using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Identity.Commands.DTOs;
using EPharmacy.Application.Identity.Common.Queries.DTOs;
using EPharmacy.Application.Pharmacies.Commands.CreatePharmacy;
using EPharmacy.Application.Users.Common.Commands.CreateUser;
using EPharmacy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Users.Ponea.Commands.CreatePharmacyAdmin;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record CreatePharmacyAdminCommand(CreatePharmacyAdminDTO PharmacyUser) : IRequestWrapper<ApplicationUserDTO>;

public class CreatePharmacyAdminCommandHandler : IRequestHandlerWrapper<CreatePharmacyAdminCommand, ApplicationUserDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public CreatePharmacyAdminCommandHandler(IApplicationDbContext context, IMediator mediator)
    {
        (_context, _mediator) = (context, mediator);
    }

    public async Task<ServiceResult<ApplicationUserDTO>> Handle(CreatePharmacyAdminCommand request, CancellationToken cancellationToken)
    {
        var newPharmacy = request.PharmacyUser.Pharmacy;

        if (await _context.Pharmacies.AnyAsync(pharm => pharm.ConciergeID == newPharmacy.ConciergeID, cancellationToken))
            throw new CustomException("This pharmacy already has a administrator");

        var user = await _mediator.Send(new CreateUserCommand(request.PharmacyUser.User), cancellationToken);

        newPharmacy.Users = new List<PharmacyUser> { new PharmacyUser { ID = Guid.Parse(user.Data.ID) } };

        await _mediator.Send(new CreatePharmacyCommand(newPharmacy), cancellationToken);

        return ServiceResult.Success(user.Data);
    }
}