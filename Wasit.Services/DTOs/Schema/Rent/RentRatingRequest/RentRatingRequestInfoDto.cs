using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;

namespace Wasit.Services.DTOs.Schema.Rent.RentRatingRequest
{
    public class RentRatingRequestInfoDto
    {
        public long Id { get; set; }
        public RentEstateDto EstateInfo { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public RatingStatus StatusEnum { get; set; }
        public double EvaluatuionPrice { get; set; }
        public string DelegateName { get; set; }
        public string? Report { get; set; }
    }
}
