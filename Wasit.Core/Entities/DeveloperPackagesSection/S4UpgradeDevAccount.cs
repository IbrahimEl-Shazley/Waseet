using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.DeveloperPackagesSection
{
    public class S4UpgradeDevAccount : Entity
    {
        public string PNameAr { get; set; }
        public string PNameEn { get; set; }
        public int PSubscriptionDays { get; set; }
        public double Price { get; set; }
        public string PDescriptionAr { get; set; }
        public string PDescriptionEn { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public TypePay TypePay { get; set; }
        public bool IsPay { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationDbUser User { get; set; }
    }
}
