using Wasit.Core.Entities.EstateCategories;
using Wasit.Services.DTOs.Schema.EstateCategories.Category;

namespace Wasit.Services.Interfaces.Generic.Shared
{
    public interface ICategoriesService : IBaseService<Category, CategoryDto, CreateCategoryDto, UpdateCategoryDto>
    {
        
    }
}
