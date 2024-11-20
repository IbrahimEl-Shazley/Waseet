using Wasit.Core.Entities.RentEstateSection;

namespace Wasit.Repositories.Interfaces.Rent
{
    public interface IRentEstateRepository : IBaseRepository<RentEstate>
    {
        Task<bool> RentEstateExists(string estateNumber);
        Task<bool> RentEstateExists(string estateNumber, string userId);
    }
}
