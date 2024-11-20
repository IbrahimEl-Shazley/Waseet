using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.BrokerEstateSection
{
    public class P4AddBrockerEstate : LookupEntity
    {
        public int SubscriptionDays { get; set; }
        public int EstatesCount { get; set; }
        public double Price { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
    }
}
