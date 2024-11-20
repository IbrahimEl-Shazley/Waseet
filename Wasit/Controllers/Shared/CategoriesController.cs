using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wasit.Controllers;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Services.DTOs.Schema.EstateCategories.Category;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.API.Controllers.Shared
{
    [ApiExplorerSettings(GroupName = "Shared")]
    public class CategoriesController : BaseController<Category, CategoryDto, CreateCategoryDto, UpdateCategoryDto>
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService) : base(categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [AllowAnonymous]
        [HttpGet("GetList")]
        public override async Task<IActionResult> GetList()
        {
            return _OK(await _categoriesService.GetListAsync(x => x.IsActive));
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public override async Task<IActionResult> Create([FromForm] CreateCategoryDto dto)
        {
            return _OK(await _categoriesService.AddAndCommitAsync(dto), Localize("ItemCreatedSuccessfully"));
        }
       
    }
}
