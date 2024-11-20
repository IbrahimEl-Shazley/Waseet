using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Enums;
using Wasit.Core.Helpers.General;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.ContactUs;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DContactUsService : IDContactUsService
    {
        private readonly ApplicationDbContext _context;

        public DContactUsService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<ContactUsViewModel>> ContactUsMessages()
        {
            return await _context.ContactUs
                .AsNoTracking()
                .Select(x => new ContactUsViewModel
                {
                    Id = x.Id,
                    Name = x.UserName,
                    Date = x.CreatedOn.Value.ToString("dd-MM-yyyy"),
                    Phone = x.Phone,
                    Subject = EnumHelper.ContactUsSubjectName(x.Subject, Language.Ar),
                    Message = x.Msg
                }).OrderByDescending(x => x.Id)
                .ToListAsync();
        }
    }
}
