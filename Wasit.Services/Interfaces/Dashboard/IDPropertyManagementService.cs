using Wasit.Core.Enums;
using Wasit.Services.ViewModels.PropertyManagement.MaintainanceMangement;
using Wasit.Services.ViewModels.PropertyManagement.RentManagement;

namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDPropertyManagementService
    {
        Task<List<RentManagementRequestViewModel>> RentManagementRequests();
        Task<(bool isSuccess, string message)> AcceptRentManagementRequest(long id);
        Task<(bool isSuccess, string message)> RejectRentManagementRequest(long id);
        Task<RentManagementRequestDetailsViewModel?> RentManagementRequestDetails(long id);
        Task<List<MaintainanceManagementRequestViewModel>> MaintainanceManagementRequests(RentalManagementOrderStatus status);
    }
}
