using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.Entertainment.EntertainmentEstate
{
    public class EntertainmentEstateInfoDto : BaseEstateInfoDto
    {
        public bool IsRented { get; set; }
        public double Rating { get; set; }

        public List<BaseRatingItemDto> Ratings { get; set; }
        public int RequestsCount { get; set; }
    }
}
