using AAITHelper;
using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.PropertiesManagement
{
    public class ApartmentRentPay : Entity
    {
        public double RentalPrice { get; set; }
        public double AppTax { get; set; }
        public double PublisherNetTotal { get; set; }
        public TypePay TypePay { get; set; }
        public bool IsPay { get; set; }
        public DateTime PaymentDate { get; set; } = HelperDate.GetCurrentDate();
        public long EstateApartmentId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(EstateApartmentId))]
        public virtual EstateApartment EstateApartment { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }
    }
}
