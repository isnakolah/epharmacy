using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Patients.Commands.UpdatePatient;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record UpdatePatientCommand(Patient Patient) : IRequestWrapper<Patient>;

public class UpdatePatientCommandHandler : IRequestHandlerWrapper<UpdatePatientCommand, Patient>
{
    private readonly IApplicationDbContext _context;

    public UpdatePatientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<Patient>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var entity = await GetPatientByID(request.Patient, cancellationToken);

        if (entity is null)
            throw new NotFoundException(nameof(entity), request.Patient.ID);

        return ServiceResult.Success(entity);
    }

    private async Task<Patient> GetPatientByID(Patient patient, CancellationToken cancellationToken)
    {
        return await _context.Patients.SingleOrDefaultAsync(x => x.ID == patient.ID, cancellationToken);
    }
}
