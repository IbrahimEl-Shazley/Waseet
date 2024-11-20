using Microsoft.AspNetCore.Mvc;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.Advertisments;

namespace Wasit.Dashboard.Controllers
{
    public class AdvertismentsController : Controller
    {
        private readonly IDAdvertismentService _avertismentService;

        public AdvertismentsController(IDAdvertismentService avertismentService)
        {
            _avertismentService = avertismentService;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _avertismentService.Advertisments();
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertismentViewModel data)
        {
            var result = await _avertismentService.CreateAdvertisment(data);
            //return Json(result);
            return Json(new { success = result.isSuccess, message = result.message });

        }


        [HttpDelete]
        public async Task<IActionResult> Remove(long id)
        {
            var result = await _avertismentService.Remove(id);
            return Json(result);
        }
    }
}
