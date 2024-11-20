using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.EntertainmentEstateSection
{
    public class EntertainmentRequest : Entity
    {
        public EntertainmentRequest()
        {
            ReportEntertainmentComments = new HashSet<ReportEntertainmentComment>();
        }
        public DailyRentStatus Status { get; set; }
        public CancelStatus CancelStatus { get; set; }
        public DateTime CancelDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime LeaveDate { get; set; }
        public int TotalDays { get; set; }
        public double TotalPrice { get; set; }
        public TypePay TypePay { get; set; }
        public bool IsPay { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsEstateReceived { get; set; }
        public double OwnerRating { get; set; }
        public DateTime? OwnerRatingDateTime { get; set; }
        public double UserRating { get; set; }
        public double EstateRating { get; set; }
        public DateTime? UserRatingDateTime { get; set; }
        public string EstateComment { get; set; }
        public bool ShowComment { get; set; } = true;

        public long EntertainmentEstateId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(EntertainmentEstateId))]
        public virtual EntertainmentEstate EntertainmentEstate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }

        public virtual ICollection<ReportEntertainmentComment> ReportEntertainmentComments { get; set; }

    }
}
