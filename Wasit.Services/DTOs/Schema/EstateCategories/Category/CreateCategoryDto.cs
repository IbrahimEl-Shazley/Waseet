using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Wasit.Core.Enums;
using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.EstateCategories.Category
{
    public class CreateCategoryDto : LookupCreateDTO
    {
        [Required]
        public CategoryType Type { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
