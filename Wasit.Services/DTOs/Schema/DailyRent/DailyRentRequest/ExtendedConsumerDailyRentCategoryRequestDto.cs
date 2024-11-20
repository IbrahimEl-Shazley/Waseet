using Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;

namespace Wasit.Services.DTOs.Schema.DailyRent.DailyRentRequest
{
    public class ExtendedConsumerDailyRentCategoryRequestDto : ConsumerDailyRentCategoryRequestDto
    {
        public long Id { get; set; }
        public DailyRentEstateDto EstateInfo { get; set; }
        public int DaysCount { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
        public string PublisherPhoneNumber { get; set; }
        public string DateTime { get; set; }
        public string? CancelationDate { get; set; }
        public bool IsRated { get; set; }
        public RatingInfoDto? MyRating { get; set; }
        public RatingInfoDto? OwnerRating { get; set; }
    }
}
