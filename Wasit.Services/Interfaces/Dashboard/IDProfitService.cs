using Wasit.Core.Enums;
using Wasit.Services.ViewModels.Profits;
using Wasit.Services.ViewModels.Profits.AllPackages;

namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDProfitService
    {
        Task<List<ProfitViewModel>> Profits();
        Task<(bool isSuccess, string message)> Update(ServiceType type, double value);

        Task<List<PackageViewModel>> AddPriceToEstatesPackages();
        Task<UpdateAddPriceToEstatePackageViewModel?> AddPriceToEstatePackageDetails(long id);
        Task<(bool isSuccess, string message)> CreateAddPriceToEstatePackage(CreateAddPriceToEstatePackageViewModel model);
        Task<(bool isSuccess, string message)> UpdateAddPriceToEstatePackage(long id, UpdateAddPriceToEstatePackageViewModel model);
        Task<(bool isSuccess, string message)> DeleteAddPriceToEstatePackage(long id);

        Task<List<PackageViewModel>> BrokersPackages();
        Task<UpdateBrokerPackageViewModel?> BrokerPackageDetails(long id);
        Task<(bool isSuccess, string message)> CreateBrokerPackage(CreateBrokerPackageViewModel model);
        Task<(bool isSuccess, string message)> UpdateBrokerPackage(long id, UpdateBrokerPackageViewModel model);
        Task<(bool isSuccess, string message)> DeleteBrokerPackage(long id);
        
        Task<List<PackageViewModel>> DeveloperPackages();
        Task<UpdateDeveloperPackageViewModel?> DeveloperPackageDetails(long id);
        Task<(bool isSuccess, string message)> CreateDeveloperPackage(CreateDeveloperPackageViewModel model);
        Task<(bool isSuccess, string message)> UpdateDeveloperPackage(long id, UpdateDeveloperPackageViewModel model);
        Task<(bool isSuccess, string message)> DeleteDeveloperPackage(long id);

        Task<List<PackageViewModel>> ServicesPackages();
         Task<UpdateServicePackageViewModel?> ServicePackageDetails(long id);
        Task<(bool isSuccess, string message)> CreateServicePackage(CreateServicePackageViewModel model);
        Task<(bool isSuccess, string message)> UpdateServicePackage(long id, UpdateServicePackageViewModel model);
        Task<(bool isSuccess, string message)> DeleteServicePackage(long id);


    }
}
