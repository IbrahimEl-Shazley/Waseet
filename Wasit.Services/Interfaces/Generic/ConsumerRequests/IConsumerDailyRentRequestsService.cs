using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using ListDailyRentRequestsPayload = Wasit.Services.DTOs.Schema.Shared.ConsumerRequests.ListDailyRentRequestsPayload;

namespace Wasit.Services.Interfaces.Generic.ConsumerRequests
{
    public interface IConsumerDailyRentRequestsService : IBaseService
    {
        Task<PageDTO<ConsumerDailyRentCategoryRequestDto>> ListRequests(string userId, ListDailyRentRequestsPayload model);
        Task<ExtendedConsumerDailyRentCategoryRequestDto> RequestInfo(string userId, long requestId);
        Task<bool> CancelDailyRentRequest(string userId, long requestId);
    }
}
