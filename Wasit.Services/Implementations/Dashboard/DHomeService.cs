using Wasit.Context;
using Wasit.Core.Enums;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.Home;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DHomeService : IDHomeService
    {
        private readonly ApplicationDbContext _context;

        public DHomeService(ApplicationDbContext context)
        {
            _context = context;
        }


        public DashBoardHomeModel HomeIndex()
        {
            var data = (from st in _context.Settings
                        let OwnersCount = _context.Users.Where(x => x.UserType == nameof(UserType.Owner)).Count()
                        let DeleagtesCount = _context.Users.Where(x => x.UserType == nameof(UserType.Delegate)).Count()
                        let DevelopersCount = _context.Users.Where(x => x.UserType == nameof(UserType.Developer)).Count()
                        let BrokersCount = _context.Users.Where(x => x.UserType == nameof(UserType.Broker)).Count()
                        select new DashBoardHomeModel
                        {
                            OwnersCount = OwnersCount,
                            DelegatesCount = DeleagtesCount,
                            DevelopersCount = DevelopersCount,
                            BrokersCount = BrokersCount
                        }).FirstOrDefault();

            return data;
        }
    }
}
