using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Repositories.Interfaces.Entertainment;

namespace Wasit.Repositories.Implementations.Entertainment
{
    public class EntertainmentEstateRepository : BaseRepository<EntertainmentEstate>, IEntertainmentEstateRepository
    {
        private readonly ApplicationDbContext _context;

        public EntertainmentEstateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<bool> EntertainmentEstateExists(string estateNumber)
        {
            return await _context.EntertainmentEstates.AnyAsync(x => x.EstateNumber == estateNumber);
        }


        public async Task<bool> EntertainmentEstateExists(string estateNumber, string userId)
        {
            return await _context.EntertainmentEstates.AnyAsync(x => x.EstateNumber == estateNumber && x.UserId != userId);
        }
    }
}
