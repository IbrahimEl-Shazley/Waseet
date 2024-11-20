using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Repositories.Interfaces.Daily;

namespace Wasit.Repositories.Implementations.Daily
{
    public class DailyRentEstateRepository : BaseRepository<DailyRentEstate>, IDailyRentEstateRepository
    {
        private readonly ApplicationDbContext _context;
        public DailyRentEstateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DailyRentEstateExists(string estateNumber)
        {
            return await _context.DailyRentEstates.AnyAsync(x => x.EstateNumber == estateNumber);
        }

        public async Task<bool> DailyRentEstateExists(string estateNumber, string userId)
        {
            return await _context.DailyRentEstates.AnyAsync(x => x.EstateNumber == estateNumber && x.UserId != userId);
        }
    }
}
