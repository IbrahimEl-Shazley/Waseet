using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;

namespace Wasit.Services.DTOs.Schema.Rent.RentReservationRequest
{
    public class RentReservationRequestInfoDto
    {
        public long Id { get; set; }
        public RentEstateDto EstateInfo { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public ReservationStatus StatusEnum { get; set; }
        public string RequestDateTime { get; set; }
    }
}
