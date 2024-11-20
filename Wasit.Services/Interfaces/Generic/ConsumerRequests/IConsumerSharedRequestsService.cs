using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;

namespace Wasit.Services.Interfaces.Generic.ConsumerRequests
{
    public interface IConsumerSharedRequestsService : IBaseService
    {
        Task<bool> RateOwnerAndEstate(string userId, RateOwnerAndEstatePayload payload);
        Task<bool> RateReviewer(string userId, long requestId, double rating);
    }
}
