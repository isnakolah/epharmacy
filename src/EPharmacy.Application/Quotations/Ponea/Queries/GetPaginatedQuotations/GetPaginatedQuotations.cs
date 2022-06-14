using EPharmacy.Application.Quotations.Ponea.Queries.DTOs;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Quotations.Ponea.Queries.GetPaginatedQuotations;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record GetPaginatedQuotations(PaginationFilter PaginationFilter) : IRequestPaginatedWrapper<GetQuotationDTO>;

public class GetAllQuotationsForPoneaWithPaginationQueryHandler : IRequestHandlerPaginatedWrapper<GetPaginatedQuotations, GetQuotationDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginate _paginate;

    public GetAllQuotationsForPoneaWithPaginationQueryHandler(IPaginate paginate, IApplicationDbContext context)
    {
        _context = context;
        _paginate = paginate;
    }

    public async Task<PaginatedServiceResult<GetQuotationDTO>> Handle(GetPaginatedQuotations request, CancellationToken cancellationToken)
    {
        var quotationsQueryable = _context.Quotations.OrderByDescending(quote => quote.CreatedOn);

        var paginatedResult = await _paginate.CreateAsync<Quotation, GetQuotationDTO>(
            quotationsQueryable, request.PaginationFilter, cancellationToken);

        return paginatedResult;
    }
}