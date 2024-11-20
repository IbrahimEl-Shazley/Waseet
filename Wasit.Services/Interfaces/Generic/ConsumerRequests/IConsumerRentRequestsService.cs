using Wasit.Core.Enums;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Rent.RentRatingRequest;
using Wasit.Services.DTOs.Schema.Rent.RentRequest;
using Wasit.Services.DTOs.Schema.Rent.RentReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using ListRentRequestsPayload = Wasit.Services.DTOs.Schema.Shared.ConsumerRequests.ListRentRequestsPayload;

namespace Wasit.Services.Interfaces.Generic.ConsumerRequests
{
    public interface IConsumerRentRequestsService : IBaseService
    {
        Task<PageDTO<ConsumerRentCategoryRequestDto>> ListRequests(string userId, ListRentRequestsPayload model);
        Task<ExtendedConsumerRentRequestInfoDto> RentRequestInfo(string userId, long requestId);
        Task<ExtendedConsumerRatingRequestInfoForRentEstateDto> RatingRequestInfo(string userId, long requestId);
        Task<ExtendedConsumerReservationRequestInfoForRentEstateDto> ReservationRequestInfo(string userId, long requestId);
        Task<bool> CancelRentRequest(string userId, long requestId);
        Task<bool> PayForRent(string userId, long requestId, TypePay paymentMethod);
        Task<bool> ConfirmRecievingRentEstate(string userId, long requestId);
        Task<bool> RateOwner(string userId,long requestId, double rating);
    }
}
