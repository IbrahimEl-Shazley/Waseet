using System.ComponentModel.DataAnnotations;

namespace Wasit.Services.ViewModels.Region
{
    public class CreateRegionViewModel
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "يرجي تحديد المدينة")]
        public long CityId { get; set; }
    }
}
