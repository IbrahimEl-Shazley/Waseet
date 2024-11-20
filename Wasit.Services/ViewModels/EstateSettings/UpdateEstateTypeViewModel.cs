using System.ComponentModel.DataAnnotations;

namespace Wasit.Services.ViewModels.EstateSettings
{
    public class UpdateEstateTypeViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "يرجي اختيار قسم واحد علي الاقل")]
        public List<long> Categories { get; set; } = [];

        [Required(ErrorMessage = "مطلوب")]
        public List<UpdateEstateTypeSpecificationViewModel> Specifications { get; set; } = [];
        
        public List<long> SelectedSpecs { get; set; } = [];
    }
}
