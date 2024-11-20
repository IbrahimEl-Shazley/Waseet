using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Repositories.Interfaces.Sale;

namespace Wasit.Repositories.Implementations.Sale
{
    public class SaleEstateRepository : BaseRepository<SaleEstate>, ISaleEstateRepository
    {
        private readonly ApplicationDbContext _context;
        public SaleEstateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<bool> SaleEstateExists(string estateNumber)
        {
            return await _context.SaleEstates.AnyAsync(x => x.EstateNumber == estateNumber);
        }

        public async Task<bool> SaleEstateExists(string estateNumber, string userId)
        {
            return await _context.SaleEstates.AnyAsync(x => x.EstateNumber == estateNumber && x.UserId != userId);
        }

    }
}
