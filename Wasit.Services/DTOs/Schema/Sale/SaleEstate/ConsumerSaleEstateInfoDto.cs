using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;

namespace Wasit.Services.DTOs.Schema.Sale.SaleEstate
{
    public class ConsumerSaleEstateInfoDto : BaseEstateInfoDto
    {
        public bool IsSubscribedToAddPrice { get; set; }
        public double AssignDelegateCost { get; set; }
        public double AddReservationRequestCost { get; set; }
        public string Features { get; set; }
        public double Deposit { get; set; }
        public double PricingValue { get; set; }
        public double AveragePrice { get; set; }
        public PublisherInfoDto PublisherInfo { get; set; }
        public string ManagementPhoneNumber { get; set; }
        public bool HasAddedPriceBefore { get; set; }
        public bool IsSold { get; set; }
        public bool IsReserved { get; set; }
    }
}
