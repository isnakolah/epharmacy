using AutoMapper;

using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs.Resolvers;

internal class PatientResolver : IValueResolver<CreatePatientPrescriptionDTO, Prescription, Patient>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PatientResolver(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Patient Resolve(CreatePatientPrescriptionDTO source, Prescription destination, Patient destMember, ResolutionContext context)
    {
        try
        {
            var patient = _context.Patients
                .Where(patient => patient.ConciergeID == source.Patient.ConciergeID)
                .FirstOrError(Guid.Empty);

            return patient;
        }
        catch (NotFoundException)
        {

            return _mapper.Map<Patient>(source.Patient);
        }
    }
}
