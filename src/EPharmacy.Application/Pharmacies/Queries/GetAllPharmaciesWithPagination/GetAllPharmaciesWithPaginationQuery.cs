using EPharmacy.Application.Pharmacies.Queries.DTOs;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Pharmacies.Queries.GetAllPharmaciesWithPagination;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record GetAllPharmaciesWithPaginationQuery(PaginationFilter Filter) : IRequestPaginatedWrapper<GetPharmacyDTO>;

public class GetAllPharmaciesWithPaginationQueryHandler : IRequestHandlerPaginatedWrapper<GetAllPharmaciesWithPaginationQuery, GetPharmacyDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginate _paginate;

    public GetAllPharmaciesWithPaginationQueryHandler(IApplicationDbContext context, IPaginate paginate)
    {
        (_context, _paginate) = (context, paginate);
    }

    public async Task<PaginatedServiceResult<GetPharmacyDTO>> Handle(GetAllPharmaciesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Pharmacies.OrderByDescending(pharm => pharm.CreatedOn);

        var response = await _paginate.CreateAsync<Pharmacy, GetPharmacyDTO>(queryable, request.Filter, cancellationToken);

        return response;
    }
}