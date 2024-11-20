using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;

namespace Wasit.Core.Entities.EntertainmentEstateSection
{
    public class EntertainmentEstateFavorite : Entity
    {
        public long EntertainmentEstateId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(EntertainmentEstateId))]
        public virtual EntertainmentEstate EntertainmentEstate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }

    }
}
