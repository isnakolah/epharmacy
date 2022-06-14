using EPharmacy.Application.Quotations.Provider.Queries.DTOs;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Provider.Queries.GetPaginatedQuotations;

[Authorize(Roles = PHARMACY_AGENT_PERMISSIONS)]
public record GetPaginatedQuotationsQuery(PaginationFilter PaginationFilter) : IRequestPaginatedWrapper<GetQuotationDTO>;

public class GetPaginatedQuotationQueryQueryHandler : IRequestHandlerPaginatedWrapper<GetPaginatedQuotationsQuery, GetQuotationDTO>
{
    private readonly ICurrentUserPharmacy _currentUserPharmacy;
    private readonly IApplicationDbContext _context;
    private readonly IPaginate _paginate;

    public GetPaginatedQuotationQueryQueryHandler(ICurrentUserPharmacy currentUserPharmacy, IApplicationDbContext context, IPaginate paginate)
    {
        (_currentUserPharmacy, _context, _paginate) = (currentUserPharmacy, context, paginate);
    }

    public async Task<PaginatedServiceResult<GetQuotationDTO>> Handle(GetPaginatedQuotationsQuery request, CancellationToken cancellationToken)
    {
        var pharmacyID = await _currentUserPharmacy.GetIDAsync();

        var quotationsQueryable = _context.Quotations
            .OrderByDescending(quote => quote.CreatedOn)
            .Where(quote => quote.PharmacyPrescription.Pharmacy.ID == pharmacyID);

        var paginatedResult = await _paginate.CreateAsync<Quotation, GetQuotationDTO>(
            quotationsQueryable, request.PaginationFilter, cancellationToken);

        return paginatedResult;
    }
}