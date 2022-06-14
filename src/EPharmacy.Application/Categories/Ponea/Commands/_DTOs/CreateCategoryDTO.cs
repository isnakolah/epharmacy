using AutoMapper;
using EPharmacy.Application.Common.Mappings;
using EPharmacy.Domain.Entities;

namespace EPharmacy.Application.Categories.Ponea.Commands.DTOs;

public class CreateCategoryDTO : IMapTo<Category>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCategoryDTO, Category>();
    }

    public Category MapToEntity(IMapper mapper)
    {
        return mapper.Map<Category>(this);
    }
}
