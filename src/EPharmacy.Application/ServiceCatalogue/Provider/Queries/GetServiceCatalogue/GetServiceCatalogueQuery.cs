using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.ExternalServices.Ponea.Queries.GetPharmacyServiceCatalogueFromConcierge;
using EPharmacy.Application.ServiceCatalogue.Provider.Queries.DTOs;
using MediatR;

namespace EPharmacy.Application.ServiceCatalogue.Provider.Queries.GetServiceCatalogue;

[Authorize(Roles = PHARMACY_AGENT_PERMISSIONS)]
public sealed record class GetServiceCatalogueQuery : IRequestWrapper<GetPharmacyServiceCatalogueDTO[]>;

public sealed class GetServiceCatalogueQueryHandler : IRequestHandlerWrapper<GetServiceCatalogueQuery, GetPharmacyServiceCatalogueDTO[]>
{
    private readonly ICurrentUserPharmacy _currentUserPharmacy;
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public GetServiceCatalogueQueryHandler(IApplicationDbContext context, IMediator mediator, ICurrentUserPharmacy currentUserPharmacy)
    {
        (_context, _mediator, _currentUserPharmacy) = (context, mediator, currentUserPharmacy);
    }

    public async Task<ServiceResult<GetPharmacyServiceCatalogueDTO[]>> Handle(GetServiceCatalogueQuery request, CancellationToken cancellationToken)
    {
        var pharmacyID = await _currentUserPharmacy.GetIDAsync();

        var pharmacyConciergeID = await _context.Pharmacies
            .Where(pharm => pharm.ID == pharmacyID)
            .Select(pharm => pharm.ConciergeID)
            .FirstOrErrorAsync(pharmacyID, cancellationToken);

        try
        {
            var serviceCatalogue = await _mediator.Send(new GetPharmacyServiceCatalogueFromConciergeQuery(pharmacyConciergeID), cancellationToken);

            return serviceCatalogue;
        }
        catch (HttpException)
        {
            throw new ServiceTemporarilyUnavailableException();
        }
    }
}
