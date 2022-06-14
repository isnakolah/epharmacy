using AutoMapper;

using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Pharmacies.Queries.DTOs;

/// <summary>
/// DTO class for getting a Pharmacy
/// </summary>
public class GetPharmacyDTO : IMapFrom<Pharmacy>
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Pharmacy, GetPharmacyDTO>();
    }
}