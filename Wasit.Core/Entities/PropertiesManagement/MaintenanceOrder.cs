using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.PropertiesManagement
{
    public class MaintenanceOrder:Entity
    {
        public double ServiceCost { get; set; }
        public TypePay TypePay { get; set; }
        public bool IsPay { get; set; }
        public string EstateNumber { get; set; }
        public string EstateName { get; set; }
        public string EstateImage { get; set; }
        public double EstateArea { get; set; }
        public RentalManagementOrderStatus OrderStatus { get; set; }
        public double UserRate { get; set; }
        public string UserId { get; set; }
        public string? ProviderId { get; set; }
        public long RegionId { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Location { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }

        [ForeignKey(nameof(ProviderId))]
        public virtual ApplicationDbUser Provider { get; set; }

        [ForeignKey(nameof(RegionId))]
        public virtual Region Region { get; set; }




        public string Address(Language lang) => lang == Language.Ar ? $"{Region.NameAr}, {Region.City.NameAr}" : $"{Region.NameEn}, {Region.City.NameEn}";
    }
}
