using AAITHelper;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.AddPriceToEstate;
using Wasit.Core.Entities.BrokerEstateSection;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.DeveloperPackagesSection;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.EstateServiceSection;
using Wasit.Core.Entities.PropertiesManagement;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Entities.SettingTables;
using Wasit.Core.Enums;
using Wasit.Core.Helpers.General;

namespace Wasit.Core.Entities.UserTables
{
    public partial class ApplicationDbUser : IdentityUser
    {
        public ApplicationDbUser()
        {
            Notifications = new HashSet<Notification>();
            DeviceIds = new HashSet<DeviceId>();
            HistoryNotify = new HashSet<HistoryNotify>();

            S4AddPriceToEstates = new HashSet<S4AddPriceToEstate>();
            EstateServices = new HashSet<EstateService>();

            #region Relation With Sale Estates

            SaleEstates = new HashSet<SaleEstate>();
            UserPriceToEstates = new HashSet<UserPriceToEstate>();
            SaleReservationRequests = new HashSet<SaleReservationRequest>();
            UserSaleRatingRequests = new HashSet<SaleRatingRequest>();
            ProviderSaleRatingRequests = new HashSet<SaleRatingRequest>();
            PurchaseRequests = new HashSet<PurchaseRequest>();
            SaleEstateFavorites = new HashSet<SaleEstateFavorite>();

            #endregion

            #region Relation With Rent Estates

            RentEstates = new HashSet<RentEstate>();
            RentReservationRequests = new HashSet<RentReservationRequest>();
            RentRequests = new HashSet<RentRequest>();
            RentEstateFavorites = new HashSet<RentEstateFavorite>();
            UserRentRatingRequests = new HashSet<RentRatingRequest>();
            ProviderRentRatingRequests = new HashSet<RentRatingRequest>();
            RentEstateFavorites = new HashSet<RentEstateFavorite>();
            #endregion

            #region Relation With Daily Rent Estates

            DailyRentEstates = new HashSet<DailyRentEstate>();
            DailyRentEstateFavorites = new HashSet<DailyRentEstateFavorite>();
            ReportDailyRentComments = new HashSet<ReportDailyRentComment>();
            DailyRentRequests = new HashSet<DailyRentRequest>();

            #endregion

            #region Relation With Entertainment Estates

            EntertainmentEstates = new HashSet<EntertainmentEstate>();
            EntertainmentEstateFavorites = new HashSet<EntertainmentEstateFavorite>();
            EntertainmentRequests = new HashSet<EntertainmentRequest>();
            ReportEntertainmentComments = new HashSet<ReportEntertainmentComment>();

            #endregion

            #region PropertiesManagement
            ApartmentRentPays = new HashSet<ApartmentRentPay>();
            UserMaintenanceOrders = new HashSet<MaintenanceOrder>();
            ProviderMaintenanceOrders = new HashSet<MaintenanceOrder>();
            RentalManagementOrders = new HashSet<RentalManagementOrder>();
            #endregion
        }

        #region User Properties

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public string Code { get; set; }
        public bool ActiveCode { get; set; } = false;
        public bool IsApprovedByAdmin { get; set; } = false;
        public DateTime CreationDate { get; set; } = HelperDate.GetCurrentDate();

        //add props 
        public string UserType { get; set; }  // As string Or Enum
        public AccountType? AccountType { get; set; }  // As string Or Enum
        public string User_Name { get; set; }
        public string PhoneCode { get; set; } = "966";
        public string? IDNumber { get; set; }
        public string ImgProfile { get; set; }
        public double Rating { get; set; }
        public bool IsVerified { get; set; }

        //البيانات الخاصه بالوسيط
        public string? BrokerageDocumentNo { get; set; }
        public FacilityType? FacilityType { get; set; }
        public string? CommercialNo { get; set; }
        public string? LicenseNo { get; set; }

        //البيانات الخاصه بالمندوب
        public string? WorkingNo { get; set; }

        //البيانات الخاصه بالمطور
        public string? Email { get; set; }
        public string? CoverPhoto { get; set; }
        public string? Description { get; set; }

        //البيانات الخاصه باللوكيشن
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Location { get; set; }

        //البيانات الخاصه بالحساب البنكى
        public string BankName { get; set; }
        public string AccOwnerName { get; set; }
        public string AccNumber { get; set; }
        public string IbanNumber { get; set; }

        //المحفظه
        public double Wallet { get; set; }

        //الاشعارات
        public bool AllowNotify { get; set; } = true;

        //المدينه
        public long RegionId { get; set; }

