using Wasit.Core.Enums;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.PropertiesManagement.RentManagement;

namespace Wasit.Services.Interfaces.Generic.PropertiesManagement
{
    public interface IRentManagementService : IBaseService
    {
        Task<bool> AddRentalManagementOrder(AddRentalMangementOrderPayload payload, string userId);
        Task<ManagedRentEstateInfoDto> EditRentalManagementOrder(long orderId, EditRentalMangementOrderPayload payload);
        Task<PageDTO<RentalManagementOrderItemDto>> ListMyManagedRentEstates(string userId, int pageNumber, RentalManagementOrderStatus status);
        Task<ManagedRentEstateInfoDto> ManagedRentEstateInfo(string userId, long orderId);
        Task<bool> CancelRentManagementOrder(string userId, long orderId);
        Task<bool> CancelContractForRentManagement(string userId, long orderId);
    }
}
