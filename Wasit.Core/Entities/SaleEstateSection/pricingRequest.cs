using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.SaleEstateSection
{
    public class PricingRequest : Entity
    {
        public double AdminPrice { get; set; }
        public double ServiceCost { get; set; }
        public TypePay TypePay { get; set; }
        public bool IsPay { get; set; }
        public long SaleEstateId { get; set; }

        [ForeignKey(nameof(SaleEstateId))]
        public virtual SaleEstate SaleEstate { get; set; }
    }
}
