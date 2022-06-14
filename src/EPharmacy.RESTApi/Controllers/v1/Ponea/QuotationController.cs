using EPharmacy.Application.Quotations.Ponea.Commands.ApproveQuotation;
using EPharmacy.Application.Quotations.Ponea.Commands.RejectQuotation;
using EPharmacy.Application.Quotations.Ponea.Queries.DTOs;
using EPharmacy.Application.Quotations.Ponea.Queries.GetPaginatedQuotations;
using EPharmacy.Application.Quotations.Ponea.Queries.GetQuotationByID;

namespace EPharmacy.RESTApi.Controllers.v1.Ponea;

public class QuotationController : BaseApiController
{
    [HttpGet]
    [Route(Routes.Ponea.Quotation.GetAll)]
    public async Task<ActionResult<PaginatedServiceResult<GetQuotationDTO>>> GetAllQuotationsForPonea([FromQuery] PaginationFilter paginationFilter)
    {
        return await Mediator.Send(new GetPaginatedQuotations(paginationFilter));
    }

    [HttpGet]
    [Route(Routes.Ponea.Quotation.GetSingleByID)]
    public async Task<ActionResult<ServiceResult<GetQuotationWithItemsDTO>>> GetSingleQuotationForPonea(Guid id)
    {
        return await Mediator.Send(new GetQuotationByIDQuery(id));
    }

    [HttpPost]
    [Route(Routes.Ponea.Quotation.Approve)]
    public async Task<ActionResult<ServiceResult>> ApproveQuotation(Guid id)
    {
        return await Mediator.Send(new ApproveQuotationCommand(id));
    }

    [HttpPost]
    [Route(Routes.Ponea.Quotation.Reject)]
    public async Task<ActionResult<ServiceResult>> RejectQuotation(Guid id)
    {
        return await Mediator.Send(new RejectQuotationCommand(id));
    }
}