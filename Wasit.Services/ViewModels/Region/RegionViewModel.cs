using System.ComponentModel.DataAnnotations;

namespace Wasit.Services.ViewModels.Region
{
    public class RegionViewModel
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameEn { get; set; }

        public List<RegionItemViewModel> Regions { get; set; } = [];
    }
}
