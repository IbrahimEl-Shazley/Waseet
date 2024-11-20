using Wasit.Services.DTOs.Schema.Rent.RentEstate;

namespace Wasit.Services.DTOs.Schema.Rent.RentReservationRequest
{
    public class ExtendedConsumerReservationRequestInfoForRentEstateDto : ConsumerReservationRequestInfoForRentEstateDto
    {
        public long Id { get; set; }
        public RentEstateDto EstateInfo { get; set; }
    }
}
