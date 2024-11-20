using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Wasit.Services.ViewModels.Advertisments
{
    public class CreateAdvertismentViewModel
    {
        [Required(ErrorMessage = "الصورة مطلوبة")]
        public IFormFile Image { get; set; }
    }
}
