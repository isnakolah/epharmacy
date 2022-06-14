using AutoMapper;

using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Pharmacies.Commands.DTOs;

/// <summary>
/// DTO class for creating a Pharmacy
/// </summary>
public class CreatePharmacyDTO : IMapTo<Pharmacy>
{
    public string ConciergeID { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public List<PharmacyUser> Users { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePharmacyDTO, Pharmacy>();
    }

    public Pharmacy MapToEntity(IMapper mapper)
    {
        return mapper.Map<Pharmacy>(this);
    }
}
