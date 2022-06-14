using EPharmacy.Application.ServiceCatalogue.Provider.Commands.ToggleStockedInService;
using EPharmacy.Application.ServiceCatalogue.Provider.Queries.DTOs;
using EPharmacy.Application.ServiceCatalogue.Provider.Queries.GetServiceCatalogue;

namespace EPharmacy.RESTApi.Controllers.v1.Provider;

public class ServiceCatalogue : BaseApiController
{
    [HttpGet]
    [Route(Routes.Provider.ServiceCatalogue.GetAll)]
    public async Task<ActionResult<ServiceResult<GetPharmacyServiceCatalogueDTO[]>>> GetServiceCatalogue()
    {
        return await Mediator.Send(new GetServiceCatalogueQuery());
    }

    [HttpPost]
    [Route(Routes.Provider.ServiceCatalogue.ToggleStocked)]
    public async Task<ActionResult<ServiceResult>> ToggleStockedStatusInService(long serviceID, [FromQuery(Name = "stocked")] bool stocked)
    {
        return await Mediator.Send(new ToggleStockedInServiceCommand(serviceID, stocked));
    }
}
