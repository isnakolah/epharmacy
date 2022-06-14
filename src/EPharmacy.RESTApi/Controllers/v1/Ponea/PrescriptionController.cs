using EPharmacy.Application.ExternalServices.Ponea.Queries.DTOs;
using EPharmacy.Application.ExternalServices.Ponea.Queries.GetDrugsFromDrugIndex;
using EPharmacy.Application.Prescriptions.Ponea.Commands.CancelPrescription;
using EPharmacy.Application.Prescriptions.Ponea.Commands.CreatePrescription;
using EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs;
using EPharmacy.Application.Prescriptions.Ponea.Queries.DTOs;
using EPharmacy.Application.Prescriptions.Ponea.Queries.GetPaginatedPrescriptions;
using EPharmacy.Application.Prescriptions.Ponea.Queries.GetPrescriptionByID;

namespace EPharmacy.RESTApi.Controllers;

public class PrescriptionController : BaseApiController
{
    [HttpGet]
    [Route(Routes.Ponea.Prescription.GetAll)]
    public async Task<ActionResult<PaginatedServiceResult<GetPrescriptionDTO>>> GetAllPrescriptionsWithPagination([FromQuery] PaginationFilter filter)
    {
        return await Mediator.Send(new GetPaginatedPrescriptionsQuery(filter));
    }

    [HttpPost]
    [Route(Routes.Ponea.Prescription.Create)]
    public async Task<ActionResult<ServiceResult>> CreatePrescriptionAndSendToPharmacies([FromBody] CreatePatientPrescriptionDTO patientPrescription)
    {
        return await Mediator.Send(new CreatePrescriptionCommand(patientPrescription));
    }

    [HttpGet]
    [Route(Routes.Ponea.Prescription.GetSingleByID)]
    public async Task<ActionResult<ServiceResult<GetPrescriptionWithItemsDTO>>> GetPrescriptionById(Guid id)
    {
        return await Mediator.Send(new GetPrescriptionByIDQuery(id));
    }

    [HttpPost]
    [Route(Routes.Ponea.Prescription.Cancel)]
    public async Task<ActionResult<ServiceResult>> CancelPrescription(Guid id)
    {
        return await Mediator.Send(new CancelPrescriptionCommand(id));
    }

    [HttpGet]
    [Route(Routes.Ponea.Prescription.SearchDrug)]
    public async Task<ActionResult<ServiceResult<GetDrugFromDrugIndexDto[]>>> SearchDrugInDrugIndex([FromQuery(Name = "name")] string name)
    {
        return await Mediator.Send(new SearchDrugsInDrugIndexQuery(name));
    }
}