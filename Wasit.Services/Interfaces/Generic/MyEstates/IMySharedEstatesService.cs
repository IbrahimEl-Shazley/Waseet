using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.Interfaces.Generic.MyEstates
{
    public interface IMySharedEstatesService : IBaseService
    {
        Task<bool> ConfirmEstateDeedNumber(string estateDeedNumber);
        Task<dynamic> GetEstateTypes();
        Task<dynamic> ListMyEstates(string userId, int pageNumber);
        Task<bool> ChangeEstateVisibility(long estateId);
        Task<bool> RemoveEstate(long estateId);
        Task<bool> ReportComment(string userId, long requestId, string reason);
        Task<PageDTO<BaseRatingItemDto>> ListEstateRatings(string userId, long estateId, int pageNumber);
        Task<bool> SelectReservationPeriod(string userId, long estateId, DateTime startDate, DateTime endDate);
        Task<bool> RateRentee(string userId, long requestId, double rating);
        Task<string> DownloadFinancialAccounts(string userId, long estateId, DateTime startDate, DateTime endDate);
    }
}
