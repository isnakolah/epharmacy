using EPharmacy.Application.Categories.Ponea.Commands.CreateCategory;
using EPharmacy.Application.Categories.Ponea.Commands.DTOs;
using EPharmacy.Application.Categories.Ponea.Queries.DTOs;
using EPharmacy.Application.Categories.Ponea.Queries.GetCategories;

namespace EPharmacy.RESTApi.Controllers.v1.Ponea;

public class CategoryController : BaseApiController
{
    [HttpGet]
    [Route(Routes.Ponea.Category.GetAll)]
    public async Task<ActionResult<ServiceResult<GetCategoryDTO[]>>> GetAllCategories()
    {
        return await Mediator.Send(new GetCategoriesQuery());
    }

    [HttpPost]
    [Route(Routes.Ponea.Category.Create)]
    public async Task<ActionResult<Result>> CreateCategory(CreateCategoryDTO newCategory)
    {
        return await Mediator.Send(new CreateCategoryCommand(newCategory));
    }
}