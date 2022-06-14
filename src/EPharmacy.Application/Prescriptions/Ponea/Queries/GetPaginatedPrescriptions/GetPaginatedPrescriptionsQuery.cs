using EPharmacy.Application.Prescriptions.Ponea.Queries.DTOs;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Ponea.Queries.GetPaginatedPrescriptions;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record GetPaginatedPrescriptionsQuery(PaginationFilter Filter) : IRequestPaginatedWrapper<GetPrescriptionDTO>;

public class GetPaginatedPrescriptionsQueryHandler : IRequestHandlerPaginatedWrapper<GetPaginatedPrescriptionsQuery, GetPrescriptionDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginate _paginate;

    public GetPaginatedPrescriptionsQueryHandler(IPaginate paginate, IApplicationDbContext context)
    {
        (_context, _paginate) = (context, paginate);
    }

    public async Task<PaginatedServiceResult<GetPrescriptionDTO>> Handle(GetPaginatedPrescriptionsQuery request, CancellationToken cancellationToken)
    {
        var prescriptionQueryable = _context.Prescriptions
            .OrderByDescending(presc => presc.CreatedOn);
    
        var paginatedResult = await _paginate.CreateAsync<Prescription, GetPrescriptionDTO>(
            prescriptionQueryable, request.Filter, cancellationToken);
    
        return paginatedResult;
    }
}
