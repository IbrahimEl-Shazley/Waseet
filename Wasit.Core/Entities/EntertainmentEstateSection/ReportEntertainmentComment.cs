using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;

namespace Wasit.Core.Entities.EntertainmentEstateSection
{
    public class ReportEntertainmentComment : Entity
    {
        public string ReasonForReport { get; set; }
        public long EntertainmentRequestId { get; set; }
        public string ReporterId { get; set; }

        [ForeignKey(nameof(EntertainmentRequestId))]
        public virtual EntertainmentRequest EntertainmentRequest { get; set; }

        [ForeignKey(nameof(ReporterId))]
        public virtual ApplicationDbUser Reporter { get; set; }

    }
}
