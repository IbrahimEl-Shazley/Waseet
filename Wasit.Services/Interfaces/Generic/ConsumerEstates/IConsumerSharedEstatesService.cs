using Wasit.Core.Enums;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.Interfaces.Generic.ConsumerEstates
{
    public interface IConsumerSharedEstatesService : IBaseService
    {
        Task<dynamic> ListEstates(string userId, ListEstatesPayload payload);
        Task<bool> AddOrRemoveFavouriteEstate(string userId, long estateId);
        Task<bool> AssignDelegate(long estateId, TypePay paymentMethod, string userId);
        Task<bool> AddReservationRequest(Language lang, long estateId, TypePay paymentMethod, string userId);
        Task<PageDTO<BaseRatingItemDto>> AllUserRatingsOnEstate(long estateId, int pageNumber);
        Task<bool> ReportComment(string userId, long requestId, string reason);
        Task<List<string>> ReservedDays(long estateId);
    }
}
