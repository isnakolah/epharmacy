using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace EPharmacy.RESTApi.Controllers;

[Authorize]
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    private ISender? _mediator;

    private protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}