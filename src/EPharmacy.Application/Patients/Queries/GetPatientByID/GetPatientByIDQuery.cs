using EPharmacy.Application.Common.Extensions;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Patients.Queries.GetPatientByID;

public record GetPatientByIDQuery(Guid ID) : IRequestWrapper<Patient>;

public class GetPatientByIDQueryHandler : IRequestHandlerWrapper<GetPatientByIDQuery, Patient>
{
    private readonly IApplicationDbContext _context;

    public GetPatientByIDQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<Patient>> Handle(GetPatientByIDQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Patients.FindOrErrorAsync(request.ID);

        return ServiceResult.Success(entity);
    }
}