using Wasit.Services.DTOs.Schema.Sale.SaleEstate;

namespace Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest
{
    public class ExtendedConsumerRatingRequestInfoForSaleEstateDto : ConsumerRatingRequestInfoForSaleEstateDto
    {
        public long Id { get; set; }
        public SaleEstateDto EstateInfo { get; set; }
    }
}
