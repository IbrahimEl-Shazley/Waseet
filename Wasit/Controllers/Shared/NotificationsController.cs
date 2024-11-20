using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.API.Controllers.Shared
{
    [ApiExplorerSettings(GroupName = "Shared")]
    public class NotificationsController : BaseController
    {
        private readonly INotificationsService _notificationService;

        public NotificationsController(INotificationsService notificationService)
        {
            _notificationService = notificationService;
        }


        [HttpGet("ListNotifications")]
        public async Task<IActionResult> ListNotifications([Required] int pageNumber = 1)
        {
            return _OK(await _notificationService.Notifications(UserId, pageNumber));
        }


        [HttpDelete("RemoveAllNotifications")]
        public async Task<IActionResult> RemoveAllNotifications()
        {
            return _OK(await _notificationService.RemoveAllNotifications(UserId));
        }


        [HttpDelete("RemoveNotification")]
        public async Task<IActionResult> RemoveNotification(int id)
        {
            return _OK(await _notificationService.RemoveNotification(UserId, id));
        }
    }
}
