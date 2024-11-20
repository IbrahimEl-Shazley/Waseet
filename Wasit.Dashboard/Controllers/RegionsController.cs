using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.Region;

namespace Wasit.Dashboard.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IDRegionService _regionService;
        private readonly IToastNotification _toastNotification;

        public RegionsController(IDRegionService regionService, IToastNotification toastNotification)
        {
            _regionService = regionService;
            _toastNotification = toastNotification;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _regionService.Regions();
            ViewBag.Cities = new SelectList(await _regionService.Cities(), "Id", "Name");
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateRegionViewModel model)
        {
            var result = await _regionService.CreateRegion(model);
            return Json(result);
        }


        public async Task<IActionResult> Update(long id)
        {
            var result = await _regionService.RegionDetails(id);
            ViewBag.Cities = new SelectList(await _regionService.Cities(), "Id", "Name", result?.CityId);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Update(long id, UpdateRegionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cities = new SelectList(await _regionService.Cities(), "Id", "Name", model.CityId);
                return View(model);
            }

            var result = await _regionService.UpdateRegion(id, model);
            if (!result.isSuccess)
            {
                ViewBag.Cities = new SelectList(await _regionService.Cities(), "Id", "Name", model.CityId);
                return View(model);
            }

            _toastNotification.AddSuccessToastMessage(result.message, new ToastrOptions { Title = "" });
            return RedirectToAction(nameof(Index));
        }


        [HttpDelete]
        public async Task<IActionResult> Remove(long id)
        {
            var result = await _regionService.RemoveRegion(id);
            return Json(new { success = result.isSuccess, message = result.message });

        }


    }
}
