using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;

namespace Wasit.Core.Entities.DailyRentEstateSection
{
    public class DailyRentEstateFavorite:Entity
    {
        public long DailyRentEstateId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(DailyRentEstateId))]
        public virtual DailyRentEstate DailyRentEstate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }

    }
}
