using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.EstateServiceSection;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.EstateCategories
{
    public class EstateType : LookupEntity
    {
        public EstateType()
        {
            EstateTypeSpecifications = new HashSet<EstateTypeSpecification>();
            SaleEstates = new HashSet<SaleEstate>();
            RentEstates = new HashSet<RentEstate>();
            DailyRentEstates = new HashSet<DailyRentEstate>();
            EntertainmentEstates = new HashSet<EntertainmentEstate>();
            CategoryEstateTypes = new HashSet<CategoryEstateType>();
            EstateServices = new HashSet<EstateService>();

        }

        public virtual ICollection<CategoryEstateType> CategoryEstateTypes { get; set; }

        public virtual ICollection<EstateTypeSpecification> EstateTypeSpecifications { get; set; }
        
        public virtual ICollection<SaleEstate> SaleEstates { get; set; }
        
        public virtual ICollection<RentEstate> RentEstates { get; set; } 

        public virtual ICollection<DailyRentEstate> DailyRentEstates { get; set; }

        public virtual ICollection<EntertainmentEstate> EntertainmentEstates { get; set; }

        public virtual ICollection<EstateService> EstateServices { get; set; }

    }
}
