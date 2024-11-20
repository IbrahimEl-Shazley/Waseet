using Wasit.Core.Enums;
using Wasit.Services.ViewModels.Estates;
using Wasit.Services.ViewModels.Estates.MyEstates;
using Wasit.Services.ViewModels.Shared;

namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDEstateService
    {
        Task<List<EstateViewModel>> MyEstates(CategoryType category = CategoryType.Sale, long type = 0);
        Task<List<EstateViewModel>> UsersEstates(CategoryType category = CategoryType.Sale, long type = 0);
        Task<List<SharedViewModel>> EstateTypes(CategoryType category);
        Task<MyEstateDetailsViewModel> MyEstateDetails(long id, CategoryType category);
        Task<MyEstateDetailsViewModel> UserEstateDetails(long id, CategoryType category);
        Task<(bool isSuccess, string message)> AcceptEstate(long id, string userId, CategoryType category, double deposit);
        Task<(bool isSuccess, string message)> RejectEstate(long id, string userId, CategoryType category, string reason);
        Task<(bool isSuccess, string message)> ChangeVisibility(long id, CategoryType category);
        Task<(bool isSuccess, string message)> Remove(long id, CategoryType category);
        Task<(bool isSuccess, long estateId, long estateTypeId, string message)> Create(CreateEstateViewModel model);
        Task<(bool isSuccess, string message)> DeleteUsersPrices(long id);
        Task<List<SpecificationFormItemViewModel>> GetSpecsForm(long estateTypeId);
        Task<List<SpecificationFormItemViewModel>> GetEstateSpecsForm(long estateId);
        Task<(bool isSuccess, string message)> CreateSpecs(CreateUpdateEstateSpecsViewModel model);
        Task<(bool isSuccess, string message)> UpdateSpecs(CreateUpdateEstateSpecsViewModel model);
        Task<UpdateSaleEstateViewModel?> SaleEstateDetails(long id);
        Task<(bool isSuccess, long estateId, string message)> UpdateSaleEstate(UpdateSaleEstateViewModel model);
        Task<UpdateRentEstateViewModel?> RentEstateDetails(long id);
        Task<(bool isSuccess, long estateId, string message)> UpdateRentEstate(UpdateRentEstateViewModel model);
        Task<UpdateDailyRentEstateViewModel?> DailyRentEstateDetails(long id);
        Task<(bool isSuccess, long estateId, string message)> UpdateDailyRentEstate(UpdateDailyRentEstateViewModel model);
        Task<UpdateEntertainmentRentEstateViewModel?> EntertainmentRentEstateDetails(long id);
        Task<(bool isSuccess, long estateId, string message)> UpdateEntertainmentRentEstate(UpdateEntertainmentRentEstateViewModel model);
        Task<MyRequestPageViewModel> MyReservationRequests(CategoryType category, long estateId, ReservationStatus status);
        Task<MyRequestPageViewModel> MyEvaluationRequests(CategoryType category, long estateId, RatingStatus status);
        Task<MyRequestPageViewModel> MyPurchaseRequests(long estateId);
        Task<MyRequestPageViewModel> MyRentRequests(long estateId);
        Task<MyRequestPageViewModel> MyDailyRentRequests(long estateId, DailyRentStatus status);
        Task<MyRequestPageViewModel> MyEntertainmentRentRequests(long estateId, DailyRentStatus status);
        Task<MyRequestDetailsViewModel> MyRequestDetails(long requestId, CategoryType category, int requestType);
        Task<(bool isSuccess, string message)> AcceptPurchaseRequest(long id);
        Task<(bool isSuccess, string message)> RejectPurchaseRequest(long id);
        Task<(bool isSuccess, string message)> ConfirmEstateIsSold(long estateId, double price, long requestId);
        Task<(bool isSuccess, string message)> AcceptRentRequest(long requestId);
        Task<(bool isSuccess, string message)> RejectRentRequest(long requestId);
        Task<(bool isSuccess, string message)> ConfirmEstateRented(long estateId, long requestId);
    }
}
