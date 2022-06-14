using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.Quotations.Provider.Queries.DTOs;

namespace EPharmacy.Application.Quotations.Provider.Queries.GetQuotationByID;

[Authorize(Roles = PHARMACY_AGENT_PERMISSIONS)]
public record GetQuotationByIDQuery(Guid QuotationID) : IRequestWrapper<GetQuotationDTO>;

public class GetQuotationByIDQueryHandler : IRequestHandlerWrapper<GetQuotationByIDQuery, GetQuotationDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfigurationProvider _mapperConfig;

    public GetQuotationByIDQueryHandler(IApplicationDbContext context, IConfigurationProvider mapperConfig)
    {
        (_context, _mapperConfig) = (context, mapperConfig);
    }

    public async Task<ServiceResult<GetQuotationDTO>> Handle(GetQuotationByIDQuery request, CancellationToken cancellationToken)
    {
        var quotation = await _context.Quotations
            .Where(quote => quote.ID == request.QuotationID)
            .ProjectTo<GetQuotationDTO>(_mapperConfig)
            .FirstOrErrorAsync(request.QuotationID, cancellationToken);

        return ServiceResult.Success(quotation);
    }
}
