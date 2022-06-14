using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Categories.Ponea.Queries.DTOs;

public class GetCategoryDTO : IMapFrom<Category>
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, GetCategoryDTO>();
    }
}
