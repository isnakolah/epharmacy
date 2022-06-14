using EPharmacy.Application.Prescriptions.Provider.Queries.DTOs;
using EPharmacy.Application.Prescriptions.Provider.Queries.GetPaginatedPrescriptions;

namespace EPharmacy.RESTApi.Controllers.v1.Provider;

public class PrescriptionController : BaseApiController
{
    [HttpGet]
    [Route(Routes.Provider.Prescription.GetAll)]
    public async Task<ActionResult<PaginatedServiceResult<GetPrescriptionDTO>>> GetProviderPrescriptions([FromQuery] PaginationFilter paginationFilter)
    {
        return await Mediator.Send(new GetPaginatedPrescriptions(paginationFilter));
    }
}