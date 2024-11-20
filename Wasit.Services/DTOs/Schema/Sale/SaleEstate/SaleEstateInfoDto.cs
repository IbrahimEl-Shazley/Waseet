using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.Sale.SaleEstate
{
    public class SaleEstateInfoDto : BaseEstateInfoDto
    {
        public double Deposit { get; set; }
        public double AveragePrice { get; set; }
        public double FinalSalePrice { get; set; }
        public bool IsReserved { get; set; }
        public bool IsSold { get; set; }
        public double PricingServiceCost { get; set; }

        public int ReservationRequestsCount { get; set; }
        public int EvaluationRequestsCount { get; set; }
        public int PurchaseRequestsCount { get; set; }
    }
}
