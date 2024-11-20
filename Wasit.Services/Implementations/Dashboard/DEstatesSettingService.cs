using AAITHelper.Enums;
using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Core.Helpers.IO;
using Wasit.Core.Models.IO;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.Interfaces.General;
using Wasit.Services.ViewModels.EstateSettings;
using Wasit.Services.ViewModels.Shared;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DEstatesSettingService : IDEstatesSettingService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly IDSharedService _sharedService;

        public DEstatesSettingService(ApplicationDbContext context, INotificationService notificationService, IDSharedService sharedService)
        {
            _context = context;
            _notificationService = notificationService;
            _sharedService = sharedService;
        }



        public async Task<List<EstateTypeViewModel>> EstateTypes(long categoryId)
        {
            var data = await _context.EstateTypes
                .Include(x => x.CategoryEstateTypes)
                .Where(x => categoryId == 0 || x.CategoryEstateTypes.Any(y => y.CategoryId == categoryId))
                .AsNoTracking().OrderByDescending(x => x.CreatedOn)
                .Select(x => new EstateTypeViewModel
                {
                    Id = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    Categories = string.Join(" - ", x.CategoryEstateTypes.Select(c => c.Category.NameAr).ToList())
                }).ToListAsync();

            return data;
        }


        public async Task<(bool isSuccess, string message)> CreateEstateType(CreateEstateTypeViewModel model)
        {
            if (model.Categories.Count == 0)
                return (false, "يرجي اختيار قسم واحد علي الاقل");

            if (model.Specifications.Count == 0)
                return (false, "يرجي اختيار خاصية واحدة علي الاقل");

            var categories = new List<CategoryEstateType>();
            foreach (var category in model.Categories)
            {
                var id = await _context.Categories.AsNoTracking().Where(x => x.Type == category).Select(x => x.Id).SingleOrDefaultAsync();
                categories.Add(new CategoryEstateType
                {
                    CategoryId = id
                });
            }

            var specs = new List<EstateTypeSpecification>();
            foreach (var spec in model.Specifications)
            {
                specs.Add(new EstateTypeSpecification
                {
                    SpecificationId = spec.Id,
                    IsRequired = spec.IsRequired
                });
            }

            var estateType = new EstateType
            {
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                IsActive = true,
                EstateTypeSpecifications = specs,
                CategoryEstateTypes = categories,
                CreatedOn=DateTime.Now
            };

            _context.EstateTypes.Add(estateType);
            await _context.SaveChangesAsync();

            return (true, string.Empty);
        }


        public async Task<UpdateEstateTypeViewModel?> EstateTypeById(long id)
        {
            var type = await _context.EstateTypes
               .AsNoTracking()
               .Where(x => x.Id == id)
               .Select(x => new UpdateEstateTypeViewModel
               {
                   Id = x.Id,
                   NameAr = x.NameAr,
                   NameEn = x.NameEn,
                   Categories = x.CategoryEstateTypes.Select(c => Convert.ToInt64(c.Category.Type)).ToList(),
                   Specifications = x.EstateTypeSpecifications.Select(s => new UpdateEstateTypeSpecificationViewModel
                   {
                       Id = s.SpecificationId,
                       Name = s.Specification.NameAr,
                       IsRequired = s.IsRequired
                   }).ToList(),
                   SelectedSpecs = x.EstateTypeSpecifications.Select(s => s.SpecificationId).ToList()
               }).SingleOrDefaultAsync();

            if (type is null)
                return null;

            return type;
        }

        public async Task<(bool isSuccess, string message)> EditEstateType(long id, UpdateEstateTypeViewModel model)
        {
            try
            {
                var estateType = await _context.EstateTypes
                    .Include(x => x.CategoryEstateTypes)
                    .Include(x => x.EstateTypeSpecifications)
                    .SingleOrDefaultAsync(x => x.Id == id);

                if (estateType is null)
                    return (false, "هذا النوع غير موجود");

                if (model.Categories.Count == 0)
                    return (false, "يرجي اختيار قسم واحد علي الاقل");

                if (model.Specifications.Count == 0)
                    return (false, "يرجي اختيار خاصية واحدة علي الاقل");

                foreach (var category in estateType.CategoryEstateTypes)
                {
                    category.IsDeleted = true;
                }
                await _context.SaveChangesAsync();

                var categories = new List<CategoryEstateType>();
                foreach (var categoryId in model.Categories)
                {
                   var Id=await _context.Categories.Where(x => x.Type == (CategoryType)categoryId).Select(x => x.Id).SingleOrDefaultAsync();
                    categories.Add(new CategoryEstateType
                    {
                        CategoryId = Id,
                        EstateTypeId = estateType.Id

                    });
                }
                _context.CategoryEstateTypes.AddRange(categories);
                await _context.SaveChangesAsync();

                foreach (var spec in estateType.EstateTypeSpecifications)
                {
                    spec.IsDeleted = true;
                }
                await _context.SaveChangesAsync();

                var specs = new List<EstateTypeSpecification>();
                foreach (var spec in model.Specifications)
                {
                    specs.Add(new EstateTypeSpecification
                    {
                        SpecificationId = spec.Id,
                        IsRequired = spec.IsRequired,
                        EstateTypeId = estateType.Id,
                        IsDeleted = false

                    });
                }
                _context.EstateTypeSpecifications.AddRange(specs);
                await _context.SaveChangesAsync();

                estateType.NameAr = model.NameAr;
                estateType.NameEn = model.NameEn;


                _context.EstateTypes.Update(estateType);
                await _context.SaveChangesAsync();
                return (true, "تم الحفظ بنجاح");

            }
            catch (Exception ex)
            {
                return (false, "حدث خطا");
            }
        }


        public async Task<EstateTypeSpecificationsPageViewModel?> EstateTypeSpecifications(long estateTypeId)
        {
            var estateType = await _context.EstateTypes
                .AsNoTracking()
                .Include(x => x.EstateTypeSpecifications)
                .ThenInclude(x => x.Specification)
                .SingleOrDefaultAsync(x => x.Id == estateTypeId);

            var categories = await _context.CategoryEstateTypes
                .AsNoTracking()
                .Where(x => x.EstateTypeId == estateTypeId)
                .Select(x => x.Category.NameAr)
                .ToListAsync();

            if (estateType is null)
                return null;

            var data = new EstateTypeSpecificationsPageViewModel
            {
                Id = estateType.Id,
                NameAr = estateType.NameAr,
                NameEn = estateType.NameEn,
                Categories = string.Join(" - ", categories),
                Specifications = estateType.EstateTypeSpecifications
                    .Select(x => new EstateTypeSpecificationViewModel
                    {
                        Id = x.Id,
                        Name = x.Specification.NameAr,
                        IsRequired = x.IsRequired ? "الزامي" : "اختياري"
                    }).ToList()
            };

            return data;
        }


        public async Task<List<SpecificationViewModel>> Specifications()
        {
            return await _context.Specifications.OrderByDescending(x => x.CreatedOn)
                .AsNoTracking()
                .Select(x => new SpecificationViewModel
                {
                    Id = x.Id,
                    Icon = MyConstants.DomainUrl + x.Icon,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    Type = EnumHelper.SpecificationTypeName(x.Type, Language.Ar)
                }).ToListAsync();
        }


        public async Task<EditSpecificationViewModel?> GetSpecificationDetails(long id)
        {
            var specification = await _context.Specifications
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new EditSpecificationViewModel
                {
                    Id = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    Type = x.Type,
                    IconPath = MyConstants.DomainUrl + x.Icon
                }).SingleOrDefaultAsync();

            if (specification is null)
                return null;

            return specification;
        }

        public async Task<(bool isSuccess, string message)> CreateSpecification(CreateSpecificationViewModel model)
        {
            var specification = new Specification
            {
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                Type = model.Type,
                Icon = await _sharedService.UploadFileUsingApi(new UploadImageUsingApiDto { image = model.Icon, fileName = (int)FileName.Specifications }),
                IsActive = true
            };

            _context.Specifications.Add(specification);
            await _context.SaveChangesAsync();

            return (true, string.Empty);
        }


        public async Task<(bool isSuccess, string message)> EditSpecification(long id, EditSpecificationViewModel model)
        {
            var specification = await _context.Specifications.FindAsync(id);
            specification.NameAr = model.NameAr;
            specification.NameEn = model.NameEn;
            specification.Type = model.Type;
            specification.Icon = model.Icon == null ? specification.Icon : await _sharedService.UploadFileUsingApi(new UploadImageUsingApiDto { image = model.Icon, fileName = (int)FileName.Specifications });

            _context.Specifications.Update(specification);
            await _context.SaveChangesAsync();

            return (true, string.Empty);
        }


        public async Task<List<SharedViewModel>> SpecificationTypes()
        {
            var types = Enum.GetValues(typeof(SpecificationType));

            return types
                .Cast<SpecificationType>()
                .Select(x => new SharedViewModel
                {
                    Id = (long)x,
                    Name = EnumHelper.SpecificationTypeName(x, Language.Ar)
                }).ToList();
        }


        public async Task<(bool isSuccess, string message)> Remove(long estateTypeId)
        {
            try
            {
                var estateType = await _context.EstateTypes
                    .SingleOrDefaultAsync(x => x.Id == estateTypeId);

                if (estateType is null)
                    return (false, "هذا النوع غير موجود");

                estateType.IsDeleted = true;
                _context.EstateTypes.Update(estateType);
                await _context.SaveChangesAsync();

                return (true, "تم الحذف بنجاح");
            }
            catch
            {
                return (false, "حدث خطأ ما");
            }
        }


        public async Task<(bool isSuccess, string message)> RemoveSpecification(long id)
        {
            try
            {
                var spec = await _context.Specifications
                    .SingleOrDefaultAsync(x => x.Id == id);

                if (spec is null)
                    return (false, "هذه الخاصية غير موجودة");

                spec.IsDeleted = true;
                _context.Specifications.Update(spec);
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
