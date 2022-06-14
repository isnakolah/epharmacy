using EPharmacy.Application.Prescriptions.Provider.Queries.DTOs;
using EPharmacy.Domain.Entities;
using EPharmacy.Domain.Enums;

namespace EPharmacy.Application.Prescriptions.Provider.Queries.GetPaginatedPrescriptions;

[Authorize(Roles = PHARMACY_AGENT_PERMISSIONS)]
public record GetPaginatedPrescriptions(PaginationFilter PaginationFilter) : IRequestPaginatedWrapper<GetPrescriptionDTO>;

public class GetPaginatedPrescriptionsHandler : IRequestHandlerPaginatedWrapper<GetPaginatedPrescriptions, GetPrescriptionDTO>
{
    private readonly ICurrentUserPharmacy _currentUserPharmacy;
    private readonly IApplicationDbContext _context;
    private readonly IPaginate _paginate;
    private readonly IDateTime _dateTime;

    public GetPaginatedPrescriptionsHandler(ICurrentUserPharmacy currentUserPharmacy, IApplicationDbContext context, IPaginate paginate, IDateTime dateTime)
    {
        (_currentUserPharmacy, _dateTime, _paginate, _context) = (currentUserPharmacy, dateTime, paginate, context);
    }

    public async Task<PaginatedServiceResult<GetPrescriptionDTO>> Handle(
        GetPaginatedPrescriptions request,
        CancellationToken cancellationToken)
    {
        var pharmacyID = await _currentUserPharmacy.GetIDAsync();

        var quotationsQueryable = _context.PharmacyPrescriptions
            .OrderBy(pharmPresc => pharmPresc.Expiry)
            .Where(pharmPresc =>
                pharmPresc.Expiry > _dateTime.Now
                && pharmPresc.Pharmacy.ID == pharmacyID
                && pharmPresc.Quotation == null
                && pharmPresc.Prescription.Status != PrescriptionStatus.CANCELLED);

        var paginatedResult = await _paginate.CreateAsync<PharmacyPrescription, GetPrescriptionDTO>(
            quotationsQueryable, request.PaginationFilter, cancellationToken);

        return paginatedResult;
    }
}