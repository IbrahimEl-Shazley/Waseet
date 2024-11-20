using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.Sale.PurchaseRequest
{
    public class PurchaseRequestDto : BaseRequestDto
    {
        public bool IsAccepted { get; set; }
        public bool IsRejected { get; set; }
        public double FinalEstatePrice { get; set; }
        public bool IsPaid { get; set; }
    }
}
