using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.City;

namespace Wasit.Dashboard.Controllers
{
    public class CitiesController : Controller
    {
        private readonly IDCityService _cityService;
        private readonly IToastNotification _toastNotification;

        public CitiesController(IDCityService cityService, IToastNotification toastNotification)
        {
            _cityService = cityService;
            _toastNotification = toastNotification;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _cityService.Cities();
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateCityViewModel model)
        {
            var result = await _cityService.CreateCity(model);
            return Json(result);
        }


        public async Task<IActionResult> Update(long id)
        {
            var result = await _cityService.CityDetails(id);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Update(long id, UpdateCityViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _cityService.UpdateCity(id, model);
            if (!result.isSuccess)
                return View(model);

            _toastNotification.AddSuccessToastMessage(result.message, new ToastrOptions { Title = "" });
            return RedirectToAction(nameof(Index));
        }


        [HttpDelete]
        public async Task<IActionResult> Remove(long id)
        {
            var result = await _cityService.RemoveCity(id);
            return Json(new { success = result.isSuccess, message = result.message });
        }
    }
}
