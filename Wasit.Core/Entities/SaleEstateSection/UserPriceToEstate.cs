using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;

namespace Wasit.Core.Entities.SaleEstateSection
{
    public class UserPriceToEstate:Entity
    {
        public double Price { get; set; }
        public long SaleEstateId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(SaleEstateId))]
        public virtual SaleEstate SaleEstate { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }
    }
}
