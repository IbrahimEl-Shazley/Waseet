using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.RentEstateSection
{
    public class RentEstateSpecificationValue : Entity
    {
        public string SpecificationValue { get; set; }
        public long RentEstateId { get; set; }
        public long EstateTypeSpecificationId { get; set; }

        [ForeignKey(nameof(RentEstateId))]
        public virtual RentEstate RentEstate { get; set; }

        [ForeignKey(nameof(EstateTypeSpecificationId))]
        public virtual EstateTypeSpecification EstateTypeSpecification { get; set; }

    }
}
