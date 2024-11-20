using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.Interfaces.Generic.MyEstates
{
    public interface IGeneralMySharedEstatesService : IBaseService
    {
        Task<IEnumerable<SpecificationFormItemDto>> GetSpecsForm(long estateTypeId);

        Task<bool> CheckReservationRequests();
        Task<bool> CheckShortTermRentRequests();
        Task<bool> CheckApartmentsRentStatus();
        Task<bool> CheckUnAcceptedEvaluationRequests();
        Task<bool> CheckLongTermRentRequests();
        Task<bool> CheckAllowedBookingPeriodForShortRentEstates();
    }
}
