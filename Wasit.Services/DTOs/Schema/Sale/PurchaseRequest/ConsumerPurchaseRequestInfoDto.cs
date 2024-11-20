namespace Wasit.Services.DTOs.Schema.Sale.PurchaseRequest
{
    public class ConsumerPurchaseRequestInfoDto
    {
        public double Deposit { get; set; }
        public bool IsPaid { get; set; }
        public bool HasRefundRequest { get; set; }
    }
}
