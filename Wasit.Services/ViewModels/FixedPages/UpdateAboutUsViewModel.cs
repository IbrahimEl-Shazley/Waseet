using System.ComponentModel.DataAnnotations;

namespace Wasit.Services.ViewModels.FixedPages
{
    public class UpdateAboutUsViewModel
    {

        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        public string AboutUsAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string AboutUsEn { get; set; }
    }
}
