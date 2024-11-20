using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentEstate;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;

namespace Wasit.Services.DTOs.Schema.Entertainment.EntertainmentRentRequest
{
    public class EntertainmentRentRequestInfoDto
    {
        public long Id { get; set; }
        public EntertainmentEstateDto EstateInfo { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public DailyRentStatus StatusEnum { get; set; }
        public int DaysCount { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
        public string DateTime { get; set; }
        public bool IsRated { get; set; }
        public RatingInfoDto? UserRating { get; set; }
    }
}
