using EPharmacy.Application.Quotations.Provider.Commands.CreateQuotation;
using EPharmacy.Application.Quotations.Provider.Commands.DTOs;
using EPharmacy.Application.Quotations.Provider.Queries.DTOs;
using EPharmacy.Application.Quotations.Provider.Queries.GetPaginatedQuotations;
using EPharmacy.Application.Quotations.Provider.Queries.GetQuotationByID;

namespace EPharmacy.RESTApi.Controllers.v1.Provider;
public class QuotationController : BaseApiController
{
    [HttpGet]
    [Route(Routes.Provider.Quotation.GetSingleByID)]
    public async Task<ActionResult<ServiceResult<GetQuotationDTO>>> GetSingleQuotationForPharmacy(Guid id)
    {
        return await Mediator.Send(new GetQuotationByIDQuery(id));
    }

    [HttpGet]
    [Route(Routes.Provider.Quotation.GetAll)]
    public async Task<ActionResult<PaginatedServiceResult<GetQuotationDTO>>> GetAllQuotedQuotationsForPharmacy([FromQuery] PaginationFilter paginationFilter)
    {
        return await Mediator.Send(new GetPaginatedQuotationsQuery(paginationFilter));
    }

    [HttpPost]
    [Route(Routes.Provider.Quotation.Create)]
    public async Task<ActionResult<ServiceResult>> CreateQuotationFromPharmacy([FromBody] CreateQuotationDTO newQuotation)
    {
        return await Mediator.Send(new CreateQuotationCommand(newQuotation));
    }
}