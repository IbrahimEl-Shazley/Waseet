using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.Shared.MyEstates
{
    public class BaseRatingItemDto : DTO<long>
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string TimeSpan { get; set; }
    }
}
