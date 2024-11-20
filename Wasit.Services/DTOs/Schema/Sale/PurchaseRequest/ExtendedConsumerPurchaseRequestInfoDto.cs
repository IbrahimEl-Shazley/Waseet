using Wasit.Services.DTOs.Schema.Sale.SaleEstate;

namespace Wasit.Services.DTOs.Schema.Sale.PurchaseRequest
{
    public class ExtendedConsumerPurchaseRequestInfoDto : ConsumerPurchaseRequestInfoDto
    {
        public long Id { get; set; }
        public SaleEstateDto EstateInfo { get; set; }
        public double FinalPrice { get; set; }
    }
}
