using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.PropertiesManagement
{
    public class RentalManagementOrder : Entity
    {
        public RentalManagementOrder()
        {
            EstateApartments = new HashSet<EstateApartment>();
        }

        public string EstateNumber { get; set; }
        public string EstateName { get; set; }
        public string EstateImage { get; set; }
        public RentalManagementOrderStatus OrderStatus { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsTerminated { get; set; }
        public bool IsApproved { get; set; }
        public string UserId { get; set; }
        public long RegionId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }

        [ForeignKey(nameof(RegionId))]
        public virtual Region Region { get; set; }

        public virtual ICollection<EstateApartment> EstateApartments { get; set; }



        public string Address(Language lang) => lang == Language.Ar ? $"{Region.NameAr}, {Region.City.NameAr}" : $"{Region.NameEn}, {Region.City.NameEn}";

    }
}
