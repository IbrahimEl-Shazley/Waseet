using Wasit.Core.Entities.EstateCategories;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.EstateCategories.Category;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.Services.Implementations.Generic.Shared
{
    public class CategoriesService : BaseService<Category, CategoryDto, CreateCategoryDto, UpdateCategoryDto>, ICategoriesService
    {
        public CategoriesService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
        }
    }
}
