using EPharmacy.Application.Pharmacies.Queries.DTOs;
using EPharmacy.Application.Pharmacies.Queries.GetAllPharmaciesWithPagination;
using EPharmacy.Application.Pharmacies.Queries.GetAllPharmacyIDs;
using EPharmacy.Application.Pharmacies.Queries.GetPharmacyByID;
using EPharmacy.Application.Pharmacies.Queries.SearchPharmaciesPaginated;

namespace EPharmacy.RESTApi.Controllers;

public class PharmacyController : BaseApiController
{
    [HttpGet]
    [Route(Routes.Ponea.Pharmacy.GetAll)]
    public async Task<ActionResult<PaginatedServiceResult<GetPharmacyDTO>>> GetAllPharmaciesWithPagination([FromQuery] PaginationFilter filter)
    {
        return await Mediator.Send(new GetAllPharmaciesWithPaginationQuery(filter));
    }

    [HttpGet]
    [Route(Routes.Ponea.Pharmacy.GetSingleByID)]
    public async Task<ActionResult<ServiceResult<GetPharmacyDTO>>> GetPharmacyByID(Guid id)
    {
        return await Mediator.Send(new GetPharmacyByIDQuery(id));
    }

    [HttpGet]
    [Route(Routes.Ponea.Pharmacy.SearchByName)]
    public async Task<ActionResult<PaginatedServiceResult<GetPharmacyDTO>>> SearchPharmaciesByNameWithPagination([FromQuery(Name = "name")] string name, [FromQuery] PaginationFilter paginationFilter)
    {
        return await Mediator.Send(new SearchPharmaciesPaginatedQuery(name, paginationFilter));
    }

    [HttpGet]
    [Route(Routes.Ponea.Pharmacy.GetAllPharmacyIDs)]
    public async Task<ActionResult<ServiceResult<List<string>>>> GetPharmacyIDsHashTable()
    {
        return await Mediator.Send(new GetAllPharmacyIDsQuery());
    }
}