using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.EntertainmentEstateSection
{
    public class EntertainmentEstateImage : Entity
    {
        public string Image { get; set; }
        public long EntertainmentEstateId { get; set; }

        [ForeignKey(nameof(EntertainmentEstateId))]
        public virtual EntertainmentEstate EntertainmentEstate { get; set; }

    }
}
