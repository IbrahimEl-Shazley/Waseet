using Microsoft.AspNetCore.Mvc;
using Wasit.Services.Interfaces.Dashboard;

namespace Wasit.Dashboard.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IDContactUsService _contactUsService;

        public ContactUsController(IDContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }



        public async Task<IActionResult> Index()
        {
            var result = await _contactUsService.ContactUsMessages();
            return View(result);
        }


    }
}
