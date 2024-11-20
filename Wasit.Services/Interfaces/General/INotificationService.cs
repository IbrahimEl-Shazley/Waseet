using Wasit.Core.Enums;
using Wasit.Services.DTOs.General;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;

namespace Wasit.Services.Interfaces.General
{
    public interface INotificationService : IBaseService
    {
        Task<bool> Send(Notification dto, NotificationTypeEnum verificationType);
        Task<bool> Send(Notification dto);
        Task<bool> SendNotifyAsync(NotificationModel model);
        Task<bool> SendNotifyToDashboardAsync(NotificationModel model);
    }
}
