using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;
using Wasit.Services.DTOs.Schema.Rent.RentRatingRequest;
using Wasit.Services.DTOs.Schema.Rent.RentRequest;
using Wasit.Services.DTOs.Schema.Rent.RentReservationRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.Interfaces.Generic.MyEstates
{
    public interface IRentEstatesService : IBaseService<RentEstate, RentEstateDto, CreateRentEstateDto, UpdateRentEstateDto>
    {
        Task<bool> AddNewRentEstate(string userId, CreateRentEstateDto model);
        Task<RentEstateInfoDto> MyRentEstateInfo(string userId, long estateId);
        Task<bool> EditRentEstate(string userId, UpdateRentEstateDto model);
        Task<IEnumerable<SpecificationValueDto>> GetSpecificationValues(string userId, long estateId);
        Task<PageDTO<RentReservationRequestDto>> ListRentReservationRequests(ListReservationRequestsPayload model);
        Task<RentReservationRequestInfoDto> ReservationRentRequestInfo(long requestId);
        Task<PageDTO<RentRatingRequestDto>> ListRentRatingRequests(ListRatingRequestsPayload model);
        Task<RentRatingRequestInfoDto> RentRatingRequestInfo(long requestId);
        Task<PageDTO<RentRequestDto>> ListRentRequests(ListRentRequestsPayload model);
        Task<RentRequestInfoDto> RentRequestInfo(long requestId);
        Task<bool> AcceptRentRequest(long requestId);
        Task<bool> RejectRentRequest(long requestId);
        Task<bool> ConfirmEstateRented(long estateId, long requestId);
    }
}
