using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Wasit.Core.Enums;

namespace Wasit.Services.ViewModels.EstateSettings
{
    public class CreateSpecificationViewModel
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "يجب ارفاق ايقونة للخاصية")]
        public IFormFile Icon { get; set; }

        [Required(ErrorMessage = "يرجي اختيار نوع الخاصية")]
        public SpecificationType Type { get; set; }
    }
}
