using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.EntertainmentEstateSection
{
    public class EntertainmentEstate : Entity
    {
        public EntertainmentEstate()
        {
            Images = new HashSet<EntertainmentEstateImage>();
            SpecificationValues = new HashSet<EntertainmentEstateSpecificationValue>();
            Favorites = new HashSet<EntertainmentEstateFavorite>();
            Requests = new HashSet<EntertainmentRequest>();
        }
        public string EstateNumber { get; set; }
        public string EstateName { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Location { get; set; }
        public double EstateArea { get; set; }
        public string EstateDescription { get; set; }
        public string EstateFeatures { get; set; }
        public double DayRentPrice { get; set; }
        public bool Developable { get; set; }
        public bool IsShow { get; set; }
        public bool IsRented { get; set; }
        public DateTime? BookingFrom { get; set; }
        public DateTime? BookingTo { get; set; }
        public double Rating { get; set; }
        public bool IsReviewed { get; set; }
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

        public virtual ICollection<EntertainmentEstateImage> Images { get; set; }
        public virtual ICollection<EntertainmentEstateSpecificationValue> SpecificationValues { get; set; }
        public virtual ICollection<EntertainmentEstateFavorite> Favorites{ get; set; }
        public virtual ICollection<EntertainmentRequest> Requests{ get; set; }



        public string Address(Language lang) => lang == Language.Ar ? $"{Region.NameAr}, {Region.City.NameAr}" : $"{Region.NameEn}, {Region.City.NameEn}";
    }
}
