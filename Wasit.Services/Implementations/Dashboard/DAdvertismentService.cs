using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.SettingTables;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.IO;
using Wasit.Core.Models.IO;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.Advertisments;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DAdvertismentService : IDAdvertismentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDSharedService _sharedService;
        public DAdvertismentService(ApplicationDbContext context, IDSharedService sharedService)
        {
            _context = context;
            _sharedService = sharedService;
        }


        public async Task<List<AdvertismentViewModel>> Advertisments()
        {
            return await _context.Advertisments
                .AsNoTracking()
                .Select(x => new AdvertismentViewModel
                {
                    Id = x.Id,
                    Image = MyConstants.DomainUrl + x.Image
                }).ToListAsync();
        }


        public async Task<(bool isSuccess, string message)> CreateAdvertisment(CreateAdvertismentViewModel model)
        {
            var imagevalidation = IOHelper.ValidateImage(model.Image);
            if (imagevalidation == string.Empty)
            {
                var advertisment = new Advertisment
                {
                    Image = await _sharedService.UploadFileUsingApi(new UploadImageUsingApiDto
                    {
                        fileName = (int)FileName.Advertisment,
                        image = model.Image
                    }),
                    IsActive = true
                };
                _context.Advertisments.Add(advertisment);
                await _context.SaveChangesAsync();
                return (true, string.Empty);
            }
            else
            {
                return (false, imagevalidation);
            }
        }


        public async Task<(bool isSuccess, string message)> Remove(long id)
        {
            try
            {
                var ad = await _context.Advertisments
                    .SingleOrDefaultAsync(x => x.Id == id);

                if (ad is null)
                    return (false, "هذا الاعلان غير موجود");

                _context.Advertisments.Remove(ad);
                await _context.SaveChangesAsync();

                return (true, "تم الحذف بنجاح");
            }
            catch
            {
                return (false, "حدث خطأ ما");
            }
        }
    }
}
