using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Patients.Queries.GetConciergePatient;
using EPharmacy.Domain.Entities;
using MediatR;

namespace EPharmacy.Application.Patients.Commands.CreateOrFindConciergePatient;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record CreateOrFindConciergePatientCommand(Patient PatientPrescription) : IRequestWrapper<Patient>;

public class CreateOrFindPatientFromConciergeCommandHandler : IRequestHandlerWrapper<CreateOrFindConciergePatientCommand, Patient>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public CreateOrFindPatientFromConciergeCommandHandler(IApplicationDbContext context, IMediator mediator)
    {
        (_context, _mediator) = (context, mediator);
    }

    public async Task<ServiceResult<Patient>> Handle(CreateOrFindConciergePatientCommand request, CancellationToken cancellationToken)
    {
        var entity = request.PatientPrescription with { Prescriptions = null };

        try
        {
            var findPatient = await _mediator.Send(new GetPatientByConciergeIDQuery(entity.ConciergeID), cancellationToken);

            entity = findPatient.Data;
        }
        catch (NotFoundException)
        {
            _context.Patients.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }

        return ServiceResult.Success(entity);
    }
}
