using System.ComponentModel.DataAnnotations;
using Wasit.Core.Constants;

namespace Wasit.Services.ViewModels.Profits.AllPackages
{
    public class CreateBrokerPackageViewModel
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [RegularExpression(RegEx.IntegerNumber, ErrorMessage = "يجب ان تكون القيمة رقم صحيح")]
        public int MaxUsageCount { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [RegularExpression(RegEx.IntegerNumber, ErrorMessage = "يجب ان تكون القيمة رقم صحيح")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string DetailsAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string DetailsEn { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [RegularExpression(RegEx.Price, ErrorMessage = "يجب ان تكون القيمة رقم")]
        public double Price { get; set; }
    }
}
