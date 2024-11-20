using Wasit.Services.DTOs.Schema.Rent.RentEstate;

namespace Wasit.Services.DTOs.Schema.Rent.RentRequest
{
    public class ExtendedConsumerRentRequestInfoDto : ConsumerRentRequestInfoDto
    {
        public long Id { get; set; }
        public RentEstateDto EstateInfo { get; set; }
    }
}
