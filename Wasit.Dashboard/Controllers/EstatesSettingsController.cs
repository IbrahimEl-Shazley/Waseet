using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.EstateSettings;

namespace Wasit.Dashboard.Controllers
{
    public class EstatesSettingsController : Controller
    {
        private readonly IDEstatesSettingService _estatesSettingService;
        private readonly IDSharedService _sharedService;

        public EstatesSettingsController(IDEstatesSettingService estatesSettingService, IDSharedService sharedService)
        {
            _estatesSettingService = estatesSettingService;
            _sharedService = sharedService;
        }


        #region Types
        public async Task<IActionResult> Types(long categoryId = 0)
        {
            var types = await _estatesSettingService.EstateTypes(categoryId);
            ViewBag.Categories = new SelectList(await _sharedService.Categories(), "Id", "Name", categoryId);

            return View(types);
        }


        public async Task<IActionResult> Create()
        {
            //ViewBag.Categories = new SelectList(await _sharedService.Categories(), "Id", "Name");
            ViewBag.Specifications = new SelectList(await _sharedService.Specifications(), "Value", "Text");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateEstateTypeViewModel model)
        {
            var result = await _estatesSettingService.CreateEstateType(model);
            if (result.isSuccess)
                return Json(result);

            //ViewBag.Categories = new SelectList(await _sharedService.Categories(), "Id", "Name");
            ViewBag.Specifications = await _sharedService.Specifications();

            return View(model);
        }


        public async Task<IActionResult> Update(long id)
        {
            ViewBag.Specifications = await _sharedService.Specifications();
            var result = await _estatesSettingService.EstateTypeById(id);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Update(long id, UpdateEstateTypeViewModel model)
        {
            var result = await _estatesSettingService.EditEstateType(id, model);

            return Json(new { success = result.isSuccess, message = result.message });

        }


        [HttpDelete]
        public async Task<IActionResult> Remove(long estateTypeId)
        {
            (bool isSuccess, string message) result = await _estatesSettingService.Remove(estateTypeId);
            return Json(result);
        }
        #endregion

        #region EstateTypeSpecifications
        public async Task<IActionResult> EstateTypeSpecifications(long id)
        {
            var result = await _estatesSettingService.EstateTypeSpecifications(id);
            return View(result);
        }
        #endregion

        #region Specifications
        public async Task<IActionResult> Specifications()
        {
            var result = await _estatesSettingService.Specifications();
            return View(result);
        }


        public async Task<IActionResult> CreateSpecification()
        {
            ViewBag.SpecificationTypes = new SelectList(await _estatesSettingService.SpecificationTypes(), "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateSpecification(CreateSpecificationViewModel model)
        {
            var result = await _estatesSettingService.CreateSpecification(model);
            if (result.isSuccess)
                return Json(result);

            ViewBag.SpecificationTypes = new SelectList(await _estatesSettingService.SpecificationTypes(), "Id", "Name");
            return View(model);
        }


        public async Task<IActionResult> EditSpecification(long id)
        {
            var specification = await _estatesSettingService.GetSpecificationDetails(id);
            if (specification == null)
                return NotFound();

            ViewBag.SpecificationTypes = new SelectList(await _estatesSettingService.SpecificationTypes(), "Id", "Name", (long)specification.Type);
            return View(specification);
        }


        [HttpPost]
        public async Task<IActionResult> EditSpecification(long id, EditSpecificationViewModel model)
        {
            var result = await _estatesSettingService.EditSpecification(id, model);
            if (result.isSuccess)
            {
                return RedirectToAction(nameof(Specifications));
            }

            ViewBag.SpecificationTypes = new SelectList(await _estatesSettingService.SpecificationTypes(), "Id", "Name");
            return View(model);
        }


        [HttpDelete]
        public async Task<IActionResult> RemoveSpecification(long id)
        {
            (bool isSuccess, string message) result = await _estatesSettingService.RemoveSpecification(id);
            return Json(result);
        }
        #endregion
    }
}
