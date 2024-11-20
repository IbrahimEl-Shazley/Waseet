using System.ComponentModel.DataAnnotations;

namespace Wasit.Services.ViewModels.FixedPages
{
    public class UpdateTermsAndConditionsViewModel
    {

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string ConditionsOwnerAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string CondtionsOwnerEn { get; set; }


        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string ConditionsDelegateAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string ConditionsDelegateEn { get; set; }


        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string ConditionsBrokerAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string ConditionsBrokerEn { get; set; }


        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string ConditionsDeveloperAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string ConditionsDeveloperEn { get; set; }
    }
}
