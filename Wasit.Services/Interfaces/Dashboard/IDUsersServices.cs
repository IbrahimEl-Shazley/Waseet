using Wasit.Core.Entities.UserTables;
using Wasit.Services.ViewModels.Users;

namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDUsersServices
    {
        Task<List<OwnerViewModel>> Owners();
        Task<List<BrokerViewModel>> Brokers();
        Task<List<DelegateViewModel>> Delegates();
        Task<List<DeveloperViewModel>> Developers();


        Task<ApplicationDbUser?> GetUserById(string userId);
        Task<AdditionalOwnerInfoViewModel> AdditionalOwnerInfo(string id);
        Task<IndividualBrokerViewModel> AdditionalIndividualBrokerInfo(string id);
        Task<FaciltyBrokerViewModel> AdditionalFaciltyBrokerInfo(string id);
        Task<AdditionalDelegateInfoViewModel> AdditionalDelegateInfo(string id);
        Task<AdditionalDeveloperInfoViewModel> AdditionalDeveloperInfo(string id);
        Task<(bool isSuccess, string message)> ChangeActivation(string userId);
        Task<(bool isSuccess, string message)> Remove(string userId);
        Task<(bool isSuccess, string message)> SendNotification(string userId, string title, string content);
    }
}
