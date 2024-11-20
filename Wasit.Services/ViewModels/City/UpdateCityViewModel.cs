using System.ComponentModel.DataAnnotations;

namespace Wasit.Services.ViewModels.City
{
    public class UpdateCityViewModel
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameEn { get; set; }
    }
}
