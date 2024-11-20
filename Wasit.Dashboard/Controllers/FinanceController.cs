using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Wasit.Core.Enums;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.Profits.AllPackages;

namespace Wasit.Dashboard.Controllers
{
    public class FinanceController : Controller
    {
        private readonly IDProfitService _profitService;
        private readonly IToastNotification _toastNotification;

        public FinanceController(IDProfitService profitService, IToastNotification toastNotification)
        {
            _profitService = profitService;
            _toastNotification = toastNotification;
        }


        public async Task<IActionResult> Profits()
        {
            var result = await _profitService.Profits();
            return View(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update(ServiceType type, double value)
        {
            var result = await _profitService.Update(type, value);
            return Json(result);
        }


        #region BrokerPackages
        public async Task<IActionResult> BrokersPackages()
        {
            var result = await _profitService.BrokersPackages();
            return View(result);
        }


        public IActionResult CreateBrokerPackage()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateBrokerPackage(CreateBrokerPackageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _profitService.CreateBrokerPackage(model);
            if (result.isSuccess)
                _toastNotification.AddSuccessToastMessage(result.message, new ToastrOptions { Title = "" });

            return RedirectToAction(nameof(BrokersPackages));
        }


        public async Task<IActionResult> UpdateBrokerPackage(long id)
        {
            var result = await _profitService.BrokerPackageDetails(id);
            if (result is null)
                return NotFound();

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBrokerPackage(long id, UpdateBrokerPackageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _profitService.UpdateBrokerPackage(id, model);
            if (result.isSuccess)
                _toastNotification.AddSuccessToastMessage(result.message, new ToastrOptions { Title = "" });

            return RedirectToAction(nameof(BrokersPackages));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteBrokerPackage(long id)
        {
            var result = await _profitService.DeleteBrokerPackage(id);
            return Json(result);
        }
        #endregion


        #region AddPriceToEstatePackages
        public async Task<IActionResult> AddPriceToEstatePackages()
        {
            var result = await _profitService.AddPriceToEstatesPackages();
            return View(result);
        }


        public IActionResult CreateAddPriceToEstatePackage()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateAddPriceToEstatePackage(CreateAddPriceToEstatePackageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _profitService.CreateAddPriceToEstatePackage(model);
            if (result.isSuccess)
                _toastNotification.AddSuccessToastMessage(result.message, new ToastrOptions { Title = "" });

            return RedirectToAction(nameof(AddPriceToEstatePackages));
        }


        public async Task<IActionResult> UpdateAddPriceToEstatePackage(long id)
        {
            var result = await _profitService.AddPriceToEstatePackageDetails(id);
            if (result is null)
                return NotFound();

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAddPriceToEstatePackage(long id, UpdateAddPriceToEstatePackageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _profitService.UpdateAddPriceToEstatePackage(id, model);
            if (result.isSuccess)
                _toastNotification.AddSuccessToastMessage(result.message, new ToastrOptions { Title = "" });

            return RedirectToAction(nameof(AddPriceToEstatePackages));
        }
        
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAddPriceToEstatePackage(long id)
        {      
            var result = await _profitService.DeleteAddPriceToEstatePackage(id);
            return Json(result);
        }
        #endregion


        #region DeveloperPackages
        public async Task<IActionResult> DeveloperPackages()
        {
            var result = await _profitService.DeveloperPackages();
            return View(result);
        }


        public IActionResult CreateDeveloperPackage()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateDeveloperPackage(CreateDeveloperPackageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _profitService.CreateDeveloperPackage(model);
            if (result.isSuccess)
                _toastNotification.AddSuccessToastMessage(result.message, new ToastrOptions { Title = "" });

            return RedirectToAction(nameof(DeveloperPackages));
        }


        public async Task<IActionResult> UpdateDeveloperPackage(long id)
        {
            var result = await _profitService.DeveloperPackageDetails(id);
            if (result is null)
                return NotFound();

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateDeveloperPackage(long id, UpdateDeveloperPackageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _profitService.UpdateDeveloperPackage(id, model);
            if (result.isSuccess)
                _toastNotification.AddSuccessToastMessage(result.message, new ToastrOptions { Title = "" });

            return RedirectToAction(nameof(DeveloperPackages));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDeveloperPackage(long id)
        {
            var result = await _profitService.DeleteDeveloperPackage(id);
            return Json(result);
        }
        #endregion


        #region ServicesPackages
        public async Task<IActionResult> ServicesPackages()
        {
            var result = await _profitService.ServicesPackages();
            return View(result);
        }


        public IActionResult CreateServicePackage()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateServicePackage(CreateServicePackageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _profitService.CreateServicePackage(model);
            if (result.isSuccess)
                _toastNotification.AddSuccessToastMessage(result.message, new ToastrOptions { Title = "" });

            return RedirectToAction(nameof(ServicesPackages));
        }


        public async Task<IActionResult> UpdateServicePackage(long id)
        {
            var result = await _profitService.ServicePackageDetails(id);
            if (result is null)
                return NotFound();

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateServicePackage(long id, UpdateServicePackageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _profitService.UpdateServicePackage(id, model);
            if (result.isSuccess)
                _toastNotification.AddSuccessToastMessage(result.message, new ToastrOptions { Title = "" });

            return RedirectToAction(nameof(ServicesPackages));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteServicePackage(long id)
        {
            var result = await _profitService.DeleteServicePackage(id);
            return Json(result);
        }
        #endregion
    }
}
