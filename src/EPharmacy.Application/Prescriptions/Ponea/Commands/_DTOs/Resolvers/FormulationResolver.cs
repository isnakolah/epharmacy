using AutoMapper;

using EPharmacy.Application.Common.Extensions;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs.Resolvers;

internal class FormulationResolver : IValueResolver<CreatePharmaceuticalItemDTO, PharmaceuticalItem, Formulation>
{
    private readonly IApplicationDbContext _context;

    public FormulationResolver(IApplicationDbContext context)
    {
        _context = context;
    }

    public Formulation Resolve(CreatePharmaceuticalItemDTO source, PharmaceuticalItem destination, Formulation destMember, ResolutionContext context)
    {
        var formulation = _context.Formulations.FindOrError(source.FormulationID);

        return formulation;
    }
}
