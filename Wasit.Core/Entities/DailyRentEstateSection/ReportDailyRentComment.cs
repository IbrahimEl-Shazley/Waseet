using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;

namespace Wasit.Core.Entities.DailyRentEstateSection
{
    public class ReportDailyRentComment:Entity
    {
        public string ReasonForReport { get; set; }
        public long DailyRentRequestId { get; set; }
        public string ReporterId { get; set; }

        [ForeignKey(nameof(DailyRentRequestId))]
        public virtual DailyRentRequest DailyRentRequest { get; set; }
 
        [ForeignKey(nameof(ReporterId))]
        public virtual ApplicationDbUser Reporter { get; set; }
    }
}
