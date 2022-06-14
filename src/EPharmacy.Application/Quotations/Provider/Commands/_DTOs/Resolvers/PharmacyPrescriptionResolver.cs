using AutoMapper;
using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Quotations.Provider.Commands.DTOs.Resolvers;

public class PharmacyPrescriptionResolver : IValueResolver<CreateQuotationDTO, Quotation, PharmacyPrescription>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserPharmacy _currentUserPharmacy;

    public PharmacyPrescriptionResolver(IApplicationDbContext context, ICurrentUserPharmacy currentUserPharmacy)
    {
        _context = context;
        _currentUserPharmacy = currentUserPharmacy;
    }

    public PharmacyPrescription Resolve(CreateQuotationDTO source, Quotation destination, PharmacyPrescription destMember, ResolutionContext context)
    {
        var pharmacyID = _currentUserPharmacy.GetIDAsync().Result;

        var pharmacyPrescription = _context.PharmacyPrescriptions
            .Where(pharmPresc => pharmPresc.Prescription.ID == source.PrescriptionID && pharmPresc.Pharmacy.ID == pharmacyID)
            .Include(pharmPresc => pharmPresc.Prescription)
            .Include(pharmPresc => pharmPresc.Quotation)
            .FirstOrDefault();

        if (pharmacyPrescription is null)
            throw new NotFoundException(nameof(_context.PharmacyPrescriptions), source.PrescriptionID);

        return pharmacyPrescription;
    }
}
