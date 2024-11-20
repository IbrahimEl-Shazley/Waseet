using Wasit.Core.Enums;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Sale.PurchaseRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;

namespace Wasit.Services.Interfaces.Generic.ConsumerRequests
{
    public interface IConsumerPurchaseRequestsService : IBaseService
    {
        Task<PageDTO<ConsumerPurchaseCategoryRequestDto>> ListRequests(string userId, ListSaleRequestsPayload model);
        Task<ExtendedConsumerPurchaseRequestInfoDto> PurchaseRequestInfo(string userId, long requestId);
        Task<ExtendedConsumerRatingRequestInfoForSaleEstateDto> RatingRequestInfo(string userId, long requestId);
        Task<ExtendedConsumerReservationRequestInfoForSaleEstateDto> ReservationRequestInfo(string userId, long requestId);
        Task<bool> CancelPurchaseRequest(string userId, long requestId);
        Task<bool> PayDeposit(string userId, long requestId, TypePay paymentMethod);
        Task<bool> ConfirmRecievingSaleEstate(string userId, long requestId);
        Task<bool> AddRefundRequest(string userId, long requestId, string reason);
    }
}
