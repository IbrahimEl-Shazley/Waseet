using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.RentEstateSection
{
    public class RentEstateImage : Entity
    {
        public string Image { get; set; }
        public long RentEstateId { get; set; }

        [ForeignKey(nameof(RentEstateId))]
        public virtual RentEstate RentEstate { get; set; }

    }
}
