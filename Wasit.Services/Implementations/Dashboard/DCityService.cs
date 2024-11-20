using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.UserTables;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.City;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DCityService : IDCityService
    {
        private readonly ApplicationDbContext _context;
        public DCityService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<CityViewModel> Cities()
        {
            var cities = await _context.Cities
                .Select(x => new CityItemViewModel
                {
                    Id = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    DateTime = x.CreatedOn.Value.ToString("dd-MM-yyyy"),
                }).ToListAsync();

            var data = new CityViewModel
            {
                Cities = cities
            };

            return data;
        }


        public async Task<(bool isSuccess, string message)> CreateCity(CreateCityViewModel model)
        {
            var city = new City
            {
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                IsActive = true
            };

            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return (true, "تمت الاضافة بنجاح");
        }

        public async Task<UpdateCityViewModel?> CityDetails(long id)
        {
            var model = await _context.Cities
                .Where(x => x.Id == id)
                .Select(x => new UpdateCityViewModel
                {
                    NameAr = x.NameAr,
                    NameEn = x.NameEn
                }).SingleOrDefaultAsync();

            return model;
        }

        public async Task<(bool isSuccess, string message)> UpdateCity(long id, UpdateCityViewModel model)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city is null)
                return (false, "لم يتم العثور علي المدينة");

            city.NameAr = model.NameAr;
            city.NameEn = model.NameEn;
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
            return (true, "تم التعديل بنجاح");
        }


        public async Task<(bool isSuccess, string message)> RemoveCity(long id)
        {
            var city = await _context.Cities.SingleOrDefaultAsync(x => x.Id == id);
            if (city is null)
                return (false, "لم يتم العثور علي المدينة");


            var hasUsers = _context.Users
             .Any(u => u.Region.CityId == id);  

        
            if (hasUsers)
            {
                return (false, "لايمكن حذف المدينة");
            }

            city.IsDeleted = true;
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
            return (true, "تم الحذف بنجاح");
        }
    }
}
