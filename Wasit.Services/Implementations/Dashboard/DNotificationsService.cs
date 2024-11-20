using AAITHelper.ModelHelper;
using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.SettingTables;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.Helpers.Notifications;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.Interfaces.General;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DNotificationsService : IDNotificationsService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;
        public DNotificationsService(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }


        public async Task<(bool isSuccess, string message)> Send(string message, string userType)
        {
            var userIds = await _context.Users.AsNoTracking().Where(x => x.UserType == userType && x.AllowNotify).Select(x => x.Id).ToArrayAsync();

            foreach (var userId in userIds)
            {     
                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = message,
                    TextEn = message,
                    Type = NotifyTypes.NotifyFromDashBord,
                    UserId = userId
                });
            }

            return (true, "تم الارسال بنجاح");
        }
 
           
    }
}
