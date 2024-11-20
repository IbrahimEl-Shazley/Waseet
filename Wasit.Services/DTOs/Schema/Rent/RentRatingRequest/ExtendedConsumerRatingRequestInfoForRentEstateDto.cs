using Wasit.Services.DTOs.Schema.Rent.RentEstate;

namespace Wasit.Services.DTOs.Schema.Rent.RentRatingRequest
{
    public class ExtendedConsumerRatingRequestInfoForRentEstateDto : ConsumerRatingRequestInfoForRentEstateDto
    {
        public long Id { get; set; }
        public RentEstateDto EstateInfo { get; set; }
    }
}
