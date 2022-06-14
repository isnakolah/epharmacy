using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Application.Prescriptions.Ponea.Queries.DTOs;

namespace EPharmacy.Application.Prescriptions.Ponea.Queries.GetPrescriptionByID;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record GetPrescriptionByIDQuery(Guid ID) : IRequestWrapper<GetPrescriptionWithItemsDTO>;

public class GetPrescriptionByIDQueryHandler : IRequestHandlerWrapper<GetPrescriptionByIDQuery, GetPrescriptionWithItemsDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfigurationProvider _mapperConfig;

    public GetPrescriptionByIDQueryHandler(IApplicationDbContext context, IConfigurationProvider mapperConfig)
    {
        (_context, _mapperConfig) = (context, mapperConfig);
    }

    public async Task<ServiceResult<GetPrescriptionWithItemsDTO>> Handle(GetPrescriptionByIDQuery request, CancellationToken cancellationToken)
    {
        var prescription = await _context.Prescriptions
            .Where(presc => presc.ID == request.ID)
            .ProjectTo<GetPrescriptionWithItemsDTO>(_mapperConfig)
            .FirstOrErrorAsync(request.ID, cancellationToken);

        return ServiceResult.Success(prescription);
    }
}