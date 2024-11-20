using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.UserTables;
using Wasit.Services.DTOs.Schema.Shared;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.City;
using Wasit.Services.ViewModels.Region;
using Wasit.Services.ViewModels.Shared;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DRegionService : IDRegionService
    {
        private readonly ApplicationDbContext _context;
        public DRegionService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<RegionViewModel> Regions()
        {
            var regions = await _context.Regions
                .Select(x => new RegionItemViewModel
                {
                    Id = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    DateTime = x.CreatedOn.Value.ToString("dd-MM-yyyy"),
                }).ToListAsync();

            var data = new RegionViewModel
            {
                Regions = regions
            };

            return data;
        }


        public async Task<List<SharedViewModel>> Cities()
        {
            return await _context.Cities
                .Select(x => new SharedViewModel
                {
                    Id = x.Id,
                    Name = x.NameAr
                }).ToListAsync();
        }

        public async Task<(bool isSuccess, string message)> CreateRegion(CreateRegionViewModel model)
        {
            var region = new Region
            {
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                CityId = model.CityId,
                IsActive = true
            };

            _context.Regions.Add(region);
            await _context.SaveChangesAsync();
            return (true, "تمت الاضافة بنجاح");
        }

        public async Task<UpdateRegionViewModel?> RegionDetails(long id)
        {
            var model = await _context.Regions
                .Where(x => x.Id == id)
                .Select(x => new UpdateRegionViewModel
                {
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    CityId = x.CityId
                }).SingleOrDefaultAsync();

            return model;
        }

        public async Task<(bool isSuccess, string message)> UpdateRegion(long id, UpdateRegionViewModel model)
        {
            var region = await _context.Regions.FindAsync(id);

            if (region is null)
                return (false, "لم يتم العثور علي الحي");

            region.NameAr = model.NameAr;
            region.NameEn = model.NameEn;
            region.CityId = model.CityId;
            _context.Regions.Update(region);
            await _context.SaveChangesAsync();
            return (true, "تم التعديل بنجاح");
        }


        public async Task<(bool isSuccess, string message)> RemoveRegion(long id)
        {
            var region = await _context.Regions.SingleOrDefaultAsync(x => x.Id == id);
            if (region is null)
                return (false, "لم يتم العثور علي الحي");

            var hasUsers = _context.Users
         .Any(u => u.Region.Id == id);
            if (hasUsers)
            {
                return (false, "لايمكن حذف الحى");

            }

            region.IsDeleted = true;
            _context.Regions.Update(region);
            await _context.SaveChangesAsync();
            return (true, "تم الحذف بنجاح");
        }
    }
}
