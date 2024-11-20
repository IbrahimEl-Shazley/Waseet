using Wasit.Core.Entities.Shared;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.EstateServiceSection
{
    public class P4ShowEstateService : LookupEntity
    {
        public int SubscriptionDays { get; set; }
        public double Price { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }

        public string Name(Language lang) => lang == Language.Ar ? NameAr : NameEn;
        public string Description(Language lang) => lang == Language.Ar ? DescriptionAr : DescriptionEn;

    }
}
