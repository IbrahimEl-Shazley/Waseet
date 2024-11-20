using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.AddPriceToEstate
{
    public class P4AddPriceToEstate : LookupEntity
    {
        public int SubscriptionDays { get; set; }
        public int AddPriceCount { get; set; }
        public double Price { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
    }
}
