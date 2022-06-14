using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.Quotations.Ponea.Queries.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Quotations.Ponea.Queries.GetQuotationByID;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record GetQuotationByIDQuery(Guid ID) : IRequestWrapper<GetQuotationWithItemsDTO>;

public class GetQuotationByIDQueryHandler : IRequestHandlerWrapper<GetQuotationByIDQuery, GetQuotationWithItemsDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfigurationProvider _mapperConfig;

    public GetQuotationByIDQueryHandler(IApplicationDbContext context, IConfigurationProvider mapperConfig)
    {
        (_context, _mapperConfig) = (context, mapperConfig);
    }

    public async Task<ServiceResult<GetQuotationWithItemsDTO>> Handle(GetQuotationByIDQuery request, CancellationToken cancellationToken)
    {
        var quotation = await _context.Quotations
            .Where(quote => quote.ID == request.ID)
            .AsNoTracking()
            .ProjectTo<GetQuotationWithItemsDTO>(_mapperConfig)
            .FirstOrErrorAsync(request.ID, cancellationToken);

        return ServiceResult.Success(quotation);
    }
}