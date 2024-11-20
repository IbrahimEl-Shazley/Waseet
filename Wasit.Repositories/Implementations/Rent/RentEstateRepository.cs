using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Repositories.Interfaces.Rent;

namespace Wasit.Repositories.Implementations.Rent
{
    public class RentEstateRepository : BaseRepository<RentEstate>, IRentEstateRepository
    {
        private readonly ApplicationDbContext _context;

        public RentEstateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> RentEstateExists(string estateNumber)
        {
            return await _context.RentEstates.AnyAsync(x => x.EstateNumber == estateNumber);
        }

        public async Task<bool> RentEstateExists(string estateNumber, string userId)
        {
            return await _context.RentEstates.AnyAsync(x => x.EstateNumber == estateNumber && x.UserId != userId);
        }

    }
}
