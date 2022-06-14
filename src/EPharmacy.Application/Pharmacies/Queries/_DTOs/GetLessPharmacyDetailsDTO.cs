using AutoMapper;

using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Pharmacies.Queries.DTOs;

/// <summary>
/// GetLessPharmacyDetailsDTO with only the ID and Name of the pharmacy
/// </summary>
public class GetLessPharmacyDetailsDTO : IMapFrom<Pharmacy>
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public string ConciergeID { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Pharmacy, GetLessPharmacyDetailsDTO>();
    }
}
