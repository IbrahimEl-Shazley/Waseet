using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.SaleEstateSection
{
    public class SaleEstateImage : Entity
    {
        public string Image { get; set; }
        public long SaleEstateId { get; set; }

        [ForeignKey(nameof(SaleEstateId))]
        public virtual SaleEstate SaleEstate { get; set; }
    }
}
