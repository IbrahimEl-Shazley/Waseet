using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Sale.PurchaseRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;
using Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.Interfaces.Generic.MyEstates
{
    public interface ISaleEstatesService : IBaseService<SaleEstate, SaleEstateDto, CreateSaleEstateDto, UpdateSaleEstateDto>
    {
        Task<bool> AddNewSaleEstate(string userId, CreateSaleEstateDto model);
        Task<bool> EditSaleEstate(string userId, UpdateSaleEstateDto model);
        Task<IEnumerable<SpecificationValueDto>> GetSpecificationValues(string userId, long estateId);
        Task<SaleEstateInfoDto> MySaleEstateInfo(string userId, long estateId);
        Task<PageDTO<SaleReservationRequestDto>> ListSaleReservationRequests(ListReservationRequestsPayload model);
        Task<SaleReservationRequestInfoDto> ReservationSaleRequestInfo(long requestId);
        Task<PageDTO<SaleRatingRequestDto>> ListSaleRatingRequests(ListRatingRequestsPayload model);
        Task<SaleRatingRequestInfoDto> SaleRatingRequestInfo(long requestId);
        Task<PageDTO<PurchaseRequestDto>> ListPurchaseRequests(ListPurchaseRequestsPayload model);
        Task<PurchaseRequestInfoDto> PurchaseRequestInfo(long requestId);
        Task<bool> AcceptPurchaseRequest(long requestId);
        Task<bool> RejectPurchaseRequest(long requestId);
        Task<bool> ConfirmEstateIsSold(long estateId, double price, long requestId);
        Task<bool> AddPricingRequest(string userId, long estateId, TypePay paymentMethod);
    }
}
