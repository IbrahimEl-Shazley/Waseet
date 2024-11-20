using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.DailyRentEstateSection
{
    public class DailyRentEstateImage : Entity
    {
        public string Image { get; set; }
        public long DailyRentEstateId { get; set; }

        [ForeignKey(nameof(DailyRentEstateId))]
        public virtual DailyRentEstate DailyRentEstate { get; set; }

    }
}
