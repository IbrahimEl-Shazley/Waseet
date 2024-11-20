using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using BaseEstateInfoDto = Wasit.Services.DTOs.Schema.Shared.ConsumerEstates.BaseEstateInfoDto;

namespace Wasit.Services.DTOs.Schema.Entertainment.EntertainmentEstate
{
    public class ConsumerEntertainmentEstateInfoDto : BaseEstateInfoDto
    {
        public double Rating { get; set; }
        public HashSet<BaseRatingItemDto> Ratings { get; set; } = [];
        public PublisherInfoDto PublisherInfo { get; set; }
        public string ManagementPhoneNumber { get; set; }
        public bool IsAvailable { get; set; }
    }
}
