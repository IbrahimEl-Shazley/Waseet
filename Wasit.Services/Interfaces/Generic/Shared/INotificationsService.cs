using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;

namespace Wasit.Services.Interfaces.Generic.Shared
{
    public interface INotificationsService : IBaseService
    {
        Task<PageDTO<NotificationItemDto>> Notifications(string userId, int pageNumber);
        Task<bool> RemoveAllNotifications(string userId);
        Task<bool> RemoveNotification(string userId, int notificationId);
    }
}
