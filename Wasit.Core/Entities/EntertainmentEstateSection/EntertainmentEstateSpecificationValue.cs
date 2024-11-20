using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.EntertainmentEstateSection
{
    public class EntertainmentEstateSpecificationValue : Entity
    {
        public string SpecificationValue { get; set; }
        public long EntertainmentEstateId { get; set; }
        public long EstateTypeSpecificationId { get; set; }

        [ForeignKey(nameof(EntertainmentEstateId))]
        public virtual EntertainmentEstate EntertainmentEstate { get; set; }

        [ForeignKey(nameof(EstateTypeSpecificationId))]
        public virtual EstateTypeSpecification EstateTypeSpecification { get; set; }

    }
}
