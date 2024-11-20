using Wasit.Core.Enums;

namespace Wasit.Services.DTOs.Schema.Sale.UserPriceToEstate
{
    public class SubscribeToAddPricePackageDto
    {
        public bool IsActive { get; set; }
        public bool IsPay { get; set; }
        public TypePay PaymentMethod { get; set; }
        public string UserId { get; set; }
        public int AddPriceCount { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public int RemainingAddPriceCount { get; set; }
        public string PDescriptionAr { get; set; }
        public string PDescriptionEn { get; set; }
        public string PNameAr { get; set; }
        public string PNameEn { get; set; }
        public int PSubscriptionDays { get; set; }
        public double Price { get; set; }
    }
}
