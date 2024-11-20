using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.DailyRentEstateSection
{
    public class DailyRentEstateSpecificationValue : Entity
    {
        public string SpecificationValue { get; set; }
        public long DailyRentEstateId { get; set; }
        public long EstateTypeSpecificationId { get; set; }

        [ForeignKey(nameof(DailyRentEstateId))]
        public virtual DailyRentEstate DailyRentEstate { get; set; }

        [ForeignKey(nameof(EstateTypeSpecificationId))]
        public virtual EstateTypeSpecification EstateTypeSpecification { get; set; }


    }
}
