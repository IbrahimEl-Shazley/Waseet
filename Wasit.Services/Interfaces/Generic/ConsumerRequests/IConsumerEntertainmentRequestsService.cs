using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentRentRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;

namespace Wasit.Services.Interfaces.Generic.ConsumerRequests
{
    public interface IConsumerEntertainmentRequestsService : IBaseService
    {
        Task<PageDTO<ConsumerEntertainmentCategoryRequestDto>> ListRequests(string userId, ListEntertainmentRequestsPayload model);
        Task<ExtendedConsumerEntertainmentCategoryRequestDto> RequestInfo(string userId, long requestId);
        Task<bool> CancelEntertainmentRequest(string userId, long requestId);
    }
}
