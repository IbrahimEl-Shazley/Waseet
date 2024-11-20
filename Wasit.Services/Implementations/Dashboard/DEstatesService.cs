using AAITHelper;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Core.Models.IO;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.Interfaces.General;
using Wasit.Services.ViewModels.Estates;
using Wasit.Services.ViewModels.Estates.MyEstates;
using Wasit.Services.ViewModels.Shared;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DEstatesService : IDEstateService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly ICurrentUserService _currentUser;
        private readonly IDSharedService _sharedService;

        public DEstatesService(ApplicationDbContext context, ICurrentUserService currentUser, IDSharedService sharedService, INotificationService notificationService)
        {
            _context = context;
            _currentUser = currentUser;
            _sharedService = sharedService;
            _notificationService = notificationService;
        }

        public async Task<List<EstateViewModel>> MyEstates(CategoryType category = CategoryType.Sale, long type = 0)
        {
            return category switch
            {
                CategoryType.Sale => await SaleEstates(type)
                        .Select(x => new EstateViewModel
                        {
                            Id = x.Id,
                            Name = x.EstateName,
                            City = x.Region.City.NameAr,
                            Region = x.Region.NameAr,
                            Price = x.EstatePrice,
                            UniqueNumber = x.EstateNumber,
                            IsVisible = x.IsShow
                        }).ToListAsync(),

                CategoryType.Rent => await RentEstates(type)
                        .Select(x => new EstateViewModel
                        {
                            Id = x.Id,
                            Name = x.EstateName,
                            City = x.Region.City.NameAr,
                            Region = x.Region.NameAr,
                            Price = x.MonthRentPrice,
                            UniqueNumber = x.EstateNumber,
                            IsVisible = x.IsShow,
                        }).ToListAsync(),

                CategoryType.DailyRent => await DailyRentEstates(type)
                        .Select(x => new EstateViewModel
                        {
                            Id = x.Id,
                            Name = x.EstateName,
                            City = x.Region.City.NameAr,
                            Region = x.Region.NameAr,
                            Price = x.DayRentPrice,
                            UniqueNumber = x.EstateNumber,
                            IsVisible = x.IsShow,
                        }).ToListAsync(),

                CategoryType.Entertainment => await EntertainmentEstates(type)
                        .Select(x => new EstateViewModel
                        {
                            Id = x.Id,
                            Name = x.EstateName,
                            City = x.Region.City.NameAr,
                            Region = x.Region.NameAr,
                            Price = x.DayRentPrice,
                            UniqueNumber = x.EstateNumber,
                            IsVisible = x.IsShow,
                        }).ToListAsync(),
                _ => Enumerable.Empty<EstateViewModel>().ToList(),
            };

            IQueryable<SaleEstate> SaleEstates(long type) => _context.SaleEstates
                .Where(x => x.User.UserType == "Admin" && (type == 0 || x.EstateTypeId == type))
                .OrderByDescending(x => x.Id)
                .AsNoTracking();

            IQueryable<RentEstate> RentEstates(long type) => _context.RentEstates
                .Where(x => x.User.UserType == "Admin" && (type == 0 || x.EstateTypeId == type))
                .OrderByDescending(x => x.Id)
                .AsNoTracking();

            IQueryable<DailyRentEstate> DailyRentEstates(long type) => _context.DailyRentEstates
                .Where(x => x.User.UserType == "Admin" && (type == 0 || x.EstateTypeId == type))
                .OrderByDescending(x => x.Id)
                .AsNoTracking();

            IQueryable<EntertainmentEstate> EntertainmentEstates(long type) => _context.EntertainmentEstates
                .Where(x => x.User.UserType == "Admin" && (type == 0 || x.EstateTypeId == type))
                .OrderByDescending(x => x.Id)
                .AsNoTracking();
        }


        public async Task<List<EstateViewModel>> UsersEstates(CategoryType category = CategoryType.Sale, long type = 0)
        {
            return category switch
            {
                CategoryType.Sale => await SaleEstates(type)
                        .Select(x => new EstateViewModel
                        {
                            Id = x.Id,
                            Name = x.EstateName,
                            City = x.Region.City.NameAr,
                            Region = x.Region.NameAr,
                            Price = x.EstatePrice,
                            UniqueNumber = x.EstateNumber,
                            IsVisible = x.IsShow
                        }).ToListAsync(),

                CategoryType.Rent => await RentEstates(type)
                        .Select(x => new EstateViewModel
                        {
                            Id = x.Id,
                            Name = x.EstateName,
                            City = x.Region.City.NameAr,
                            Region = x.Region.NameAr,
                            Price = x.MonthRentPrice,
                            UniqueNumber = x.EstateNumber,
                            IsVisible = x.IsShow,
                        }).ToListAsync(),

                CategoryType.DailyRent => await DailyRentEstates(type)
                        .Select(x => new EstateViewModel
                        {
                            Id = x.Id,
                            Name = x.EstateName,
                            City = x.Region.City.NameAr,
                            Region = x.Region.NameAr,
                            Price = x.DayRentPrice,
                            UniqueNumber = x.EstateNumber,
                            IsVisible = x.IsShow,
                        }).ToListAsync(),

                CategoryType.Entertainment => await EntertainmentEstates(type)
                        .Select(x => new EstateViewModel
                        {
                            Id = x.Id,
                            Name = x.EstateName,
                            City = x.Region.City.NameAr,
                            Region = x.Region.NameAr,
                            Price = x.DayRentPrice,
                            UniqueNumber = x.EstateNumber,
                            IsVisible = x.IsShow,
                        }).ToListAsync(),
                _ => Enumerable.Empty<EstateViewModel>().ToList(),
            };

            IQueryable<SaleEstate> SaleEstates(long type) => _context.SaleEstates
                .Where(x => x.User.UserType != "Admin" && (type == 0 || x.EstateTypeId == type))
                .OrderByDescending(x => x.Id)
                .AsNoTracking();

            IQueryable<RentEstate> RentEstates(long type) => _context.RentEstates
                .Where(x => x.User.UserType != "Admin" && (type == 0 || x.EstateTypeId == type))
                .OrderByDescending(x => x.Id)
                .AsNoTracking();

            IQueryable<DailyRentEstate> DailyRentEstates(long type) => _context.DailyRentEstates
                .Where(x => x.User.UserType != "Admin" && (type == 0 || x.EstateTypeId == type))
                .OrderByDescending(x => x.Id)
                .AsNoTracking();

            IQueryable<EntertainmentEstate> EntertainmentEstates(long type) => _context.EntertainmentEstates
                .Where(x => x.User.UserType != "Admin" && (type == 0 || x.EstateTypeId == type))
                .OrderByDescending(x => x.Id)
                .AsNoTracking();
        }

        public async Task<List<SharedViewModel>> EstateTypes(CategoryType category)
        {
            var data = await _context.CategoryEstateTypes
                .Where(x => x.Category.Type == category)
                .Include(x => x.EstateType)
                .Select(x => new SharedViewModel
                {
                    Id = x.EstateTypeId,
                    Name = x.EstateType.NameAr
                }).ToListAsync();

            return data.Prepend(new SharedViewModel { Id = 0, Name = "الكل" }).ToList();
        }


        public async Task<(bool isSuccess, string message)> ChangeVisibility(long id, CategoryType category)
        {
            if (category == CategoryType.Sale)
            {
                var estate = await _context.SaleEstates.FindAsync(id);
                estate.IsShow = !estate.IsShow;
                _context.SaleEstates.Update(estate);
            }

            if (category == CategoryType.Rent)
            {
                var estate = await _context.RentEstates.FindAsync(id);
                estate.IsShow = !estate.IsShow;
                _context.RentEstates.Update(estate);
            }

            if (category == CategoryType.DailyRent)
            {
                var estate = await _context.DailyRentEstates.FindAsync(id);
                estate.IsShow = !estate.IsShow;
                _context.DailyRentEstates.Update(estate);
            }

            if (category == CategoryType.Entertainment)
            {
                var estate = await _context.EntertainmentEstates.FindAsync(id);
                estate.IsShow = !estate.IsShow;
                _context.EntertainmentEstates.Update(estate);
            }

            await _context.SaveChangesAsync();
            return (true, "تم");
        }


        public async Task<(bool isSuccess, string message)> Remove(long id, CategoryType category)
        {
            if (category == CategoryType.Sale)
            {
                var estate = await _context.SaleEstates.FindAsync(id);

                if (estate.PurchaseRequests.Any(x => x.Status == PurchaseStatus.Current) ||
                    estate.ReservationRequests.Any(x => x.ReservationStatus == ReservationStatus.Current) ||
                    estate.RatingRequests.Any(x => x.RatingStatus == RatingStatus.New || x.RatingStatus == RatingStatus.Current))
                    return (false, "لا يمكن حذف العقار لوجود طلبات قيد التنفيذ");

                estate.IsDeleted = true;
                _context.SaleEstates.Update(estate);
            }

            if (category == CategoryType.Rent)
            {
                var estate = await _context.RentEstates.FindAsync(id);

                if (estate.RentRequests.Any(x => x.Status == RentStatus.Current) ||
                    estate.ReservationRequests.Any(x => x.ReservationStatus == ReservationStatus.Current) ||
                    estate.RatingRequests.Any(x => x.RatingStatus == RatingStatus.New || x.RatingStatus == RatingStatus.Current))
                    return (false, "لا يمكن حذف العقار لوجود طلبات قيد التنفيذ");

                estate.IsDeleted = true;
                _context.RentEstates.Update(estate);
            }

            if (category == CategoryType.DailyRent)
            {
                var estate = await _context.DailyRentEstates.FindAsync(id);

                if (estate.Requests.Any(x => x.Status != DailyRentStatus.Finished && x.Status != DailyRentStatus.Canceled))
                    return (false, "لا يمكن حذف العقار لوجود طلبات قيد التنفيذ");

                estate.IsDeleted = true;
                _context.DailyRentEstates.Update(estate);
            }

            if (category == CategoryType.Entertainment)
            {
                var estate = await _context.EntertainmentEstates.FindAsync(id);

                if (estate.Requests.Any(x => x.Status != DailyRentStatus.Finished && x.Status != DailyRentStatus.Canceled))
                    return (false, "لا يمكن حذف العقار لوجود طلبات قيد التنفيذ");

                estate.IsDeleted = true;
                _context.EntertainmentEstates.Update(estate);
            }

            await _context.SaveChangesAsync();
            return (true, "تم الحذف بنجاح");
        }


        public async Task<(bool isSuccess, string message)> AcceptEstate(long id, string userId, CategoryType category, double deposit)
        {
            if (category == CategoryType.Sale)
            {
                var estate = await _context.SaleEstates.FindAsync(id);

                estate.Deposit = deposit;
                estate.IsReviewed = true;
                _context.SaleEstates.Update(estate);
            }

            if (category == CategoryType.Rent)
            {
                var estate = await _context.RentEstates.FindAsync(id);

                estate.IsReviewed = true;
                _context.RentEstates.Update(estate);
            }

            if (category == CategoryType.DailyRent)
            {
                var estate = await _context.DailyRentEstates.FindAsync(id);

                estate.IsReviewed = true;
                _context.DailyRentEstates.Update(estate);
            }

            if (category == CategoryType.Entertainment)
            {
                var estate = await _context.EntertainmentEstates.FindAsync(id);

                estate.IsReviewed = true;
                _context.EntertainmentEstates.Update(estate);
            }

            await _context.SaveChangesAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                CategoryType = category,
                RouteId = id,
                TextAr = "تم قبول عقارك من قبل الادارة",
                TextEn = "Your estate has been approved by the management",
                Type = NotifyTypes.EstateIsApproved,
                UserId = userId
            });
            return (true, "تم القبول بنجاح");
        }


        public async Task<(bool isSuccess, string message)> RejectEstate(long id, string userId, CategoryType category, string reason)
        {
            if (category == CategoryType.Sale)
            {
                var estate = await _context.SaleEstates.FindAsync(id);

                estate.RejectionReason = reason;
                _context.SaleEstates.Update(estate);
            }

            if (category == CategoryType.Rent)
            {
                var estate = await _context.RentEstates.FindAsync(id);

                estate.RejectionReason = reason;
                _context.RentEstates.Update(estate);
            }

            if (category == CategoryType.DailyRent)
            {
                var estate = await _context.DailyRentEstates.FindAsync(id);

                estate.RejectionReason = reason;
                _context.DailyRentEstates.Update(estate);
            }

            if (category == CategoryType.Entertainment)
            {
                var estate = await _context.EntertainmentEstates.FindAsync(id);

                estate.RejectionReason = reason;
                _context.EntertainmentEstates.Update(estate);
            }

            await _context.SaveChangesAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                CategoryType = category,
                RouteId = id,
                TextAr = reason,
                TextEn = reason,
                Type = NotifyTypes.EstateIsRejected,
                UserId = userId
            });
            return (true, "تم رفض العقار بنجاح");
        }


        public async Task<(bool isSuccess, long estateId, long estateTypeId, string message)> Create(CreateEstateViewModel model)
        {
            if (await DoesEstateExist(model.CategoryType, model.UniqueNumber))
                return (false, 0, 0, "العقار موجود بالفعل");
            try
            {
                switch (model.CategoryType)
                {
                    case CategoryType.Sale:
                        var saleRstate = await CreateSaleEstate(model);
                        return (true, saleRstate.Id, saleRstate.EstateTypeId, "تم الحفظ بنجاح");

                    case CategoryType.Rent:
                        var rentEstate = await CreateRentEstate(model);
                        return (true, rentEstate.Id, rentEstate.EstateTypeId, "تم الحفظ بنجاح");

                    case CategoryType.DailyRent:
                        var dailyRentEstate = await CreateDailyRentEstate(model);
                        return (true, dailyRentEstate.Id, dailyRentEstate.EstateTypeId, "تم الحفظ بنجاح");

                    case CategoryType.Entertainment:
                        var entertainmentEstate = await CreateEntertainmentEstate(model);
                        return (true, entertainmentEstate.Id, entertainmentEstate.EstateTypeId, "تم الحفظ بنجاح");

                    default:
                        return (false, 0, 0, "حدث خطأ ما");
                }
            }
            catch
            {
                return (false, 0, 0, "حدث خطأ ما");
            }
        }


        public async Task<MyEstateDetailsViewModel> MyEstateDetails(long id, CategoryType category)
        {
            if (category == CategoryType.Sale)
                return await SaleEstateInfo(id);

            if (category == CategoryType.Rent)
                return await RentEstateInfo(id);

            if (category == CategoryType.DailyRent)
                return await DailyRentEstateInfo(id);

            if (category == CategoryType.Entertainment)
                return await EntertainmentEstateInfo(id);

            return null;
        }


        public async Task<MyEstateDetailsViewModel> UserEstateDetails(long id, CategoryType category)
        {
            if (category == CategoryType.Sale)
                return await SaleEstateInfo(id);

            if (category == CategoryType.Rent)
                return await RentEstateInfo(id);

            if (category == CategoryType.DailyRent)
                return await DailyRentEstateInfo(id);

            if (category == CategoryType.Entertainment)
                return await EntertainmentEstateInfo(id);

            return null;
        }


        public async Task<UpdateSaleEstateViewModel?> SaleEstateDetails(long id)
        {
            var estate = await _context.SaleEstates
                .Where(x => x.Id == id)
                .Include(x => x.Images)
                .Select(x => new UpdateSaleEstateViewModel
                {
                    Id = x.Id,
                    Name = x.EstateName,
                    Area = x.EstateArea,
                    Deposit = x.Deposit,
                    CityId = x.Region.CityId,
                    RegionId = x.RegionId,
                    EstateTypeId = x.EstateTypeId,
                    Description = x.EstateDescription,
                    Features = x.EstateFeatures,
                    Price = x.EstatePrice,
                    Lat = x.Lat,
                    Lng = x.Lng,
                    Location = x.Location,
                    IsDevelopable = x.Developable,
                    Images = x.Images.Select(x => new BaseImageItemDto { Id = x.Id, Url = MyConstants.DomainUrl + x.Image }).ToHashSet()
                }).SingleOrDefaultAsync();

            return estate;
        }


        public async Task<UpdateRentEstateViewModel?> RentEstateDetails(long id)
        {
            var estate = await _context.RentEstates
                .Where(x => x.Id == id)
                .Include(x => x.Images)
                .Select(x => new UpdateRentEstateViewModel
                {
                    Id = x.Id,
                    Name = x.EstateName,
                    Area = x.EstateArea,
                    CityId = x.Region.CityId,
                    RegionId = x.RegionId,
                    EstateTypeId = x.EstateTypeId,
                    Description = x.EstateDescription,
                    Features = x.EstateFeatures,
                    Price = x.MonthRentPrice,
                    Lat = x.Lat,
                    Lng = x.Lng,
                    Location = x.Location,
                    IsDevelopable = x.Developable,
                    Images = x.Images.Select(x => new BaseImageItemDto { Id = x.Id, Url = MyConstants.DomainUrl + x.Image }).ToHashSet()
                }).SingleOrDefaultAsync();

            return estate;
        }


        public async Task<UpdateDailyRentEstateViewModel?> DailyRentEstateDetails(long id)
        {
            var estate = await _context.DailyRentEstates
                .Where(x => x.Id == id)
                .Include(x => x.Images)
                .Select(x => new UpdateDailyRentEstateViewModel
                {
                    Id = x.Id,
                    Name = x.EstateName,
                    Area = x.EstateArea,
                    CityId = x.Region.CityId,
                    RegionId = x.RegionId,
                    EstateTypeId = x.EstateTypeId,
                    Description = x.EstateDescription,
                    Features = x.EstateFeatures,
                    Price = x.DayRentPrice,
                    Lat = x.Lat,
                    Lng = x.Lng,
                    Location = x.Location,
                    IsDevelopable = x.Developable,
                    Images = x.Images.Select(x => new BaseImageItemDto { Id = x.Id, Url = MyConstants.DomainUrl + x.Image }).ToHashSet()
                }).SingleOrDefaultAsync();

            return estate;
        }


        public async Task<UpdateEntertainmentRentEstateViewModel?> EntertainmentRentEstateDetails(long id)
        {
            var estate = await _context.EntertainmentEstates
                .Where(x => x.Id == id)
                .Include(x => x.Images)
                .Select(x => new UpdateEntertainmentRentEstateViewModel
                {
                    Id = x.Id,
                    Name = x.EstateName,
                    Area = x.EstateArea,
                    CityId = x.Region.CityId,
                    RegionId = x.RegionId,
                    EstateTypeId = x.EstateTypeId,
                    Description = x.EstateDescription,
                    Features = x.EstateFeatures,
                    Price = x.DayRentPrice,
                    Lat = x.Lat,
                    Lng = x.Lng,
                    Location = x.Location,
                    IsDevelopable = x.Developable,
                    Images = x.Images.Select(x => new BaseImageItemDto { Id = x.Id, Url = MyConstants.DomainUrl + x.Image }).ToHashSet()
                }).SingleOrDefaultAsync();

            return estate;
        }


        public async Task<(bool isSuccess, long estateId, string message)> UpdateSaleEstate(UpdateSaleEstateViewModel model)
        {
            try
            {
                var estate = await _context.SaleEstates
                    .IgnoreAutoIncludes()
                    .SingleOrDefaultAsync(x => x.Id == model.Id);

                estate.EstateName = model.Name;
                estate.EstateDescription = model.Description;
                estate.Developable = model.IsDevelopable;
                estate.EstatePrice = model.Price;
                estate.Lat = model.Lat;
                estate.Lng = model.Lng;
                estate.Location = model.Location;
                estate.EstateArea = model.Area;
                estate.Deposit = model.Deposit;
                estate.EstateFeatures = model.Features;
                estate.RegionId = model.RegionId;
                estate.EstateTypeId = model.EstateTypeId;
                estate.RegionId = model.RegionId;

                var images = new List<SaleEstateImage>();

                if (model.ImagesFiles.Count > 0)
                {
                    _context.SaleEstateImages.RemoveRange(estate.Images);

                    foreach (var image in model.ImagesFiles)
                    {
                        images.Add(new SaleEstateImage
                        {
                            Image = await _sharedService.UploadFileUsingApi(new UploadImageUsingApiDto { image = image, fileName = (int)FileName.Estates }),
                        });
                    }
                }

                _context.SaleEstates.Update(estate);
                await _context.SaveChangesAsync();
                return (true, estate.Id, "تم التعديل بنجاح");
            }
            catch
            {
                throw;
            }
        }

        public async Task<(bool isSuccess, long estateId, string message)> UpdateRentEstate(UpdateRentEstateViewModel model)
        {
            try
            {
                var estate = await _context.RentEstates
                    .IgnoreAutoIncludes()
                    .SingleOrDefaultAsync(x => x.Id == model.Id);

                estate.EstateName = model.Name;
                estate.EstateDescription = model.Description;
                estate.Developable = model.IsDevelopable;
                estate.MonthRentPrice = model.Price;
                estate.Lat = model.Lat;
                estate.Lng = model.Lng;
                estate.Location = model.Location;
                estate.EstateArea = model.Area;
                estate.EstateFeatures = model.Features;
                estate.RegionId = model.RegionId;
                estate.EstateTypeId = model.EstateTypeId;
                estate.RegionId = model.RegionId;

                var images = new List<RentEstateImage>();

                if (model.ImagesFiles.Count > 0)
                {
                    _context.RentEstateImages.RemoveRange(estate.Images);

                    foreach (var image in model.ImagesFiles)
                    {
                        images.Add(new RentEstateImage
                        {
                            Image = await _sharedService.UploadFileUsingApi(new UploadImageUsingApiDto { image = image, fileName = (int)FileName.Estates }),
                        });
                    }
                }

                _context.RentEstates.Update(estate);
                await _context.SaveChangesAsync();
                return (true, estate.Id, "تم التعديل بنجاح");
            }
            catch
            {
                throw;
            }
        }


        public async Task<(bool isSuccess, long estateId, string message)> UpdateDailyRentEstate(UpdateDailyRentEstateViewModel model)
        {
            try
            {
                var estate = await _context.DailyRentEstates
                    .IgnoreAutoIncludes()
                    .SingleOrDefaultAsync(x => x.Id == model.Id);

                estate.EstateName = model.Name;
                estate.EstateDescription = model.Description;
                estate.Developable = model.IsDevelopable;
                estate.DayRentPrice = model.Price;
                estate.Lat = model.Lat;
                estate.Lng = model.Lng;
                estate.Location = model.Location;
                estate.EstateArea = model.Area;
                estate.EstateFeatures = model.Features;
                estate.RegionId = model.RegionId;
                estate.EstateTypeId = model.EstateTypeId;
                estate.RegionId = model.RegionId;

                var images = new List<DailyRentEstateImage>();

                if (model.ImagesFiles.Count > 0)
                {
                    _context.DailyRentEstateImages.RemoveRange(estate.Images);

                    foreach (var image in model.ImagesFiles)
                    {
                        images.Add(new DailyRentEstateImage
                        {
                            Image = await _sharedService.UploadFileUsingApi(new UploadImageUsingApiDto { image = image, fileName = (int)FileName.Estates }),
                        });
                    }
                }

                _context.DailyRentEstates.Update(estate);
                await _context.SaveChangesAsync();
                return (true, estate.Id, "تم التعديل بنجاح");
            }
            catch
            {
                throw;
            }
        }


        public async Task<(bool isSuccess, long estateId, string message)> UpdateEntertainmentRentEstate(UpdateEntertainmentRentEstateViewModel model)
        {
            try
            {
                var estate = await _context.EntertainmentEstates
                    .IgnoreAutoIncludes()
                    .SingleOrDefaultAsync(x => x.Id == model.Id);

                estate.EstateName = model.Name;
                estate.EstateDescription = model.Description;
                estate.Developable = model.IsDevelopable;
                estate.DayRentPrice = model.Price;
                estate.Lat = model.Lat;
                estate.Lng = model.Lng;
                estate.Location = model.Location;
                estate.EstateArea = model.Area;
                estate.EstateFeatures = model.Features;
                estate.RegionId = model.RegionId;
                estate.EstateTypeId = model.EstateTypeId;
                estate.RegionId = model.RegionId;

                var images = new List<EntertainmentEstateImage>();

                if (model.ImagesFiles.Count > 0)
                {
                    _context.EntertainmentEstateImages.RemoveRange(estate.Images);

                    foreach (var image in model.ImagesFiles)
                    {
                        images.Add(new EntertainmentEstateImage
                        {
                            Image = await _sharedService.UploadFileUsingApi(new UploadImageUsingApiDto { image = image, fileName = (int)FileName.Estates }),
                        });
                    }
                }

                _context.EntertainmentEstates.Update(estate);
                await _context.SaveChangesAsync();
                return (true, estate.Id, "تم التعديل بنجاح");
            }
            catch
            {
                throw;
            }
        }


        public async Task<List<SpecificationFormItemViewModel>> GetSpecsForm(long estateTypeId)
        {
            return await _context.EstateTypeSpecifications
                .Where(x => x.EstateTypeId == estateTypeId)
                .Include(x => x.Specification)
                .AsNoTracking()
                .Select(x => new SpecificationFormItemViewModel
                {
                    Id = x.Id,
                    Name = x.Specification.Name(Language.En).Dehumanize(),
                    Label = x.Specification.Name(Language.Ar),
                    IsRequired = x.IsRequired,
                    Type = x.Specification.Type
                }).ToListAsync();
        }


        public async Task<List<SpecificationFormItemViewModel>> GetEstateSpecsForm(long estateId)
        {
            return await _context.SaleEstateSpecificationValues
                .Where(x => x.SaleEstateId == estateId)
                .Include(x => x.EstateTypeSpecification.Specification)
                .AsNoTracking()
                .Select(x => new SpecificationFormItemViewModel
                {
                    Id = x.Id,
                    EstateTypeSpecificationId = x.EstateTypeSpecificationId,
                    IsRequired = x.EstateTypeSpecification.IsRequired,
                    Label = x.EstateTypeSpecification.Specification.Name(Language.Ar),
                    Name = x.EstateTypeSpecification.Specification.Name(Language.En).Dehumanize(),
                    Type = x.EstateTypeSpecification.Specification.Type,
                    Value = x.SpecificationValue
                }).ToListAsync();
        }

        public async Task<(bool isSuccess, string message)> UpdateSpecs(CreateUpdateEstateSpecsViewModel model)
        {
            if (model.Category == CategoryType.Sale)
            {
                var estateSpecifications = new List<SaleEstateSpecificationValue>();
                foreach (var item in model.Specs)
                {
                    if (!await _context.SaleEstateSpecificationValues.AnyAsync(x => x.SaleEstateId == model.EstateId && x.EstateTypeSpecificationId == item.Id))
                        estateSpecifications.Add(new SaleEstateSpecificationValue
                        {
                            SaleEstateId = model.EstateId,
                            EstateTypeSpecificationId = item.Id,
                            SpecificationValue = item.Value
                        });

                    await _context.SaleEstateSpecificationValues.Where(x => x.SaleEstateId == model.EstateId && x.EstateTypeSpecificationId == item.Id)
                        .ExecuteUpdateAsync(x => x.SetProperty(x => x.SpecificationValue, item.Value));
                }
            }

            if (model.Category == CategoryType.Rent)
            {
                var estateSpecifications = new List<RentEstateSpecificationValue>();
                foreach (var item in model.Specs)
                {
                    if (!await _context.RentEstateSpecificationValues.AnyAsync(x => x.RentEstateId == model.EstateId && x.EstateTypeSpecificationId == item.Id))
                        estateSpecifications.Add(new RentEstateSpecificationValue
                        {
                            RentEstateId = model.EstateId,
                            EstateTypeSpecificationId = item.Id,
                            SpecificationValue = item.Value
                        });

                    await _context.RentEstateSpecificationValues.Where(x => x.RentEstateId == model.EstateId && x.EstateTypeSpecificationId == item.Id)
                        .ExecuteUpdateAsync(x => x.SetProperty(x => x.SpecificationValue, item.Value));
                }
            }

            if (model.Category == CategoryType.DailyRent)
            {
                var estateSpecifications = new List<DailyRentEstateSpecificationValue>();
                foreach (var item in model.Specs)
                {
                    if (!await _context.DailyRentEstateSpecificationValues.AnyAsync(x => x.DailyRentEstateId == model.EstateId && x.EstateTypeSpecificationId == item.Id))
                        estateSpecifications.Add(new DailyRentEstateSpecificationValue
                        {
                            DailyRentEstateId = model.EstateId,
                            EstateTypeSpecificationId = item.Id,
                            SpecificationValue = item.Value
                        });

                    await _context.DailyRentEstateSpecificationValues.Where(x => x.DailyRentEstateId == model.EstateId && x.EstateTypeSpecificationId == item.Id)
                        .ExecuteUpdateAsync(x => x.SetProperty(x => x.SpecificationValue, item.Value));
                }
            }

            if (model.Category == CategoryType.Entertainment)
            {
                var estateSpecifications = new List<EntertainmentEstateSpecificationValue>();
                foreach (var item in model.Specs)
                {
                    if (!await _context.EntertainmentEstateSpecificationValues.AnyAsync(x => x.EntertainmentEstateId == model.EstateId && x.EstateTypeSpecificationId == item.Id))
                        estateSpecifications.Add(new EntertainmentEstateSpecificationValue
                        {
                            EntertainmentEstateId = model.EstateId,
                            EstateTypeSpecificationId = item.Id,
                            SpecificationValue = item.Value
                        });

                    await _context.EntertainmentEstateSpecificationValues.Where(x => x.EntertainmentEstateId == model.EstateId && x.EstateTypeSpecificationId == item.Id)
                        .ExecuteUpdateAsync(x => x.SetProperty(x => x.SpecificationValue, item.Value));
                }
            }

            return (true, "تم التعديل بنجاح");
        }



        public async Task<(bool isSuccess, string message)> CreateSpecs(CreateUpdateEstateSpecsViewModel model)
        {
            if (model.Category == CategoryType.Sale)
            {
                var estateSpecifications = new List<SaleEstateSpecificationValue>();
                foreach (var item in model.Specs)
                {
                    estateSpecifications.Add(new SaleEstateSpecificationValue
                    {
                        SaleEstateId = model.EstateId,
                        EstateTypeSpecificationId = item.Id,
                        SpecificationValue = item.Value
                    });
                }

                _context.SaleEstateSpecificationValues.AddRange(estateSpecifications);
            }

            if (model.Category == CategoryType.Rent)
            {
                var estateSpecifications = new List<RentEstateSpecificationValue>();
                foreach (var item in model.Specs)
                {
                    estateSpecifications.Add(new RentEstateSpecificationValue
                    {
                        RentEstateId = model.EstateId,
                        EstateTypeSpecificationId = item.Id,
                        SpecificationValue = item.Value
                    });
                }

                _context.RentEstateSpecificationValues.AddRange(estateSpecifications);
            }

            if (model.Category == CategoryType.DailyRent)
            {
                var estateSpecifications = new List<DailyRentEstateSpecificationValue>();
                foreach (var item in model.Specs)
                {
                    estateSpecifications.Add(new DailyRentEstateSpecificationValue
                    {
                        DailyRentEstateId = model.EstateId,
                        EstateTypeSpecificationId = item.Id,
                        SpecificationValue = item.Value
                    });
                }

                _context.DailyRentEstateSpecificationValues.AddRange(estateSpecifications);
            }

            if (model.Category == CategoryType.Entertainment)
            {
                var estateSpecifications = new List<EntertainmentEstateSpecificationValue>();
                foreach (var item in model.Specs)
                {
                    estateSpecifications.Add(new EntertainmentEstateSpecificationValue
                    {
                        EntertainmentEstateId = model.EstateId,
                        EstateTypeSpecificationId = item.Id,
                        SpecificationValue = item.Value
                    });
                }

                _context.EntertainmentEstateSpecificationValues.AddRange(estateSpecifications);
            }

            await _context.SaveChangesAsync();
            return (true, "تم الحفظ بنجاح");
        }



        public async Task<(bool isSuccess, string message)> DeleteUsersPrices(long id)
        {
            var prices = await _context.UserPriceToEstates
                .Where(x => x.SaleEstateId == id)
                .ToListAsync();

            _context.UserPriceToEstates.RemoveRange(prices);
            await _context.SaveChangesAsync();

            return (true, "تم الحذف بنجاح");
        }


        public async Task<MyRequestPageViewModel> MyReservationRequests(CategoryType category, long estateId, ReservationStatus status)
        {
            return category switch
            {
                CategoryType.Sale => new MyRequestPageViewModel
                {
                    EstateId = estateId,
                    Requests = await _context.SaleReservationRequests
                    .Where(x => x.SaleEstateId == estateId && x.ReservationStatus == status)
                    .Include(x => x.User)
                    .OrderByDescending(x => x.Id)
                    .Select(x => new MyRequestViewModel
                    {
                        Id = x.Id,
                        UserName = x.User.User_Name
                    }).ToListAsync()
                },

                CategoryType.Rent => new MyRequestPageViewModel
                {
                    EstateId = estateId,
                    Requests = await _context.RentReservationRequests
                    .Where(x => x.RentEstateId == estateId && x.ReservationStatus == status)
                    .Include(x => x.User)
                    .OrderByDescending(x => x.Id)
                    .Select(x => new MyRequestViewModel
                    {
                        Id = x.Id,
                        UserName = x.User.User_Name
                    }).ToListAsync()
                },

                _ => null
            };
        }


        public async Task<MyRequestPageViewModel> MyEvaluationRequests(CategoryType category, long estateId, RatingStatus status)
        {
            return category switch
            {
                CategoryType.Sale => new MyRequestPageViewModel
                {
                    EstateId = estateId,
                    Requests = await _context.SaleRatingRequests
                    .Where(x => x.Id == estateId && x.RatingStatus == status)
                    .Include(x => x.User)
                    .OrderByDescending(x => x.Id)
                    .Select(x => new MyRequestViewModel
                    {
                        Id = x.Id,
                        UserName = x.User.User_Name
                    }).ToListAsync()
                },

                CategoryType.Rent => new MyRequestPageViewModel
                {
                    EstateId = estateId,
                    Requests = await _context.RentRatingRequests
                    .Where(x => x.Id == estateId && x.RatingStatus == status)
                    .Include(x => x.User)
                    .OrderByDescending(x => x.Id)
                    .Select(x => new MyRequestViewModel
                    {
                        Id = x.Id,
                        UserName = x.User.User_Name
                    }).ToListAsync()
                },

                _ => null
            };
        }


        public async Task<MyRequestPageViewModel> MyPurchaseRequests(long estateId)
        {
            var data = new MyRequestPageViewModel
            {
                EstateId = estateId,
                Requests = await _context.PurchaseRequests
                      .Where(x => x.SaleEstateId == estateId && x.Status != PurchaseStatus.Canceled)
                      .Include(x => x.User)
                      .OrderByDescending(x => x.Id)
                      .AsNoTracking()
                      .Select(x => new MyRequestViewModel
                      {
                          Id = x.Id,
                          UserName = x.User.User_Name,
                          Status = EnumHelper.PublisherPurchaseRequestStatus(x, Language.Ar)
                      }).ToListAsync()
            };

            return data;
        }


        public async Task<MyRequestPageViewModel> MyRentRequests(long estateId)
        {
            var data = new MyRequestPageViewModel
            {
                EstateId = estateId,
                Requests = await _context.RentRequests
                      .Where(x => x.RentEstateId == estateId && x.Status != RentStatus.Canceled)
                      .Include(x => x.User)
                      .OrderByDescending(x => x.Id)
                      .AsNoTracking()
                      .Select(x => new MyRequestViewModel
                      {
                          Id = x.Id,
                          UserName = x.User.User_Name,
                          Status = EnumHelper.PublisherRentRequestStatus(x, Language.Ar)
                      }).ToListAsync()
            };

            return data;
        }


        public async Task<MyRequestPageViewModel> MyDailyRentRequests(long estateId, DailyRentStatus status)
        {
            var data = new MyRequestPageViewModel
            {
                EstateId = estateId,
                Requests = await _context.DailyRentRequests
                      .Where(x => x.DailyRentEstateId == estateId && x.Status == status)
                      .Include(x => x.User)
                      .OrderByDescending(x => x.Id)
                      .AsNoTracking()
                      .Select(x => new MyRequestViewModel
                      {
                          Id = x.Id,
                          UserName = x.User.User_Name,
                      }).ToListAsync()
            };

            return data;
        }


        public async Task<MyRequestPageViewModel> MyEntertainmentRentRequests(long estateId, DailyRentStatus status)
        {
            var data = new MyRequestPageViewModel
            {
                EstateId = estateId,
                Requests = await _context.EntertainmentRequests
                      .Where(x => x.EntertainmentEstateId == estateId && x.Status == status)
                      .Include(x => x.User)
                      .OrderByDescending(x => x.Id)
                      .AsNoTracking()
                      .Select(x => new MyRequestViewModel
                      {
                          Id = x.Id,
                          UserName = x.User.User_Name
                      }).ToListAsync()
            };

            return data;
        }


        public async Task<(bool isSuccess, string message)> AcceptPurchaseRequest(long id)
        {
            var purchaseRequest = await _context.PurchaseRequests.FirstOrDefaultAsync(x => x.Id == id);

            if (purchaseRequest == null)
                return (false, "لم يتم العثور علي الطلب");

            if (purchaseRequest.Status != PurchaseStatus.New)
                return (false, "لا يمكن قبول الطلب");

            //if (otherRequests.Any(x => x.Status == PurchaseStatus.Current && x.Deposit > 0))
            //    throw new BussinessRuleException("RequestCanNotBeAcceptedAsThereIsACurrentPurchaseRequestWithDeposit");

            purchaseRequest.IsAccepted = true;
            purchaseRequest.Status = PurchaseStatus.Current;

            var otherRequests = purchaseRequest.SaleEstate.PurchaseRequests.Where(x => x.Id != id && x.Status != PurchaseStatus.Canceled).ToList();
            foreach (var request in otherRequests)
            {
                request.Status = PurchaseStatus.Finished;
            }
            _context.PurchaseRequests.Update(purchaseRequest);
            _context.PurchaseRequests.UpdateRange(otherRequests);
            await _context.SaveChangesAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم قبول طلب شرائك لعقار رقم {purchaseRequest.SaleEstate.EstateNumber}",
                TextEn = $"Your purchase request for estate no. {purchaseRequest.SaleEstate.EstateNumber} has been accepted",
                UserId = purchaseRequest.UserId,
                Type = NotifyTypes.RequestAccepted,
                CategoryType = CategoryType.Sale,
                RouteId = id
            });

            return (true, "تم قبول الطلب بنجاح");
        }


        public async Task<(bool isSuccess, string message)> RejectPurchaseRequest(long id)
        {
            var purchaseRequest = await _context.PurchaseRequests.FirstOrDefaultAsync(x => x.Id == id);

            if (purchaseRequest.Status != PurchaseStatus.New)
                return (false, "لا يمكن قبول الطلب");

            purchaseRequest.Status = PurchaseStatus.Finished;

            _context.PurchaseRequests.Update(purchaseRequest);
            await _context.SaveChangesAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم رفض طلب شرائك لعقار رقم {purchaseRequest.SaleEstate.EstateNumber}",
                TextEn = $"Your purchase request for estate no. {purchaseRequest.SaleEstate.EstateNumber} has been rejected",
                UserId = purchaseRequest.UserId,
                Type = NotifyTypes.RequestRejected,
                CategoryType = CategoryType.Sale,
                RouteId = id
            });

            return (true, "تم رفض الطلب بنجاح");
        }


        public async Task<(bool isSuccess, string message)> ConfirmEstateIsSold(long estateId, double price, long requestId)
        {
            var saleEstate = await _context.SaleEstates.FirstOrDefaultAsync(x => x.Id == estateId);
            if (saleEstate == null)
                return (false, "لم يتم العثور علي العقار");

            if (saleEstate.PurchaseRequests.Any(x => x.Id == requestId && !x.IsPay))
                return (false, "لم يتم الدفع");

            if (saleEstate.PurchaseRequests.Any(x => x.Id == requestId && x.HasRefundRequest))
                return (false, "قام المشتري بطلب استرداد العربون");

            saleEstate.IsSold = true;
            saleEstate.FinalEstatePrice = price;

            _context.SaleEstates.Update(saleEstate);
            await _context.SaveChangesAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم تأكيد البيع للعقار رقم {saleEstate.EstateNumber} من قبل المعلن",
                TextEn = $"The sale of property no. {saleEstate.EstateNumber} has been confirmed by the advertiser",
                UserId = saleEstate.PurchaseRequests.FirstOrDefault(x => x.Id == requestId).UserId,
                Type = NotifyTypes.EstateIsSoldOrRentedConfirmation,
                CategoryType = CategoryType.Sale,
                RouteId = requestId
            });

            return (true, "تم تأكيد البيع بنجاح");
        }


        public async Task<(bool isSuccess, string message)> AcceptRentRequest(long requestId)
        {
            var rentRequest = await _context.RentRequests.FirstOrDefaultAsync(x => x.Id == requestId);

            if (rentRequest == null)
                return (false, "لم يتم العثور علي الطلب");

            if (rentRequest.Status != RentStatus.New)
                return (false, "لا يمكن قبول الطلب");

            rentRequest.IsAccepted = true;
            rentRequest.Status = RentStatus.Current;

            var otherRequests = rentRequest.RentEstate.RentRequests.Where(x => x.Id != requestId).ToList();
            foreach (var request in otherRequests)
            {
                request.Status = RentStatus.Finished;
            }
            _context.RentRequests.Update(rentRequest);
            _context.RentRequests.UpdateRange(otherRequests);
            await _context.SaveChangesAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم قبول طلب ايجارك للعقار رقم {rentRequest.RentEstate.EstateNumber}",
                TextEn = $"Your rent request for property no. {rentRequest.RentEstate.EstateNumber} has been accepted",
                UserId = rentRequest.UserId,
                Type = NotifyTypes.RequestAccepted,
                CategoryType = CategoryType.Rent,
                RouteId = requestId
            });

            return (true, "تم قبول الطلب بنجاح");
        }


        public async Task<(bool isSuccess, string message)> RejectRentRequest(long requestId)
        {
            var rentRequest = await _context.RentRequests.FirstOrDefaultAsync(x => x.Id == requestId);

            if (rentRequest == null)
                return (false, "لم يتم العثور علي الطلب");

            if (rentRequest.Status != RentStatus.New)
                return (false, "لا يمكن رفض الطلب");

            rentRequest.Status = RentStatus.Finished;

            _context.RentRequests.Update(rentRequest);
            await _context.SaveChangesAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم رفض طلب ايجارك للعقار رقم {rentRequest.RentEstate.EstateNumber}",
                TextEn = $"Your rent request for property no. {rentRequest.RentEstate.EstateNumber} has been rejected",
                UserId = rentRequest.UserId,
                Type = NotifyTypes.RequestRejected,
                CategoryType = CategoryType.Rent,
                RouteId = requestId
            });

            return (true, "تم رفض الطلب بنجاح");
        }


        public async Task<(bool isSuccess, string message)> ConfirmEstateRented(long estateId, long requestId)
        {
            var rentEstate = await _context.RentEstates.FirstOrDefaultAsync(x => x.Id == estateId);
            if (rentEstate == null)
                return (false, "لم يتم العثور علي العقار");

            var request = await _context.RentRequests.FirstOrDefaultAsync<RentRequest>(x => x.Id == requestId);
            if (request == null)
                return (false, "لم يتم العثور علي الطلب");

            if (!request.IsAccepted && !request.IsPay)
                return (false, "لا يمكن تاكيد ايجار العقار لأنه لم يتم قبوله");

            rentEstate.IsRented = true;
            request.IsRented = true;
            _context.RentEstates.Update(rentEstate);
            _context.RentRequests.Update(request);
            await _context.SaveChangesAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم تأكيد استلام العقار رقم {rentEstate.EstateNumber} من قبل المعلن",
                TextEn = $"The rent of property no. {rentEstate.EstateNumber} has been confirmed by the advertiser",
                UserId = rentEstate.RentRequests.FirstOrDefault(x => x.Id == requestId).UserId,
                Type = NotifyTypes.EstateIsSoldOrRentedConfirmation,
                CategoryType = CategoryType.Rent,
                RouteId = requestId
            });

            return (true, "تم تاكيد ايجار العقار بنجاح");
        }

        public async Task<MyRequestDetailsViewModel> MyRequestDetails(long requestId, CategoryType category, int requestType)
        {
            #region Sale
            if (category == CategoryType.Sale)
            {
                if (requestType == (int)SaleRequestType.ReservationRequest)
                {
                    var data = await _context.SaleReservationRequests
                        .Where(x => x.Id == requestId)
                        .Select(x => new MyRequestDetailsViewModel
                        {
                            Id = x.Id,
                            UserName = x.User.User_Name,
                            EstateInfo = new MyEstateInfoCardViewModel
                            {
                                Id = x.SaleEstateId,
                                Name = x.SaleEstate.EstateName,
                                Address = x.SaleEstate.Address(Language.Ar),
                                Number = x.SaleEstate.EstateNumber,
                                Image = MyConstants.DomainUrl + x.SaleEstate.Images.FirstOrDefault().Image,
                                Price = x.SaleEstate.EstatePrice
                            },
                            ReservationRequestDetails = new MyReservationRequestDetailsViewModel
                            {
                                Status = EnumHelper.ReservationRequestStatus(x.ReservationStatus, Language.Ar),
                                TimeSpan = x.ReservationDate.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                            }
                        }).FirstOrDefaultAsync();

                    return data;
                }
                else if (requestType == (int)SaleRequestType.RatingRequest)
                {
                    var data = await _context.SaleRatingRequests
                        .Where(x => x.Id == requestId)
                        .Select(x => new MyRequestDetailsViewModel
                        {
                            Id = x.Id,
                            UserName = x.User.User_Name,
                            EstateInfo = new MyEstateInfoCardViewModel
                            {
                                Id = x.SaleEstateId,
                                Name = x.SaleEstate.EstateName,
                                Address = x.SaleEstate.Address(Language.Ar),
                                Number = x.SaleEstate.EstateNumber,
                                Image = MyConstants.DomainUrl + x.SaleEstate.Images.FirstOrDefault().Image,
                                Price = x.SaleEstate.EstatePrice
                            },
                            RatingRequestDetails = new MyRatingRequestDetailsViewModel
                            {
                                Id = x.Id,
                                Price = x.ServiceCost,
                                DelegateName = x.RatingStatus == RatingStatus.New ? "لم يحدد بعد" : x.Provider.User_Name,
                                Report = string.IsNullOrWhiteSpace(x.ReportUrl) ? null : MyConstants.DomainUrl + x.ReportUrl
                            }
                        }).FirstOrDefaultAsync();

                    return data;
                }
                else if (requestType == (int)SaleRequestType.PurchaseRequest)
                {
                    var data = await _context.PurchaseRequests
                        .Where(x => x.Id == requestId)
                        .Select(x => new MyRequestDetailsViewModel
                        {
                            Id = x.Id,
                            UserName = x.User.User_Name,
                            EstateInfo = new MyEstateInfoCardViewModel
                            {
                                Id = x.SaleEstateId,
                                Name = x.SaleEstate.EstateName,
                                Address = x.SaleEstate.Address(Language.Ar),
                                Number = x.SaleEstate.EstateNumber,
                                Image = MyConstants.DomainUrl + x.SaleEstate.Images.FirstOrDefault().Image,
                                Price = x.SaleEstate.EstatePrice
                            },
                            PurchaseRequestDetails = new MyPurchaseRequestDetailsViewModel
                            {
                                Id = x.Id,
                                Deposit = x.Deposit,
                                IsAccepted = x.IsAccepted,
                                IsRejected = !x.IsAccepted && x.Status == PurchaseStatus.Finished,
                                HasRefundRequest = x.HasRefundRequest,
                                Status = EnumHelper.PublisherPurchaseRequestStatus(x, Language.Ar),
                                StatusEnum = x.Status,
                                FinalPrice = x.SaleEstate.FinalEstatePrice,
                                IsPaid = x.IsPay
                            }
                        }).FirstOrDefaultAsync();

                    return data;
                }
            }
            #endregion

            #region Rent
            if (category == CategoryType.Rent)
            {
                if (requestType == (int)RentRequestType.ReservationRequest)
                {
                    var data = await _context.RentReservationRequests
                        .Where(x => x.Id == requestId)
                        .Select(x => new MyRequestDetailsViewModel
                        {
                            Id = x.Id,
                            UserName = x.User.User_Name,
                            EstateInfo = new MyEstateInfoCardViewModel
                            {
                                Id = x.RentEstateId,
                                Name = x.RentEstate.EstateName,
                                Address = x.RentEstate.Address(Language.Ar),
                                Number = x.RentEstate.EstateNumber,
                                Image = MyConstants.DomainUrl + x.RentEstate.Images.FirstOrDefault().Image,
                                Price = x.RentEstate.MonthRentPrice
                            },
                            ReservationRequestDetails = new MyReservationRequestDetailsViewModel
                            {
                                Status = EnumHelper.ReservationRequestStatus(x.ReservationStatus, Language.Ar),
                                TimeSpan = x.ReservationDate.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                            }
                        }).FirstOrDefaultAsync();

                    return data;
                }
                else if (requestType == (int)RentRequestType.RatingRequest)
                {
                    var data = await _context.RentRatingRequests
                        .Where(x => x.Id == requestId)
                        .Select(x => new MyRequestDetailsViewModel
                        {
                            Id = x.Id,
                            UserName = x.User.User_Name,
                            EstateInfo = new MyEstateInfoCardViewModel
                            {
                                Id = x.RentEstateId,
                                Name = x.RentEstate.EstateName,
                                Address = x.RentEstate.Address(Language.Ar),
                                Number = x.RentEstate.EstateNumber,
                                Image = MyConstants.DomainUrl + x.RentEstate.Images.FirstOrDefault().Image,
                                Price = x.RentEstate.MonthRentPrice
                            },
                            RatingRequestDetails = new MyRatingRequestDetailsViewModel
                            {
                                Id = x.Id,
                                Price = x.ServiceCost,
                                DelegateName = x.RatingStatus == RatingStatus.New ? "لم يحدد بعد" : x.Provider.User_Name,
                                Report = string.IsNullOrWhiteSpace(x.ReportUrl) ? null : MyConstants.DomainUrl + x.ReportUrl
                            }
                        }).FirstOrDefaultAsync();

                    return data;
                }
                else if (requestType == (int)RentRequestType.RentRequest)
                {
                    var data = await _context.RentRequests
                        .Where(x => x.Id == requestId)
                        .Select(x => new MyRequestDetailsViewModel
                        {
                            Id = x.Id,
                            UserName = x.User.User_Name,
                            EstateInfo = new MyEstateInfoCardViewModel
                            {
                                Id = x.RentEstateId,
                                Name = x.RentEstate.EstateName,
                                Address = x.RentEstate.Address(Language.Ar),
                                Number = x.RentEstate.EstateNumber,
                                Image = MyConstants.DomainUrl + x.RentEstate.Images.FirstOrDefault().Image,
                                Price = x.RentEstate.MonthRentPrice,
                            },
                            RentRequestDetails = new MyRentRequestDetailsViewModel
                            {
                                Id = x.Id,
                                IsAccepted = x.IsAccepted,
                                StatusEnum = x.Status,
                                Status = EnumHelper.PublisherRentRequestStatus(x, Language.Ar),
                                MonthsCount = x.MonthCount,
                                YearsCount = x.YearCount,
                                IsPaid = x.IsPay,
                                IsRented = x.IsRented
                            }
                        }).FirstOrDefaultAsync();

                    return data;
                }
            }
            #endregion

            #region DailyRent
            if (category == CategoryType.DailyRent)
            {
                var data = await _context.DailyRentRequests
                    .Where(x => x.Id == requestId)
                    .Select(x => new MyRequestDetailsViewModel
                    {
                        Id = x.Id,
                        UserName = x.User.User_Name,
                        EstateInfo = new MyEstateInfoCardViewModel
                        {
                            Id = x.DailyRentEstateId,
                            Name = x.DailyRentEstate.EstateName,
                            Address = x.DailyRentEstate.Address(Language.Ar),
                            Number = x.DailyRentEstate.EstateNumber,
                            Image = MyConstants.DomainUrl + x.DailyRentEstate.Images.FirstOrDefault().Image,
                            Price = x.DailyRentEstate.DayRentPrice
                        },
                        DailyRentRequestDetails = new MyDailyRentRequestDetailsViewModel
                        {
                            Id = x.Id,
                            StatusEnum = x.Status,
                            DaysCount = x.TotalDays,
                            ArrivalDate = x.ArrivalDate.ToString("dd/MM/yyyy"),
                            DepartureDate = x.LeaveDate.ToString("dd/MM/yyyy"),
                            CancelDate = x.CancelDate.ToString("dd/MM/yyyy"),
                            //TimeSpan = (x.LeaveDate - DateTime.UtcNow.AddHours(2)).ToString("yyyy-MM-dd HH:mm:ss"),
                        }
                    }).FirstOrDefaultAsync();

                return data;
            }
            #endregion

            #region Entertainment
            if (category == CategoryType.Entertainment)
            {
                var data = await _context.EntertainmentRequests
                    .Where(x => x.Id == requestId)
                    .Select(x => new MyRequestDetailsViewModel
                    {
                        Id = x.Id,
                        UserName = x.User.User_Name,
                        EstateInfo = new MyEstateInfoCardViewModel
                        {
                            Id = x.EntertainmentEstateId,
                            Name = x.EntertainmentEstate.EstateName,
                            Address = x.EntertainmentEstate.Address(Language.Ar),
                            Number = x.EntertainmentEstate.EstateNumber,
                            Image = MyConstants.DomainUrl + x.EntertainmentEstate.Images.FirstOrDefault().Image,
                            Price = x.EntertainmentEstate.DayRentPrice
                        },
                        EntertainmentRentRequestDetails = new MyEntertainmentRentRequestDetailsViewModel
                        {
                            Id = x.Id,
                            StatusEnum = x.Status,
                            DaysCount = x.TotalDays,
                            ArrivalDate = x.ArrivalDate.ToString("dd/MM/yyyy"),
                            DepartureDate = x.LeaveDate.ToString("dd/MM/yyyy"),
                            CancelDate = x.CancelDate.ToString("dd/MM/yyyy"),
                            //TimeSpan = (x.LeaveDate - DateTime.UtcNow.AddHours(2)).ToString("yyyy-MM-dd HH:mm:ss")
                        }
                    }).FirstOrDefaultAsync();

                return data;
            }
            #endregion

            return null;
        }


        #region Private Methods
        async Task<bool> DoesEstateExist(CategoryType category, string estateNumber)
        {
            return category switch
            {
                CategoryType.Sale => await _context.SaleEstates.AnyAsync(x => x.EstateNumber == estateNumber && x.User.UserType != nameof(UserType.Admin)),
                CategoryType.Rent => await _context.RentEstates.AnyAsync(x => x.EstateNumber == estateNumber && x.User.UserType != nameof(UserType.Admin)),
                CategoryType.DailyRent => await _context.DailyRentEstates.AnyAsync(x => x.EstateNumber == estateNumber && x.User.UserType != nameof(UserType.Admin)),
                CategoryType.Entertainment => await _context.EntertainmentEstates.AnyAsync(x => x.EstateNumber == estateNumber && x.User.UserType != nameof(UserType.Admin)),
                _ => true
            };
        }


        private async Task<MyEstateDetailsViewModel?> SaleEstateInfo(long id)
        {
            var estate = await _context.SaleEstates
                .Where(x => x.Id == id)
                .Select(x => new MyEstateDetailsViewModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.EstateName,
                    Addresss = x.Address(Language.Ar),
                    Area = x.EstateArea,
                    Images = x.Images.Select(i => new BaseImageItemDto
                    {
                        Id = i.Id,
                        Url = MyConstants.DomainUrl + i.Image
                    }).ToHashSet(),
                    AveragePrice = x.UserPriceToEstates.Count != 0 ? x.UserPriceToEstates.Average(p => p.Price) : 0,
                    Number = x.EstateNumber,
                    IsVisible = x.IsShow,
                    IsDevelopable = x.Developable,
                    IsReserved = x.IsReserved,
                    IsSold = x.IsSold,
                    Description = x.EstateDescription,
                    Deposit = x.Deposit,
                    Price = x.EstatePrice,
                    FinalSalePrice = x.FinalEstatePrice,
                    Specs = x.SpecificationValues.Select(s => new SpecificationItemDto
                    {
                        Id = s.Id,
                        Name = $"{s.EstateTypeSpecification.Specification.Name(Language.Ar)}: {s.SpecificationValue}",
                        Icon = $"{MyConstants.DomainUrl}{s.EstateTypeSpecification.Specification.Icon}"
                    }).ToHashSet(),
                    ReservationRequestsCount = x.ReservationRequests.Count(),
                    EvaluationRequestsCount = x.RatingRequests.Where(x => x.RatingStatus != RatingStatus.New).Count(),
                    PurchaseRequestsCount = x.PurchaseRequests.Where(x => x.Status != PurchaseStatus.Canceled).Count(),
                    IsAccepted = x.IsReviewed,
                    IsRejected = !x.IsReviewed && !string.IsNullOrWhiteSpace(x.RejectionReason)
                }).SingleOrDefaultAsync();

            return estate;
        }


        private async Task<MyEstateDetailsViewModel?> RentEstateInfo(long id)
        {
            var estate = await _context.RentEstates
                .Where(x => x.Id == id)
                .Select(x => new MyEstateDetailsViewModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.EstateName,
                    Addresss = x.Address(Language.Ar),
                    Area = x.EstateArea,
                    Images = x.Images.Select(i => new BaseImageItemDto
                    {
                        Id = i.Id,
                        Url = MyConstants.DomainUrl + i.Image
                    }).ToHashSet(),
                    Number = x.EstateNumber,
                    IsVisible = x.IsShow,
                    IsDevelopable = x.Developable,
                    IsReserved = x.IsReserved,
                    IsRented = x.IsRented,
                    Description = x.EstateDescription,
                    Price = x.MonthRentPrice,
                    Specs = x.SpecificationValues.Select(s => new SpecificationItemDto
                    {
                        Id = s.Id,
                        Name = $"{s.EstateTypeSpecification.Specification.Name(Language.Ar)}: {s.SpecificationValue}",
                        Icon = $"{MyConstants.DomainUrl}{s.EstateTypeSpecification.Specification.Icon}"
                    }).ToHashSet(),
                    ReservationRequestsCount = x.ReservationRequests.Count(),
                    EvaluationRequestsCount = x.RatingRequests.Where(x => x.RatingStatus != RatingStatus.New).Count(),
                    RentRequestsCount = x.RentRequests.Where(x => x.Status != RentStatus.Canceled).Count(),
                    IsAccepted = x.IsReviewed,
                    IsRejected = !x.IsReviewed && !string.IsNullOrWhiteSpace(x.RejectionReason)
                }).SingleOrDefaultAsync();

            return estate;
        }

        private async Task<MyEstateDetailsViewModel?> DailyRentEstateInfo(long id)
        {
            var estate = await _context.DailyRentEstates
                .Where(x => x.Id == id)
                .Select(x => new MyEstateDetailsViewModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.EstateName,
                    Addresss = x.Address(Language.Ar),
                    Area = x.EstateArea,
                    Images = x.Images.Select(i => new BaseImageItemDto
                    {
                        Id = i.Id,
                        Url = MyConstants.DomainUrl + i.Image
                    }).ToHashSet(),
                    Number = x.EstateNumber,
                    IsVisible = x.IsShow,
                    IsDevelopable = x.Developable,
                    IsRented = x.IsRented,
                    Description = x.EstateDescription,
                    Price = x.DayRentPrice,
                    Specs = x.SpecificationValues.Select(s => new SpecificationItemDto
                    {
                        Id = s.Id,
                        Name = $"{s.EstateTypeSpecification.Specification.Name(Language.Ar)}: {s.SpecificationValue}",
                        Icon = $"{MyConstants.DomainUrl}{s.EstateTypeSpecification.Specification.Icon}"
                    }).ToHashSet(),
                    DailyRentRequestsCount = x.Requests.Where(x => x.Status != DailyRentStatus.Canceled).Count(),
                    IsAccepted = x.IsReviewed,
                    IsRejected = !x.IsReviewed && !string.IsNullOrWhiteSpace(x.RejectionReason)
                }).SingleOrDefaultAsync();

            return estate;
        }


        private async Task<MyEstateDetailsViewModel?> EntertainmentEstateInfo(long id)
        {
            var estate = await _context.EntertainmentEstates
                .Where(x => x.Id == id)
                .Select(x => new MyEstateDetailsViewModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.EstateName,
                    Addresss = x.Address(Language.Ar),
                    Area = x.EstateArea,
                    Images = x.Images.Select(i => new BaseImageItemDto
                    {
                        Id = i.Id,
                        Url = MyConstants.DomainUrl + i.Image
                    }).ToHashSet(),
                    Number = x.EstateNumber,
                    IsVisible = x.IsShow,
                    IsDevelopable = x.Developable,
                    IsRented = x.IsRented,
                    Description = x.EstateDescription,
                    Price = x.DayRentPrice,
                    Specs = x.SpecificationValues.Select(s => new SpecificationItemDto
                    {
                        Id = s.Id,
                        Name = $"{s.EstateTypeSpecification.Specification.Name(Language.Ar)}: {s.SpecificationValue}",
                        Icon = $"{MyConstants.DomainUrl}{s.EstateTypeSpecification.Specification.Icon}"
                    }).ToHashSet(),
                    EntertainmentRentRequestsCount = x.Requests.Where(x => x.Status != DailyRentStatus.Canceled).Count(),
                    IsAccepted = x.IsReviewed,
                    IsRejected = !x.IsReviewed && !string.IsNullOrWhiteSpace(x.RejectionReason)
                }).SingleOrDefaultAsync();

            return estate;
        }


        async Task<SaleEstate> CreateSaleEstate(CreateEstateViewModel model)
        {
            var images = new List<SaleEstateImage>();
            foreach (var image in model.Images)
            {
                images.Add(new SaleEstateImage
                {
                    Image = await _sharedService.UploadFileUsingApi(new UploadImageUsingApiDto { image = image, fileName = (int)FileName.Estates }),
                });
            }

            var saleEstate = new SaleEstate
            {
                EstateNumber = model.UniqueNumber,
                EstateName = model.Name,
                Deposit = model.Deposit,
                Developable = model.IsDevelopable,
                EstateArea = model.Area,
                EstateDescription = model.Description,
                EstateFeatures = model.Features,
                EstatePrice = model.Price,
                EstateTypeId = model.EstateTypeId,
                IsReviewed = true,
                IsShow = true,
                Lat = model.Lat,
                Lng = model.Lng,
                Location = model.Location,
                RegionId = model.RegionId,
                UserId = _currentUser.UserId,
                Images = images
            };

            await _context.SaleEstates.AddAsync(saleEstate);
            await _context.SaveChangesAsync();

            return saleEstate;
        }

        async Task<RentEstate> CreateRentEstate(CreateEstateViewModel model)
        {
            var images = new List<RentEstateImage>();
            foreach (var image in model.Images)
            {
                images.Add(new RentEstateImage
                {
                    Image = await _sharedService.UploadFileUsingApi(new UploadImageUsingApiDto { image = image, fileName = (int)FileName.Estates }),
                });
            }

            var rentEstate = new RentEstate
            {
                EstateNumber = model.UniqueNumber,
                EstateName = model.Name,
                Developable = model.IsDevelopable,
                EstateArea = model.Area,
                EstateDescription = model.Description,
                EstateFeatures = model.Features,
                MonthRentPrice = model.Price,
                EstateTypeId = model.EstateTypeId,
                IsReviewed = true,
                IsShow = true,
                Lat = model.Lat,
                Lng = model.Lng,
                Location = model.Location,
                RegionId = model.RegionId,
                UserId = _currentUser.UserId,
                Images = images
            };

            await _context.RentEstates.AddAsync(rentEstate);
            await _context.SaveChangesAsync();

            return rentEstate;
        }


        async Task<DailyRentEstate> CreateDailyRentEstate(CreateEstateViewModel model)
        {
            var images = new List<DailyRentEstateImage>();
            foreach (var image in model.Images)
            {
                images.Add(new DailyRentEstateImage
                {
                    Image = await _sharedService.UploadFileUsingApi(new UploadImageUsingApiDto { image = image, fileName = (int)FileName.Estates }),
                });
            }

            var dailyRentEstate = new DailyRentEstate
            {
                EstateNumber = model.UniqueNumber,
                EstateName = model.Name,
                Developable = model.IsDevelopable,
                EstateArea = model.Area,
                EstateDescription = model.Description,
                EstateFeatures = model.Features,
                DayRentPrice = model.Price,
                EstateTypeId = model.EstateTypeId,
                IsReviewed = true,
                IsShow = true,
                Lat = model.Lat,
                Lng = model.Lng,
                Location = model.Location,
                RegionId = model.RegionId,
                UserId = _currentUser.UserId,
                Images = images,
            };

            await _context.DailyRentEstates.AddAsync(dailyRentEstate);
            await _context.SaveChangesAsync();

            return dailyRentEstate;
        }


        async Task<EntertainmentEstate> CreateEntertainmentEstate(CreateEstateViewModel model)
        {
            var images = new List<EntertainmentEstateImage>();
            foreach (var image in model.Images)
            {
                images.Add(new EntertainmentEstateImage
                {
                    Image = await _sharedService.UploadFileUsingApi(new UploadImageUsingApiDto { image = image, fileName = (int)FileName.Estates }),
                });
            }

            var entertainmentEstate = new EntertainmentEstate
            {
                EstateNumber = model.UniqueNumber,
                EstateName = model.Name,
                Developable = model.IsDevelopable,
                EstateArea = model.Area,
                EstateDescription = model.Description,
                EstateFeatures = model.Features,
                DayRentPrice = model.Price,
                EstateTypeId = model.EstateTypeId,
                IsReviewed = true,
                IsShow = true,
                Lat = model.Lat,
                Lng = model.Lng,
                Location = model.Location,
                RegionId = model.RegionId,
                UserId = _currentUser.UserId,
                Images = images
            };

            await _context.EntertainmentEstates.AddAsync(entertainmentEstate);
            await _context.SaveChangesAsync();

            return entertainmentEstate;
        }
        #endregion
    }
}