        public Language Lang { get; set; } = Language.Ar;

        #endregion




        public bool IsEligibleDelegate()
        {
            return (UserType == "Broker" || UserType == "Delegate") && (IsActive);
        }
        
        public bool IsEligibleDelegate(SaleEstate saleEstate)
        {
            return (UserType == "Broker" || UserType == "Delegate") && (IsActive && Region.CityId == saleEstate.Region.CityId && GeneralHelper.GetDistance(Lat, Lng, saleEstate.Lat, saleEstate.Lng) <= 50);
        }

        public bool IsEligibleDelegate(RentEstate rentEstate)
        {
            return (UserType == "Broker" || UserType == "Delegate") && (IsActive && Region.CityId == rentEstate.Region.CityId && GeneralHelper.GetDistance(Lat, Lng, rentEstate.Lat, rentEstate.Lng) <= 50);
        }


        #region Relations
        public virtual Region Region { get; set; }
        public virtual ICollection<DeviceId> DeviceIds { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<HistoryNotify> HistoryNotify { get; set; }


        public virtual ICollection<S4AddPriceToEstate> S4AddPriceToEstates { get; set; } = [];
        public virtual ICollection<S4ShowEstateService> S4ShowEstateServices { get; set; } = [];
        public virtual ICollection<S4AddBrockerEstate> S4AddBrockerEstates { get; set; } = [];
        public virtual ICollection<S4UpgradeDevAccount> S4UpgradeDevAccounts { get; set; } = [];
        public virtual ICollection<EstateService> EstateServices { get; set; }


        #region Relation With Sale Estates

        // Sale Estates
        public virtual ICollection<SaleEstate> SaleEstates { get; set; }
        public virtual ICollection<UserPriceToEstate> UserPriceToEstates { get; set; }
        public virtual ICollection<SaleReservationRequest> SaleReservationRequests { get; set; }

        [InverseProperty(nameof(SaleRatingRequest.User))]
        public virtual ICollection<SaleRatingRequest> UserSaleRatingRequests { get; set; }
        [InverseProperty(nameof(SaleRatingRequest.Provider))]
        public virtual ICollection<SaleRatingRequest> ProviderSaleRatingRequests { get; set; }

        public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; }
        public virtual ICollection<SaleEstateFavorite> SaleEstateFavorites { get; set; }

        #endregion

        #region Relation With Rent Estates

        // Rent Estates
        public virtual ICollection<RentEstate> RentEstates { get; set; }
        public virtual ICollection<RentReservationRequest> RentReservationRequests { get; set; }
        public virtual ICollection<RentRequest> RentRequests { get; set; }

        [InverseProperty(nameof(RentRatingRequest.User))]
        public virtual ICollection<RentRatingRequest> UserRentRatingRequests { get; set; }
        [InverseProperty(nameof(RentRatingRequest.Provider))]
        public virtual ICollection<RentRatingRequest> ProviderRentRatingRequests { get; set; }

        public virtual ICollection<RentEstateFavorite> RentEstateFavorites { get; set; }
        #endregion

        #region Relation With Daily Rent Estates

        // Daily Rent Estates
        public virtual ICollection<DailyRentEstate> DailyRentEstates { get; set; }
        public virtual ICollection<DailyRentEstateFavorite> DailyRentEstateFavorites { get; set; }
        public virtual ICollection<DailyRentRequest> DailyRentRequests { get; set; }
        public virtual ICollection<ReportDailyRentComment> ReportDailyRentComments { get; set; }

        #endregion


        #region Relation With Entertainment Estates

        // Entertainment Estates
        public virtual ICollection<EntertainmentEstate> EntertainmentEstates { get; set; }
        public virtual ICollection<EntertainmentEstateFavorite> EntertainmentEstateFavorites { get; set; }
        public virtual ICollection<EntertainmentRequest> EntertainmentRequests { get; set; }
        public virtual ICollection<ReportEntertainmentComment> ReportEntertainmentComments { get; set; }

        #endregion

        #region PropertiesManagement
        public virtual ICollection<ApartmentRentPay> ApartmentRentPays { get; set; }

        [InverseProperty(nameof(MaintenanceOrder.User))]
        public virtual ICollection<MaintenanceOrder> UserMaintenanceOrders { get; set; }

        [InverseProperty(nameof(MaintenanceOrder.Provider))]
        public virtual ICollection<MaintenanceOrder> ProviderMaintenanceOrders{ get; set; }

        public virtual ICollection<RentalManagementOrder> RentalManagementOrders { get; set; }  
        #endregion

        #endregion

    }
}
