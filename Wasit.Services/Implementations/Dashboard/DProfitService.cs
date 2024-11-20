using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.AddPriceToEstate;
using Wasit.Core.Entities.BrokerEstateSection;
using Wasit.Core.Entities.DeveloperPackagesSection;
using Wasit.Core.Entities.EstateServiceSection;
using Wasit.Core.Enums;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.Profits;
using Wasit.Services.ViewModels.Profits.AllPackages;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DProfitService : IDProfitService
    {
        private readonly ApplicationDbContext _context;

        public DProfitService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<ProfitViewModel>> Profits()
        {
            var data = await _context.Services
                .AsNoTracking()
                .Select(x => new ProfitViewModel
                {
                    Name = x.ServiceName,
                    Cost = x.ServiceCost,
                    ServiceType = x.Type,
                    DisplayType = x.DisplayType
                }).ToListAsync();

            return data;
        }

        public async Task<(bool isSuccess, string message)> Update(ServiceType type, double value)
        {
            await _context.Services
                .Where(x => x.Type == type)
                 .ExecuteUpdateAsync(x =>
                     x.SetProperty(x => x.ServiceCost, value));

            return (true, "تم تعديل القيمة بنجاح");
        }


        #region BrokerPackages
        public async Task<List<PackageViewModel>> BrokersPackages()
        {
            var data = await _context.P4AddBrockerEstates
                .Select(x => new PackageViewModel
                {
                    Id = x.Id,
                    Name = x.NameAr,
                    Period = x.SubscriptionDays,
                    MaxUsageCount = x.EstatesCount,
                    Price = x.Price,
                    Details = x.DescriptionAr
                }).ToListAsync();

            return data;
        }

        public async Task<UpdateBrokerPackageViewModel?> BrokerPackageDetails(long id)
        {
            var data = await _context.P4AddBrockerEstates
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Select(x => new UpdateBrokerPackageViewModel
                {
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    Duration = x.SubscriptionDays,
                    MaxUsageCount = x.EstatesCount,
                    Price = x.Price,
                    DetailsAr = x.DescriptionAr,
                    DetailsEn = x.DescriptionEn
                }).SingleOrDefaultAsync();

            return data;
        }


        public async Task<(bool isSuccess, string message)> CreateBrokerPackage(CreateBrokerPackageViewModel model)
        {
            var package = new P4AddBrockerEstate
            {
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                DescriptionAr = model.DetailsAr,
                DescriptionEn = model.DetailsEn,
                SubscriptionDays = model.Duration,
                EstatesCount = model.MaxUsageCount,
                Price = model.Price,
                IsActive = true
            };
            _context.P4AddBrockerEstates.Add(package);
            await _context.SaveChangesAsync();
            return (true, "تمت الاضافة بنجاح");
        }


        public async Task<(bool isSuccess, string message)> UpdateBrokerPackage(long id, UpdateBrokerPackageViewModel model)
        {
            var package = await _context.P4AddBrockerEstates.FindAsync(id);

            if (package is null)
                return (false, "لم يتم العثور علي الباقة");

            package.NameAr = model.NameAr;
            package.NameEn = model.NameEn;
            package.Price = model.Price;
            package.EstatesCount = model.MaxUsageCount;
            package.SubscriptionDays = model.Duration;
            package.DescriptionAr = model.DetailsAr;
            package.DescriptionEn = model.DetailsEn;

            _context.P4AddBrockerEstates.Update(package);
            await _context.SaveChangesAsync();
            return (true, "تم التعديل بنجاح");
        }


        public async Task<(bool isSuccess, string message)> DeleteBrokerPackage(long id)
        {
            var package = await _context.P4AddBrockerEstates.FindAsync(id);

            if (package is null)
                return (false, "لم يتم العثور علي الباقة");

            _context.P4AddBrockerEstates.Remove(package);
            await _context.SaveChangesAsync();
            return (true, "تم الحذف بنجاح");
        }
        #endregion


        #region AddPriceToEstatePackage
        public async Task<List<PackageViewModel>> AddPriceToEstatesPackages()
        {
            var data = await _context.P4AddPriceToEstates
                .Select(x => new PackageViewModel
                {
                    Id = x.Id,
                    Name = x.NameAr,
                    Period = x.SubscriptionDays,
                    MaxUsageCount = x.AddPriceCount,
                    Price = x.Price,
                    Details = x.DescriptionAr
                }).ToListAsync();

            return data;
        }


        public async Task<UpdateAddPriceToEstatePackageViewModel?> AddPriceToEstatePackageDetails(long id)
        {
            var data = await _context.P4AddPriceToEstates
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Select(x => new UpdateAddPriceToEstatePackageViewModel
                {
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    Duration = x.SubscriptionDays,
                    MaxUsageCount = x.AddPriceCount,
                    Price = x.Price,
                    DetailsAr = x.DescriptionAr,
                    DetailsEn = x.DescriptionEn
                }).SingleOrDefaultAsync();

            return data;
        }


        public async Task<(bool isSuccess, string message)> CreateAddPriceToEstatePackage(CreateAddPriceToEstatePackageViewModel model)
        {
            var package = new P4AddPriceToEstate
            {
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                DescriptionAr = model.DetailsAr,
                DescriptionEn = model.DetailsEn,
                SubscriptionDays = model.Duration,
                AddPriceCount = model.MaxUsageCount,
                Price = model.Price,
                IsActive = true
            };
            _context.P4AddPriceToEstates.Add(package);
            await _context.SaveChangesAsync();
            return (true, "تمت الاضافة بنجاح");
        }
        
        
        public async Task<(bool isSuccess, string message)> UpdateAddPriceToEstatePackage(long id, UpdateAddPriceToEstatePackageViewModel model)
        {
            var package = await _context.P4AddPriceToEstates.FindAsync(id);

            if (package is null)
                return (false, "لم يتم العثور علي الباقة");

            package.NameAr = model.NameAr;
            package.NameEn = model.NameEn;
            package.Price = model.Price;
            package.AddPriceCount = model.MaxUsageCount;
            package.SubscriptionDays = model.Duration;
            package.DescriptionAr = model.DetailsAr;
            package.DescriptionEn = model.DetailsEn;

            _context.P4AddPriceToEstates.Update(package);
            await _context.SaveChangesAsync();
            return (true, "تم التعديل بنجاح");
        }
        
        
        public async Task<(bool isSuccess, string message)> DeleteAddPriceToEstatePackage(long id)
        {
            var package = await _context.P4AddPriceToEstates.FindAsync(id);

            if (package is null)
                return (false, "لم يتم العثور علي الباقة");

            _context.P4AddPriceToEstates.Remove(package);
            await _context.SaveChangesAsync();
            return (true, "تم الحذف بنجاح");
        }
        #endregion


        #region DeveloperPackages
        public async Task<List<PackageViewModel>> DeveloperPackages()
        {
            var data = await _context.P4UpgradeDevAccounts
                .Select(x => new PackageViewModel
                {
                    Id = x.Id,
                    Name = x.NameAr,
                    Period = x.SubscriptionDays,
                    Price = x.Price,
                    Details = x.DescriptionAr
                }).ToListAsync();

            return data;
        }

        public async Task<UpdateDeveloperPackageViewModel?> DeveloperPackageDetails(long id)
        {
            var data = await _context.P4UpgradeDevAccounts
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Select(x => new UpdateDeveloperPackageViewModel
                {
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    Duration = x.SubscriptionDays,
                    Price = x.Price,
                    DetailsAr = x.DescriptionAr,
                    DetailsEn = x.DescriptionEn
                }).SingleOrDefaultAsync();

            return data;
        }


        public async Task<(bool isSuccess, string message)> CreateDeveloperPackage(CreateDeveloperPackageViewModel model)
        {
            var package = new P4UpgradeDevAccount
            {
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                DescriptionAr = model.DetailsAr,
                DescriptionEn = model.DetailsEn,
                SubscriptionDays = model.Duration,
                Price = model.Price,
                IsActive = true
            };
            _context.P4UpgradeDevAccounts.Add(package);
            await _context.SaveChangesAsync();
            return (true, "تمت الاضافة بنجاح");
        }


        public async Task<(bool isSuccess, string message)> UpdateDeveloperPackage(long id, UpdateDeveloperPackageViewModel model)
        {
            var package = await _context.P4UpgradeDevAccounts.FindAsync(id);

            if (package is null)
                return (false, "لم يتم العثور علي الباقة");

            package.NameAr = model.NameAr;
            package.NameEn = model.NameEn;
            package.Price = model.Price;
            package.SubscriptionDays = model.Duration;
            package.DescriptionAr = model.DetailsAr;
            package.DescriptionEn = model.DetailsEn;

            _context.P4UpgradeDevAccounts.Update(package);
            await _context.SaveChangesAsync();
            return (true, "تم التعديل بنجاح");
        }


        public async Task<(bool isSuccess, string message)> DeleteDeveloperPackage(long id)
        {
            var package = await _context.P4UpgradeDevAccounts.FindAsync(id);

            if (package is null)
                return (false, "لم يتم العثور علي الباقة");

            _context.P4UpgradeDevAccounts.Remove(package);
            await _context.SaveChangesAsync();
            return (true, "تم الحذف بنجاح");
        }
        #endregion

        #region DeveloperPackages
        public async Task<List<PackageViewModel>> ServicesPackages()
        {
            var data = await _context.P4ShowEstateServices
                .Select(x => new PackageViewModel
                {
                    Id = x.Id,
                    Name = x.NameAr,
                    Period = x.SubscriptionDays,
                    Price = x.Price,
                    Details = x.DescriptionAr
                }).ToListAsync();

            return data;
        }

        public async Task<UpdateServicePackageViewModel?> ServicePackageDetails(long id)
        {
            var data = await _context.P4ShowEstateServices
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Select(x => new UpdateServicePackageViewModel
                {
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    Duration = x.SubscriptionDays,
                    Price = x.Price,
                    DetailsAr = x.DescriptionAr,
                    DetailsEn = x.DescriptionEn
                }).SingleOrDefaultAsync();

            return data;
        }


        public async Task<(bool isSuccess, string message)> CreateServicePackage(CreateServicePackageViewModel model)
        {
            var package = new P4ShowEstateService
            {
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                DescriptionAr = model.DetailsAr,
                DescriptionEn = model.DetailsEn,
                SubscriptionDays = model.Duration,
                Price = model.Price,
                IsActive = true
            };
            _context.P4ShowEstateServices.Add(package);
            await _context.SaveChangesAsync();
            return (true, "تمت الاضافة بنجاح");
        }


        public async Task<(bool isSuccess, string message)> UpdateServicePackage(long id, UpdateServicePackageViewModel model)
        {
            var package = await _context.P4ShowEstateServices.FindAsync(id);

            if (package is null)
                return (false, "لم يتم العثور علي الباقة");

            package.NameAr = model.NameAr;
            package.NameEn = model.NameEn;
            package.Price = model.Price;
            package.SubscriptionDays = model.Duration;
            package.DescriptionAr = model.DetailsAr;
            package.DescriptionEn = model.DetailsEn;

            _context.P4ShowEstateServices.Update(package);
            await _context.SaveChangesAsync();
            return (true, "تم التعديل بنجاح");
        }


        public async Task<(bool isSuccess, string message)> DeleteServicePackage(long id)
        {
            var package = await _context.P4ShowEstateServices.FindAsync(id);

            if (package is null)
                return (false, "لم يتم العثور علي الباقة");

            _context.P4ShowEstateServices.Remove(package);
            await _context.SaveChangesAsync();
            return (true, "تم الحذف بنجاح");
        }
        #endregion
    }
}
