using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.RentEstateSection
{
    public class RentRequest : Entity
    {
        public RentStatus Status { get; set; }
        public int YearCount { get; set; }
        public int MonthCount { get; set; }
        public double TotalPrice { get; set; }
        public double Deposit { get; set; }
        public TypePay TypePay { get; set; }
        public bool IsPay { get; set; }
        public bool IsRented { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsEstateRented { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public double OwnerRating { get; set; }

        public long RentEstateId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(RentEstateId))]
        public virtual RentEstate RentEstate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }

    }
}
