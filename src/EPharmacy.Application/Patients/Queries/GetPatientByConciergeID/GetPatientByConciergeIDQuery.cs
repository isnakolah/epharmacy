using EPharmacy.Application.Common.Extensions;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Patients.Queries.GetConciergePatient;

public record GetPatientByConciergeIDQuery(string ConciergeID) : IRequestWrapper<Patient>;

public class GetPatientByConciergeIDQueryHandler : IRequestHandlerWrapper<GetPatientByConciergeIDQuery, Patient>
{
    private readonly IApplicationDbContext _context;

    public GetPatientByConciergeIDQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<Patient>> Handle(GetPatientByConciergeIDQuery request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .FirstOrErrorAsync(patient => patient.ConciergeID == request.ConciergeID, request.ConciergeID, cancellationToken);

        return ServiceResult.Success(patient);
    }
}