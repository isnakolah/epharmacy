using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.ExternalServices.Ponea.Commands.ToggleServiceStockedStatu;
using MediatR;

namespace EPharmacy.Application.ServiceCatalogue.Provider.Commands.ToggleStockedInService;

[Authorize(Roles = PHARMACY_AGENT_PERMISSIONS)]
public record class ToggleStockedInServiceCommand(long ServiceID, bool Stocked) : IRequestWrapper;

public class ToggleStockedInServiceCommandHandler : IRequestHandlerWrapper<ToggleStockedInServiceCommand>
{
    private readonly IMediator _mediator;

    public ToggleStockedInServiceCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ServiceResult> Handle(ToggleStockedInServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _mediator.Send(new ToggleServiceStockedStatusCommand(request.ServiceID, request.Stocked), cancellationToken);

            return response;
        }
        catch (HttpException)
        {
            throw new ServiceTemporarilyUnavailableException();
        }
    }
}
