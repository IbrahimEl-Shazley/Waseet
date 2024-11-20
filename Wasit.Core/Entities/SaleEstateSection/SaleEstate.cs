using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.SaleEstateSection
{
    public class SaleEstate : Entity
    {
        public SaleEstate()
        {
            Images = new HashSet<SaleEstateImage>();
            SpecificationValues = new HashSet<SaleEstateSpecificationValue>();
            PricingRequests = new HashSet<PricingRequest>();
            UserPriceToEstates = new HashSet<UserPriceToEstate>();
            ReservationRequests = new HashSet<SaleReservationRequest>();
            RatingRequests = new HashSet<SaleRatingRequest>();
            PurchaseRequests = new HashSet<PurchaseRequest>();
            Favorites = new HashSet<SaleEstateFavorite>();    
        }
        public string EstateNumber { get; set; }
        public string EstateName { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Location { get; set; }
        public double EstateArea { get; set; }
        public string EstateDescription { get; set; }
        public string EstateFeatures { get; set; }
        public double EstatePrice { get; set; }
        public double FinalEstatePrice { get; set; }
        public double Deposit { get; set; }
        public bool Developable { get; set; }
        public bool IsShow { get; set; }
        public bool IsReviewed { get; set; }
        public bool IsReserved { get; set; }
        public bool IsSold { get; set; }
        public string? RejectionReason { get; set; }

        public long EstateTypeId { get; set; }
        public string UserId { get; set; }
        public long RegionId { get; set; }

        [ForeignKey(nameof(EstateTypeId))]
        public virtual EstateType EstateType { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }

        [ForeignKey(nameof(RegionId))]
        public virtual Region Region { get; set; }

        public virtual ICollection<SaleEstateImage> Images { get; set; }  
        public virtual ICollection<SaleEstateSpecificationValue> SpecificationValues { get; set; }  
        public virtual ICollection<PricingRequest> PricingRequests { get; set; }  
        public virtual ICollection<UserPriceToEstate> UserPriceToEstates { get; set; }  
        public virtual ICollection<SaleReservationRequest> ReservationRequests { get; set; }  
        public virtual ICollection<SaleRatingRequest> RatingRequests { get; set; }  
        public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; }  
        public virtual ICollection<SaleEstateFavorite> Favorites { get; set; }




        public string Address(Language lang) => lang == Language.Ar ? $"{Region.NameAr}, {Region.City.NameAr}" : $"{Region.NameEn}, {Region.City.NameEn}";
    }
}
