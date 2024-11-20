using Wasit.Core.Enums;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.PropertiesManagement.MaintainanceMangment;

namespace Wasit.Services.Interfaces.Generic.PropertiesManagement
{
    public interface IMaintainanceMangementService : IBaseService
    {
        Task<bool> AddMaintainanceOrder (string userId, AddMaintainanceOrderPayload maintainanceOrder);
        Task<PageDTO<MaintainanceOrderItemDto>> ListMyManagedMaintainanceEstates(string userId, int pageNumber, RentalManagementOrderStatus status);
        Task<bool> RateMaintainanceOrder(string userId, long orderId, double rating);
    }
}
