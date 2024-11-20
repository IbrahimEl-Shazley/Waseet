using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.SaleEstateSection
{
    public class PurchaseRequest : Entity
    {
        public PurchaseStatus Status { get; set; }
        public double Deposit { get; set; }
        public TypePay TypePay { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool HasRefundRequest { get; set; }
        public string RefundReason { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsEstateReceived { get; set; }

        public long SaleEstateId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(SaleEstateId))]
        public virtual SaleEstate SaleEstate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }

    }
}
