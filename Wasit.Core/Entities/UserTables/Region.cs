using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.EstateServiceSection;
using Wasit.Core.Entities.PropertiesManagement;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.UserTables
{
    public class Region : LookupEntity
    {
        public Region()
        {
            Users = new HashSet<ApplicationDbUser>();
            SaleEstates = new HashSet<SaleEstate>();
            RentEstates = new HashSet<RentEstate>();
            DailyRentEstates = new HashSet<DailyRentEstate>();
            EntertainmentEstates = new HashSet<EntertainmentEstate>();
            RentalManagementOrders = new HashSet<RentalManagementOrder>();
            MaintenanceOrders = new HashSet<MaintenanceOrder>();
            EstateEstateServices = new HashSet<EstateService>();
            CompanyEstateServices = new HashSet<EstateService>();

        }

        public long CityId { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<ApplicationDbUser> Users { get; set; }

        public virtual ICollection<SaleEstate> SaleEstates { get; set; }

        public virtual ICollection<RentEstate> RentEstates { get; set; }

        public virtual ICollection<DailyRentEstate> DailyRentEstates { get; set; }

        public virtual ICollection<EntertainmentEstate> EntertainmentEstates { get; set; }
        
        public virtual ICollection<RentalManagementOrder> RentalManagementOrders { get; set; }
        
        public virtual ICollection<MaintenanceOrder> MaintenanceOrders { get; set; }
        [InverseProperty(nameof(Wasit.Core.Entities.EstateServiceSection.EstateService.EstateRegion))]
        public virtual ICollection<EstateService> EstateEstateServices { get; set; }
        [InverseProperty(nameof(Wasit.Core.Entities.EstateServiceSection.EstateService.CompanyRegion))]
        public virtual ICollection<EstateService> CompanyEstateServices { get; set; }

    }
}
