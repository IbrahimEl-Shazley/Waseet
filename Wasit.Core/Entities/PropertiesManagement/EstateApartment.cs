using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.PropertiesManagement
{
    public class EstateApartment : Entity
    {
        public EstateApartment()
        {
            ApartmentRentPays = new HashSet<ApartmentRentPay>();
        }

        public string ApartmentName { get; set; }
        public int Number { get; set; }
        public double RentalPrice { get; set; }
        public PaymentDeadline PaymentDeadline { get; set; }
        public bool IsRentPaid { get; set; }
        public long RentalOrderId { get; set; }

        [ForeignKey(nameof(RentalOrderId))]
        public virtual RentalManagementOrder RentalOrder { get; set; }


        public virtual ICollection<ApartmentRentPay> ApartmentRentPays { get; set; }
    }
}
