using System.ComponentModel.DataAnnotations;

namespace Wasit.Services.ViewModels.City
{
    public class CityViewModel
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameEn { get; set; }

        public List<CityItemViewModel> Cities { get; set; } = [];
    }
}
