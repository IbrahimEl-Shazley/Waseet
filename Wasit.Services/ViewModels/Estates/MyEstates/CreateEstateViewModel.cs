using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Wasit.Core.Constants;
using Wasit.Core.Enums;

namespace Wasit.Services.ViewModels.Estates.MyEstates
{
    public class CreateEstateViewModel
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public CategoryType CategoryType { get; set; }
        
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int EstateTypeId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        //[RegularExpression(RegEx.IntegerNumber,ErrorMessage ="من فضلك ادخل ارقام فقط")]
        public string UniqueNumber { get; set; }

        //[Required(ErrorMessage = "هذا الحقل مطلوب")]
        //[RegularExpression(RegEx.Price, ErrorMessage = "يرجي ادخال قيمة صحيحة")]
        public double Deposit { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int CityId { get; set; }
        
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Lat { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Lng { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Location { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [RegularExpression(RegEx.Price, ErrorMessage = "يرجي ادخال قيمة صحيحة")]
        public double Area { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Description { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Features { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public bool IsDevelopable { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [RegularExpression(RegEx.Price, ErrorMessage = "يرجي ادخال قيمة صحيحة")]
        public double Price { get; set; }

        public ICollection<IFormFile> Images { get; set; } = new HashSet<IFormFile>();
    }
}
