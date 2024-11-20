using Wasit.Services.DTOs.Schema.Sale.SaleEstate;

namespace Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest
{
    public class ExtendedConsumerReservationRequestInfoForSaleEstateDto : ConsumerReservationRequestInfoForSaleEstateDto
    {
        public long Id { get; set; }
        public SaleEstateDto EstateInfo { get; set; }
    }
}
