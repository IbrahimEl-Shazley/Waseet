using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.RentEstateSection
{
    public class RentEstate : Entity
    {
        public RentEstate()
        {
            Images = new HashSet<RentEstateImage>();  
            SpecificationValues= new HashSet<RentEstateSpecificationValue>();
            ReservationRequests = new HashSet<RentReservationRequest>();
            RentRequests = new HashSet<RentRequest>();
            RatingRequests = new HashSet<RentRatingRequest>();
            Favorites = new HashSet<RentEstateFavorite>();
        }
        public string EstateNumber { get; set; }
        public string EstateName { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Location { get; set; }
        public double EstateArea { get; set; }
        public string EstateDescription { get; set; }
        public string EstateFeatures { get; set; }
        public double MonthRentPrice { get; set; }
        public double PaidRentPrice { get; set; }
        public bool Developable { get; set; }
        public bool IsShow { get; set; }
        public bool IsReserved { get; set; }
        public bool IsReviewed { get; set; }
        public bool IsRented { get; set; }
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

        public virtual ICollection<RentEstateImage> Images { get; set; }
        public virtual ICollection<RentEstateSpecificationValue> SpecificationValues { get; set; }
        public virtual ICollection<RentReservationRequest> ReservationRequests { get; set; }
        public virtual ICollection<RentRequest> RentRequests { get; set; }
        public virtual ICollection<RentRatingRequest> RatingRequests { get; set; }
        public virtual ICollection<RentEstateFavorite> Favorites { get; set; }




        public string Address(Language lang) => lang == Language.Ar ? $"{Region.NameAr}, {Region.City.NameAr}" : $"{Region.NameEn}, {Region.City.NameEn}";
    }
}
