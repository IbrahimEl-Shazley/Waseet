using AAITHelper.ModelHelper;
using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.Interfaces.General;
using Wasit.Services.ViewModels.Users;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DUsersService : IDUsersServices
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;

        public DUsersService(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }


        #region Owners
        public async Task<List<OwnerViewModel>> Owners()
        {
            var owners = await _context.Users
                .Where(x => x.UserType == nameof(UserType.Owner))
                .Include(x => x.Region.City)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new OwnerViewModel
                {
                    Id = x.Id,
                    UserName = x.User_Name,
                    Phone = x.PhoneNumber,
                    ProfilePicture = MyConstants.DomainUrl + x.ImgProfile,
                    IdentityNo = x.IDNumber,
                    City = x.Region.City.NameAr,
                    Region = x.Region.NameAr,
                    IsActive = x.IsActive
                }).ToListAsync();

            return owners;
        }

        public async Task<AdditionalOwnerInfoViewModel> AdditionalOwnerInfo(string id)
        {
            var data = await _context.Users
                 .Where(x => x.Id == id)
                 .AsNoTracking()
                 .Select(x => new AdditionalOwnerInfoViewModel
                 {
                     BankAccountNo = x.AccNumber,
                     BankAccountOwnerName = x.AccOwnerName,
                     BankName = x.BankName,
                     IbanNumber = x.IbanNumber
                 }).SingleOrDefaultAsync();

            return data;
        }
        #endregion

        #region Brokers
        public async Task<List<BrokerViewModel>> Brokers()
        {
            var brokers = await _context.Users
                .Where(x => x.UserType == nameof(UserType.Broker))
                .Include(x => x.Region.City)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new BrokerViewModel
                {
                    Id = x.Id,
                    UserName = x.User_Name,
                    Phone = x.PhoneNumber,
                    City = x.Region.City.NameAr,
                    Region = x.Region.NameAr,
                    IsActive = x.IsActive,
                    AccountType = EnumHelper.BrokerAccountType(x.AccountType, Language.Ar),
                    ProfilePicture = MyConstants.DomainUrl + x.ImgProfile
                }).ToListAsync();

            return brokers;
        }

        public async Task<IndividualBrokerViewModel> AdditionalIndividualBrokerInfo(string id)
        {
            var data = await _context.Users
                 .Where(x => x.Id == id)
                 .AsNoTracking()
                 .Select(x => new IndividualBrokerViewModel
                 {
                     BrokerageDocumentNo = x.BrokerageDocumentNo,
                     BankAccountNo = x.AccNumber,
                     BankAccountOwnerName = x.AccOwnerName,
                     BankName = x.BankName,
                     IbanNumber = x.IbanNumber
                 }).SingleOrDefaultAsync();

            return data;
        }

        public async Task<FaciltyBrokerViewModel> AdditionalFaciltyBrokerInfo(string id)
        {
            var data = await _context.Users
                 .Where(x => x.Id == id)
                 .AsNoTracking()
                 .Select(x => new FaciltyBrokerViewModel
                 {
                     CommercialNo = x.CommercialNo,
                     FacilityType = EnumHelper.BrokerFacilityType(x.FacilityType, Language.Ar),
                     WorkingDocumentNo = x.WorkingNo,
                     BankAccountNo = x.AccNumber,
                     BankAccountOwnerName = x.AccOwnerName,
                     BankName = x.BankName,
                     IbanNumber = x.IbanNumber
                 }).SingleOrDefaultAsync();

            return data;
        }
        #endregion

        #region Delegates
        public async Task<List<DelegateViewModel>> Delegates()
        {
            var delegates = await _context.Users
                .Where(x => x.UserType == nameof(UserType.Delegate))
                .Include(x => x.Region.City)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new DelegateViewModel
                {
                    Id = x.Id,
                    UserName = x.User_Name,
                    Phone = x.PhoneNumber,
                    ProfilePicture = MyConstants.DomainUrl + x.ImgProfile,
                    IdentityNo = x.IDNumber,
                    City = x.Region.City.NameAr,
                    Region = x.Region.NameAr,
                    IsActive = x.IsActive
                }).ToListAsync();

            return delegates;
        }


        public async Task<AdditionalDelegateInfoViewModel> AdditionalDelegateInfo(string id)
        {
            var data = await _context.Users
                 .Where(x => x.Id == id)
                 .AsNoTracking()
                 .Select(x => new AdditionalDelegateInfoViewModel
                 {
                     WorkingDocumentNo = x.WorkingNo,
                     BankAccountNo = x.AccNumber,
                     BankAccountOwnerName = x.AccOwnerName,
                     BankName = x.BankName,
                     IbanNumber = x.IbanNumber
                 }).SingleOrDefaultAsync();

            return data;
        }
        #endregion

        #region Developers
        public async Task<List<DeveloperViewModel>> Developers()
        {
            var owners = await _context.Users
                .Where(x => x.UserType == nameof(UserType.Developer))
                .Include(x => x.Region.City)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new DeveloperViewModel
                {
                    Id = x.Id,
                    UserName = x.User_Name,
                    Phone = x.PhoneNumber,
                    ProfilePicture = MyConstants.DomainUrl + x.ImgProfile,
                    City = x.Region.City.NameAr,
                    Region = x.Region.NameAr,
                    IsActive = x.IsActive
                }).ToListAsync();

            return owners;
        }


        public async Task<AdditionalDeveloperInfoViewModel> AdditionalDeveloperInfo(string id)
        {
            var data = await _context.Users
                 .Where(x => x.Id == id)
                 .AsNoTracking()
                 .Select(x => new AdditionalDeveloperInfoViewModel
                 {
                     CoverPhoto = MyConstants.DomainUrl + x.CoverPhoto,
                     Description = x.Description ?? "لا يوجد",
                     Email = x.Email ?? "لا يوجد",
                     BankAccountNo = x.AccNumber,
                     BankAccountOwnerName = x.AccOwnerName,
                     BankName = x.BankName,
                     IbanNumber = x.IbanNumber
                 }).SingleOrDefaultAsync();

            return data;
        }
        #endregion

        #region Shared
        public async Task<ApplicationDbUser?> GetUserById(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<(bool isSuccess, string message)> ChangeActivation(string userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                user.IsActive = !user.IsActive;
                await _context.SaveChangesAsync();

                if (!user.IsActive)
                {
                    await _notificationService.SendNotifyAsync(new NotificationModel
                    {
                        UserId = userId,
                        TextAr = "تم تعطيل حسابك من قبل الادارة",
                        TextEn = "Your account has been deactivated by the app management",
                        Type = NotifyTypes.DeactivateUser
                    });
                }

                return (true, "تم تغيير حالة المستخدم بنجاح");
            }
            catch
            {
                return (false, "حدث خطأ ما");
            }
        }

        public async Task<(bool isSuccess, string message)> Remove(string userId)
        {
            try
            {

                var user = await _context.Users.FindAsync(userId);
                var hasEstates = _context.SaleEstates.Any(se => se.UserId == userId) ||
                 _context.RentEstates.Any(re => re.UserId == userId) ||
                 _context.DailyRentEstates.Any(de => de.UserId == userId) ||
                 _context.EntertainmentEstates.Any(ee => ee.UserId == userId) ||
                 _context.SaleReservationRequests.Any(sr => sr.UserId == userId) ||
                 _context.SaleRatingRequests.Any(sr => sr.ProviderId == userId) ||
                 _context.RentReservationRequests.Any(rr => rr.UserId == userId) ||
                 _context.RentRatingRequests.Any(rr => rr.ProviderId == userId) ||
                 _context.DailyRentRequests.Any(rr => rr.UserId == userId) ||
                 _context.EntertainmentRequests.Any(er => er.UserId == userId);

                if (!hasEstates)
                {
                    user.IsDeleted = true;
                    user.PhoneNumber += Guid.NewGuid().ToString();
                    user.Email += Guid.NewGuid().ToString();
                    user.NormalizedEmail += Guid.NewGuid().ToString();
                    user.UserName += Guid.NewGuid().ToString();
                    user.NormalizedUserName += Guid.NewGuid().ToString();

                    _context.Users.Update(user);

                    await _notificationService.SendNotifyAsync(new NotificationModel
                    {
                        UserId = userId,
                        TextAr = "تم حذف حسابك من قبل الادارة",
                        TextEn = "Your account has been deleted by the app management",
                        Type = NotifyTypes.RemoveUser
                    });

                    await _context.SaveChangesAsync();

                    return (true, "تم حذف المستخدم بنجاح");
                }
                else
                {
                    return (false, "لا يمكن حذف هذا الحساب لانه يمتلك عقارات مسجله");

                }
            }
           
            catch
            {
                return (false, "حدث خطأ ما");
            }
        }

        public async Task<(bool isSuccess, string message)> SendNotification(string userId, string title, string content)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user is null)
                    return (false, "هذا المستخدم غير موجود");

                var isSuccess = await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    Title = title,
                    UserId = userId,
                    TextAr = content,
                    TextEn = content,
                    Type = NotifyTypes.NotifyFromDashBord
                });

                if (!isSuccess)
                {
                    return (false, "حدث خطأ ما");
                }

                return (true, "تم حذف المستخدم بنجاح");
            }
            catch
            {
                return (false, "حدث خطأ ما");
            }
        }
        #endregion
    }
}
