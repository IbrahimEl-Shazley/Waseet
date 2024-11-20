using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;

namespace Wasit.Core.Entities.RentEstateSection
{
    public class RentEstateFavorite : Entity
    {
        public long RentEstateId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(RentEstateId))]
        public virtual RentEstate RentEstate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }

    }
}
