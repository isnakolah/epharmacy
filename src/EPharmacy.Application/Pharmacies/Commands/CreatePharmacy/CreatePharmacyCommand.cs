using AutoMapper;
using EPharmacy.Application.Pharmacies.Commands.DTOs;
using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Pharmacies.Commands.CreatePharmacy;

[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record CreatePharmacyCommand(CreatePharmacyDTO Pharmacy) : IRequestWrapper;

public class CreatePharmacyCommandHandler : IRequestHandlerWrapper<CreatePharmacyCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreatePharmacyCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        (_context, _mapper) = (context, mapper);
    }

    public async Task<ServiceResult> Handle(CreatePharmacyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Pharmacies
                .Where(pharm => pharm.ConciergeID == request.Pharmacy.ConciergeID)
                .FirstOrDefaultAsync(cancellationToken);

        var newPharmacy = request.Pharmacy;

        if (entity is not null)
            UpdateCurrentPharmacy(entity, newPharmacy);
        else
            entity = CreateNewPharmacy(newPharmacy);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }

    private Pharmacy CreateNewPharmacy(CreatePharmacyDTO newPharmacy)
    {
        var entity = newPharmacy.MapToEntity(_mapper);

        _context.Pharmacies.Add(entity);

        return entity;
    }

    private static void UpdateCurrentPharmacy(Pharmacy entity, CreatePharmacyDTO newPharmacy)
    {
        entity.Name = newPharmacy.Name;
        entity.Location = newPharmacy.Location;
        entity.Description = newPharmacy.Description;
    }
}