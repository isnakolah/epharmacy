using AutoMapper;
using EPharmacy.Application.Common.Exceptions;
using EPharmacy.Application.Common.Extensions;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs.Resolvers;

internal class DrugResolver : IValueResolver<CreatePharmaceuticalItemDTO, PharmaceuticalItem, Drug>
{
    private readonly IApplicationDbContext _context;

    public DrugResolver(IApplicationDbContext context)
    {
        _context = context;
    }

    public Drug Resolve(CreatePharmaceuticalItemDTO source, PharmaceuticalItem destination, Drug destMember, ResolutionContext context)
    {
        try
        {
            // If drug does not have a manufacturer, then it is not in the drug index
            if (string.IsNullOrWhiteSpace(source.Drug.Manufacturer))
                throw new NotFoundException();

            var drug = _context.Drugs
                .Where(drug => drug.Name == source.Drug.Name && drug.Manufacturer == source.Drug.Manufacturer)
                .FirstOrError(Guid.Empty);

            return drug;
        }
        catch (NotFoundException)
        {
            var newDrug = new Drug
            {
                Name = source.Drug.Name,
                Manufacturer = source.Drug.Manufacturer,
            };

            return newDrug;
        }
    }
}
