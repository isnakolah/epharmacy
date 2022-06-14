using AutoMapper;

using EPharmacy.Application.Common.Extensions;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs.Resolvers;

internal class PharmacyPrescriptionResolver : IValueResolver<CreatePatientPrescriptionDTO, Prescription, ICollection<PharmacyPrescription>>
{
    private readonly IExpiryService _expiryService;
    private readonly IApplicationDbContext _context;

    public PharmacyPrescriptionResolver(IExpiryService expiryService, IApplicationDbContext context)
    {
        _expiryService = expiryService;
        _context = context;
    }

    public ICollection<PharmacyPrescription> Resolve(CreatePatientPrescriptionDTO source, Prescription destination, ICollection<PharmacyPrescription> destMember, ResolutionContext context)
    {
        var pharmacyPrescriptions = source.PharmacyIDs.Select(pharmacyID =>
        {
            var pharmacyPrescription = new PharmacyPrescription
            {
                Pharmacy = _context.Pharmacies.FindOrError(pharmacyID),
                Expiry = _expiryService.GetQuotationExpiry
            };

            return pharmacyPrescription;
        });

        return pharmacyPrescriptions.ToArray();
    }
}
