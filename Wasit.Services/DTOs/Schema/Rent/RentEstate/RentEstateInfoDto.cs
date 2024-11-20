using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.Rent.RentEstate
{
    public class RentEstateInfoDto : BaseEstateInfoDto
    {
        public bool IsReserved { get; set; }
        public bool IsRented { get; set; }
        public double PaidRentPrice { get; set; }
        public int ReservationRequestsCount { get; set; }
        public int EvaluationRequestsCount { get; set; }
        public int RentRequestsCount { get; set; }
    }
}
