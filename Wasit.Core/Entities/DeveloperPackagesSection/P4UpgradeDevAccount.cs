using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.DeveloperPackagesSection
{
    public class P4UpgradeDevAccount : LookupEntity
    {
        public int SubscriptionDays { get; set; }
        public double Price { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
    }
}
