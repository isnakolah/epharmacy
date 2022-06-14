using AutoMapper;
using EPharmacy.Application.Common.Constants;
using EPharmacy.Application.Formulations.Ponea.Commands.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace EPharmacy.Application.Formulations.Ponea.Commands.CreateFormulation;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record CreateFormulationCommand(CreateFormulationDTO Formulation) : IRequestWrapper;

public class CreateFormulationCommandHandler : IRequestHandlerWrapper<CreateFormulationCommand>
{
    public readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;

    public CreateFormulationCommandHandler(IApplicationDbContext context, IMapper mapper, IMemoryCache memoryCache)
    {
        (_context, _mapper, _memoryCache) = (context, mapper, memoryCache);
    }

    public async Task<ServiceResult> Handle(CreateFormulationCommand request, CancellationToken cancellationToken)
    {
        var formulation = request.Formulation.MapToEntity(_mapper);

        _context.Formulations.Add(formulation);

        await _context.SaveChangesAsync(cancellationToken);

        _memoryCache.Remove(CacheKeys.FORMULATIONS);

        return ServiceResult.Success();
    }
}
