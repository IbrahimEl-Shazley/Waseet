using Microsoft.AspNetCore.Mvc;
using Wasit.Services.Interfaces.Dashboard;

namespace Wasit.Dashboard.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly IDNotificationsService _notificationService;

        public NotificationsController(IDNotificationsService notificationService)
        {
            _notificationService = notificationService;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Send(string text, string type)
        {
            var result = await _notificationService.Send(text, type);
            return Json(result);
        }
    }
}
