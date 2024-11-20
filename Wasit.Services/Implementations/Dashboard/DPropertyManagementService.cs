using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.Interfaces.General;
using Wasit.Services.ViewModels.PropertyManagement.MaintainanceMangement;
using Wasit.Services.ViewModels.PropertyManagement.RentManagement;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DPropertyManagementService : IDPropertyManagementService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;
        public DPropertyManagementService(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<List<RentManagementRequestViewModel>> RentManagementRequests()
        {
            return await _context.RentalManagementOrders
                .Where(x => !x.IsCanceled)
                .Include(x => x.User)
                .AsNoTracking()
                .Select(x => new RentManagementRequestViewModel
                {
                    Id = x.Id,
                    UserName = x.User.User_Name,
                    PropertyName = x.EstateName,
                    UniqueNumber = x.EstateNumber,
                    IsApproved = x.IsApproved,
                    IsCanceled = x.IsCanceled,
                    IsTerminated = x.IsTerminated,
                    Status = EnumHelper.RentManagementRequestStatus(x.OrderStatus, Language.Ar)
                }).OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<(bool isSuccess, string message)> AcceptRentManagementRequest(long id)
        {
            var request = await _context.RentalManagementOrders
                .SingleOrDefaultAsync(x => x.Id == id);

            if (request == null)
                return (false, "لم يتم العثور على الطلب");

            request.IsApproved = true;
            _context.RentalManagementOrders.Update(request);
            await _context.SaveChangesAsync();

            return (true, "تم قبول الطلب بنجاح");
        }


        public async Task<(bool isSuccess, string message)> RejectRentManagementRequest(long id)
        {
            var rentManagementRequest = await _context.RentalManagementOrders.FindAsync(id);

            if (rentManagementRequest is null)
                return (false, "لم يتم العثور على الطلب");

            rentManagementRequest.IsDeleted = true;
            _context.RentalManagementOrders.Update(rentManagementRequest);
            await _context.SaveChangesAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = "تم رفض طلبك لادارة ايجار العقار/ات الخاصة بك",
                TextEn = "Your rent mangement request has been rejected",
                Type = NotifyTypes.RejectedRentalManagementRequest,
                UserId = rentManagementRequest.UserId
            });

            return (true, "تم رفض الطلب بنجاح");
        }


        public async Task<RentManagementRequestDetailsViewModel?> RentManagementRequestDetails(long id)
        {
            var request = await _context.RentalManagementOrders
                .Where(x => x.Id == id)
                .Include(x => x.User)
                .Include(x => x.EstateApartments)
                .AsNoTracking()
                .Select(x => new RentManagementRequestDetailsViewModel
                {
                    Id = x.Id,
                    Address = x.Address(Language.Ar),
                    ApartmentsCount = x.EstateApartments.Count,
                    Image = MyConstants.DomainUrl + x.EstateImage,
                    Name = x.EstateName,
                    UniqueNumber = x.EstateNumber,
                    UserName = x.User.User_Name,
                    Apartments = x.EstateApartments.Select(a => new ApartmentViewModel
                    {
                        Id = a.Id,
                        Name = a.ApartmentName,
                        Number = a.Number,
                        Value = a.RentalPrice,
                        PaymentDate = EnumHelper.PaymentDeadline(a.PaymentDeadline, Language.Ar),
                        IsPaid = a.IsRentPaid ? "تم السداد" : "لم يتم السداد بعد"
                    }).ToList()
                }).SingleOrDefaultAsync();

            if (request is null)
                return null;

            return request;
        }


        public async Task<List<MaintainanceManagementRequestViewModel>> MaintainanceManagementRequests(RentalManagementOrderStatus status)
        {
            return await _context.MaintenanceOrders
                .Include(x => x.User)
                .Include(x => x.Provider)
                .AsNoTracking()
                .Select(x => new MaintainanceManagementRequestViewModel
                {
                    Id = x.Id,
                    UserName = x.User.User_Name,
                    PhoneNumber = x.User.PhoneNumber,
                    EstateName = x.EstateName,
                    DelegateName= x.Provider.User_Name,
                    DelegatePhone = x.Provider.PhoneNumber,
                    Rating = x.UserRate,
                    Status = x.OrderStatus
                }).OrderByDescending(x => x.Id).ToListAsync();
        }
    }
}
