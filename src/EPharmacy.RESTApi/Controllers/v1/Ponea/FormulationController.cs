using EPharmacy.Application.Formulations.Ponea.Commands.CreateFormulation;
using EPharmacy.Application.Formulations.Ponea.Commands.DTOs;
using EPharmacy.Application.Formulations.Ponea.Queries.DTOs;
using EPharmacy.Application.Formulations.Ponea.Queries.GetFormulations;

namespace EPharmacy.RESTApi.Controllers.v1.Ponea;

public class FormulationController : BaseApiController
{
    [HttpGet]
    [Route(Routes.Ponea.Formulation.GetAll)]
    public async Task<ActionResult<ServiceResult<IEnumerable<GetFormulationDTO>>>> GetAllFormulations()
    {
        return await Mediator.Send(new GetFormulationsQuery());
    }

    [HttpPost]
    [Route(Routes.Ponea.Formulation.Create)]
    public async Task<ActionResult<ServiceResult>> CreateFormulation(CreateFormulationDTO formulation)
    {
        return await Mediator.Send(new CreateFormulationCommand(formulation));
    }
}