using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.SaleEstateSection
{
    public class SaleEstateSpecificationValue :Entity
    {
        public string SpecificationValue { get; set; }
        public long SaleEstateId { get; set; }
        public long EstateTypeSpecificationId { get; set; }

        [ForeignKey(nameof(SaleEstateId))]
        public virtual SaleEstate SaleEstate { get; set; }

        [ForeignKey(nameof(EstateTypeSpecificationId))]
        public virtual EstateTypeSpecification EstateTypeSpecification { get; set; }
    }
}
