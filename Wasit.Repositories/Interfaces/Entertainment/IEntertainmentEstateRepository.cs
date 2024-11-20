using Wasit.Core.Entities.EntertainmentEstateSection;

namespace Wasit.Repositories.Interfaces.Entertainment
{
    public interface IEntertainmentEstateRepository : IBaseRepository<EntertainmentEstate>
    {
        Task<bool> EntertainmentEstateExists(string estateNumber);
        Task<bool> EntertainmentEstateExists(string estateNumber, string userId);
    }
}
