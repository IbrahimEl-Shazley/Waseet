using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities;
using Wasit.Core.Entities.AddPriceToEstate;
using Wasit.Core.Entities.BrokerEstateSection;
using Wasit.Core.Entities.Copon;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.DeveloperPackagesSection;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.Enum;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.EstateServiceSection;
using Wasit.Core.Entities.NOTIFIC;
using Wasit.Core.Entities.PropertiesManagement;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Entities.SettingTables;
using Wasit.Core.Entities.UserTables;

namespace Wasit.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationDbUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LogExption> LogExption { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        //public DbSet<ContactUsSubject> ContactUsSubjects { get; set; }
        public DbSet<DeviceId> DeviceIds { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AdminNotification> AdminNotifications { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Copon> Copon { get; set; }
        public DbSet<CoponUsed> CoponUsed { get; set; }
        public DbSet<Advertisment> Advertisments { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<QuestionsClient> QuestionsClient { get; set; }
        public DbSet<QuestionProvider> QuestionProvider { get; set; }
        //public DbSet<Favourite> Favourites { get; set; }
        public DbSet<SmsMessage> SmsMessages { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<HistoryNotify> HistoryNotify { get; set; }

        /// <summary>
        /// Section IntroductorySite
        /// </summary>
        public DbSet<IntroSetting> IntroSettings { get; set; }
        public DbSet<IntroContactUs> IntroContactUs { get; set; }
        public DbSet<CustomerOpinion> CustomerOpinions { get; set; }
        public DbSet<AppImg> AppImgs { get; set; }
        public DbSet<Advantage> Advantages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<EstateService> EstateServices { get; set; }


        #region notif
        public DbSet<NotificationCategory> NotificationCategory { get; set; }
        public DbSet<NotificationType> NotificationType { get; set; }
        public DbSet<NotificationQueue> NotificationQueue { get; set; }
        public DbSet<NotificationQueue> NotificationTemplate { get; set; }
        #endregion

        #region EstateCategories

        public DbSet<Category> Categories { get; set; }
        public DbSet<EstateType> EstateTypes { get; set; }
        public DbSet<CategoryEstateType> CategoryEstateTypes { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<EstateTypeSpecification> EstateTypeSpecifications { get; set; }

        #endregion

        #region SaleEstateSection 
        public DbSet<SaleEstate> SaleEstates { get; set; }
        public DbSet<SaleEstateImage> SaleEstateImages { get; set; }
        public DbSet<SaleEstateSpecificationValue> SaleEstateSpecificationValues { get; set; }
        public DbSet<PricingRequest> PricingRequests { get; set; }
        public DbSet<UserPriceToEstate> UserPriceToEstates { get; set; }
        public DbSet<SaleReservationRequest> SaleReservationRequests { get; set; }
        public DbSet<SaleRatingRequest> SaleRatingRequests { get; set; }
        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
        public DbSet<SaleEstateFavorite> SaleEstateFavorites { get; set; }
        #endregion

        #region RentEstateSection 
        public DbSet<RentEstate> RentEstates { get; set; }
        public DbSet<RentEstateImage> RentEstateImages { get; set; }
        public DbSet<RentEstateSpecificationValue> RentEstateSpecificationValues { get; set; }
        public DbSet<RentReservationRequest> RentReservationRequests { get; set; }
        public DbSet<RentRequest> RentRequests { get; set; }
        public DbSet<RentRatingRequest> RentRatingRequests { get; set; }
        public DbSet<RentEstateFavorite> RentEstateFavorites { get; set; }
        #endregion

        #region DailyRentEstateSection

        public DbSet<DailyRentEstate> DailyRentEstates { get; set; }
        public DbSet<DailyRentEstateImage> DailyRentEstateImages { get; set; }
        public DbSet<DailyRentEstateSpecificationValue> DailyRentEstateSpecificationValues { get; set; }
        public DbSet<DailyRentEstateFavorite> DailyRentEstateFavorites { get; set; }
        public DbSet<DailyRentRequest> DailyRentRequests { get; set; }
        public DbSet<ReportDailyRentComment> ReportDailyRentComments { get; set; }

        #endregion

        #region EntertainmentEstateSection
        public DbSet<EntertainmentEstate> EntertainmentEstates { get; set; }
        public DbSet<EntertainmentEstateImage> EntertainmentEstateImages { get; set; }
        public DbSet<EntertainmentEstateSpecificationValue> EntertainmentEstateSpecificationValues { get; set; }
        public DbSet<EntertainmentEstateFavorite> EntertainmentEstateFavorites { get; set; }
        public DbSet<EntertainmentRequest> EntertainmentRequests { get; set; }
        public DbSet<ReportEntertainmentComment> ReportEntertainmentComments { get; set; }
        #endregion

        #region AddPriceToEstate
        public DbSet<P4AddPriceToEstate> P4AddPriceToEstates { get; set; }
        public DbSet<S4AddPriceToEstate> S4AddPriceToEstates { get; set; }
        #endregion

        #region BrokerPackages
        public DbSet<P4ShowEstateService> P4ShowEstateServices { get; set; }
        public DbSet<S4ShowEstateService> S4ShowEstateServices { get; set; }
        #endregion

        #region BrokerPackages
        public DbSet<P4AddBrockerEstate> P4AddBrockerEstates { get; set; }
        public DbSet<S4AddBrockerEstate> S4AddBrockerEstates { get; set; }
        #endregion



        #region DeveloperPackages
        public DbSet<P4UpgradeDevAccount> P4UpgradeDevAccounts { get; set; }
        public DbSet<S4UpgradeDevAccount> S4UpgradeDevAccounts { get; set; }
        #endregion

        #region PropertiesManagement
        public DbSet<RentalManagementOrder> RentalManagementOrders { get; set; }
        public DbSet<EstateApartment> EstateApartments { get; set; }
        public DbSet<ApartmentRentPay> ApartmentRentPays { get; set; }
        public DbSet<MaintenanceOrder> MaintenanceOrders { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationDbUser>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<City>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<Region>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<SaleEstate>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<SaleRatingRequest>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<RentRatingRequest>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<EstateType>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<CategoryEstateType>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<EstateTypeSpecification>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<Specification>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<RentalManagementOrder>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<MaintenanceOrder>().HasQueryFilter(s => !s.IsDeleted);


            builder.Entity<Category>()
                .Property(c => c.Type).HasColumnType("tinyint");

        }

    }

}
