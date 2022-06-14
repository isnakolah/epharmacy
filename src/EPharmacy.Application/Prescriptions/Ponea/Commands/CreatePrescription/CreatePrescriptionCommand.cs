using AutoMapper;
using EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs;
using EPharmacy.Domain.Entities;
using EPharmacy.Domain.Events;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.CreatePrescription;

/// <summary>
/// Command to create a prescription and sent to pharmacies
/// </summary>
[Authorize(Roles = CONCIERGE_AGENT_PERMISSIONS)]
public record CreatePrescriptionCommand(CreatePatientPrescriptionDTO PatientPrescription) : IRequestWrapper;

public class CreatePrescriptionAndSendToPharmaciesCommandHandler : IRequestHandlerWrapper<CreatePrescriptionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreatePrescriptionAndSendToPharmaciesCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        (_mapper, _context) = (mapper, context);
    }

    public async Task<ServiceResult> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = _mapper.Map<Prescription>(request.PatientPrescription);

        prescription.DomainEvents.Add(new PrescriptionCreatedEvent(prescription));

        await _context.Prescriptions.AddAsync(prescription, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }
}