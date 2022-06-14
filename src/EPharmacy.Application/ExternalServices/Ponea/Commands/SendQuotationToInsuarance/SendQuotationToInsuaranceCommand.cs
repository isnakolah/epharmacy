using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Quotations.Ponea.Queries.DTOs;
using EPharmacy.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace EPharmacy.Application.ExternalServices.Ponea.Commands.SendQuotationToInsuarance;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public sealed record SendQuotationToInsuaranceCommand(Guid QuotationID) : IRequestWrapper;

public sealed class SendQuotationToInsuaranceCommandHandler : IRequestHandlerWrapper<SendQuotationToInsuaranceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfigurationProvider _mapperConfig;
    private readonly IHttpClientHandler _client;

    public SendQuotationToInsuaranceCommandHandler(IApplicationDbContext context, IConfigurationProvider mapperConfig, IHttpClientHandler client)
    {
        (_context, _client, _mapperConfig) = (context, client, mapperConfig);
    }


    public async Task<ServiceResult> Handle(SendQuotationToInsuaranceCommand request, CancellationToken cancellationToken)
    {
        var endpointUri = "http://ponea.ngrok.io/medisure/api/quote_verify";

        var authentication = new AuthenticationHeaderValue("No auth");

        var body = await _context.Quotations
            .Where(quote => quote.ID == request.QuotationID)
            .ProjectTo<GetQuotationWithItemsDTO>(_mapperConfig)
            .FirstOrDefaultAsync(cancellationToken);

        await _client.GenericRequest<GetQuotationWithItemsDTO, ServiceResult>(
            nameof(SendQuotationToInsuaranceCommand)[..^7], endpointUri, authentication, cancellationToken, MethodType.Post, body);

        return ServiceResult.Success();
    }
}
