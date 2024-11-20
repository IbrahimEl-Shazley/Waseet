using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates
{
    public class DailyRentEstateInfoDto : BaseEstateInfoDto
    {
        public bool IsRented { get; set; }
        public double Rating { get; set; }

        public List<BaseRatingItemDto> Ratings { get; set; }
        public int RequestsCount { get; set; }
    }
}
