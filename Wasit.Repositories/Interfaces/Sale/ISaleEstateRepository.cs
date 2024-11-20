using Wasit.Core.Entities.SaleEstateSection;

namespace Wasit.Repositories.Interfaces.Sale
{
    public interface ISaleEstateRepository : IBaseRepository<SaleEstate>
    {
        Task<bool> SaleEstateExists(string estateNumber);
        Task<bool> SaleEstateExists(string estateNumber, string userId);
    }
}
