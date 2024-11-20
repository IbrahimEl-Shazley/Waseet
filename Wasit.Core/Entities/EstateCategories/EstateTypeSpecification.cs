using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.EstateCategories
{
    public class EstateTypeSpecification : Entity
    {
        public EstateTypeSpecification()
        {
            SaleEstateSpecificationValues = new HashSet<SaleEstateSpecificationValue>();
            RentEstateSpecificationValues = new HashSet<RentEstateSpecificationValue>();
            DailyRentEstateSpecificationValues = new HashSet<DailyRentEstateSpecificationValue>();
            EntertainmentEstateSpecificationValues = new HashSet<EntertainmentEstateSpecificationValue>();
        }
        public long EstateTypeId { get; set; }
        public long SpecificationId { get; set; }
        public bool IsRequired { get; set; }

        [ForeignKey(nameof(EstateTypeId))]
        public  virtual EstateType EstateType { get; set; }

        [ForeignKey(nameof(SpecificationId))]
        public virtual Specification Specification{ get; set; }

        public virtual ICollection<SaleEstateSpecificationValue> SaleEstateSpecificationValues { get; set; }
        public virtual ICollection<RentEstateSpecificationValue> RentEstateSpecificationValues { get; set; }
        public virtual ICollection<DailyRentEstateSpecificationValue> DailyRentEstateSpecificationValues { get; set; }
        public virtual ICollection<EntertainmentEstateSpecificationValue> EntertainmentEstateSpecificationValues { get; set; }
    }
}
