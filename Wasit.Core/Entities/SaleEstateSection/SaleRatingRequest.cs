using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.SaleEstateSection
{
    public class SaleRatingRequest : Entity
    {
        public RatingStatus RatingStatus { get; set; }
        public double ServiceCost { get; set; }
        public TypePay TypePay { get; set; }
        public bool IsPay { get; set; }
        public string ReportUrl { get; set; }
        public double UserRating { get; set; }

        public long SaleEstateId { get; set; }
        public string UserId { get; set; }
        public string? ProviderId { get; set; }


        [ForeignKey(nameof(SaleEstateId))]
        public virtual SaleEstate SaleEstate { get; set; }
   
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }
        
        [ForeignKey(nameof(ProviderId))]
        public virtual ApplicationDbUser Provider { get; set; }
    }
}
