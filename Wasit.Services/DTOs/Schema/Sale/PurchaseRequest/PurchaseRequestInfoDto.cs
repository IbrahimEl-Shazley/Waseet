using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;

namespace Wasit.Services.DTOs.Schema.Sale.PurchaseRequest
{
    public class PurchaseRequestInfoDto
    {
        public long Id { get; set; }
        public SaleEstateDto EstateInfo { get; set; }
        public string UserName { get; set; }
        public bool IsAccepted { get; set; }
        public double FinalEstatePrice { get; set; }
        public bool IsRejected { get; set; }
        public bool IsPaid { get; set; }
        public string Status { get; set; }
        public PurchaseStatus StatusEnum { get; set; }
    }
}
