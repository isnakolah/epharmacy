using EPharmacy.Application.Pharmacies.Queries.DTOs;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Pharmacies.Queries.SearchPharmaciesPaginated;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record SearchPharmaciesPaginatedQuery(string Name, PaginationFilter PaginationFilter) : IRequestPaginatedWrapper<GetPharmacyDTO>;

public class SearchPharmaciesPaginatedQueryHandler : IRequestHandlerPaginatedWrapper<SearchPharmaciesPaginatedQuery, GetPharmacyDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginate _paginate;

    public SearchPharmaciesPaginatedQueryHandler(IPaginate paginate, IApplicationDbContext context)
    {
        (_paginate, _context) = (paginate, context);
    }

    public async Task<PaginatedServiceResult<GetPharmacyDTO>> Handle(SearchPharmaciesPaginatedQuery request, CancellationToken cancellationToken)
    {
        var pharmaciesQueryable = _context.Pharmacies
            .Where(pharm => pharm.Name.Contains(request.Name))
            .OrderByDescending(pharm => pharm.CreatedOn);

        var paginatedResult = await _paginate.CreateAsync<Pharmacy, GetPharmacyDTO>(
            pharmaciesQueryable, request.PaginationFilter, cancellationToken, new() { new(nameof(Pharmacy.Name).ToLower(), request.Name) });

        return paginatedResult;
    }
}