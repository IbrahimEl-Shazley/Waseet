using System.ComponentModel.DataAnnotations;
using Wasit.Core.Enums;

namespace Wasit.Services.ViewModels.EstateSettings
{
    public class CreateEstateTypeViewModel
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "يرجي اختيار قسم واحد علي الاقل")]
        public List<CategoryType> Categories { get; set; } = [];

        [Required(ErrorMessage = "مطلوب")]
        public long SpecificationId { get; set; }

        [Required(ErrorMessage = "مطلوب")]
        public List<CreateEstateTypeSpecificationViewModel> Specifications { get; set; } = [];
    }
}
