using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Pharmacies.Queries.DTOs;

public class GetPharmacyIDDTO : IMapFrom<Pharmacy>
{
    public string ConciergeID { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Pharmacy, GetPharmacyIDDTO>();
    }
};
