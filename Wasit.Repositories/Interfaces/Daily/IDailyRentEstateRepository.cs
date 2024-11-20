using Wasit.Core.Entities.DailyRentEstateSection;

namespace Wasit.Repositories.Interfaces.Daily
{
    public interface IDailyRentEstateRepository : IBaseRepository<DailyRentEstate>
    {
        Task<bool> DailyRentEstateExists(string estateNumber);
        Task<bool> DailyRentEstateExists(string estateNumber, string userId);
    }
}
