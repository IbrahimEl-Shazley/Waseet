using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Wasit.Core.Enums;
using Wasit.Services.Interfaces.Dashboard;

namespace Wasit.Dashboard.Controllers
{
    public class PropertyManagementController : Controller
    {
        private readonly IDPropertyManagementService _propertyManagementService;
        private readonly IToastNotification _toastNotification;

        public PropertyManagementController(IDPropertyManagementService propertyManagementService, IToastNotification toastNotification)
        {
            _propertyManagementService = propertyManagementService;
            _toastNotification = toastNotification;
        }



        #region Rent Management
        public async Task<IActionResult> RentManagement()
        {
            var result = await _propertyManagementService.RentManagementRequests();
            return View(result);
        }


        [HttpPut]
        public async Task<IActionResult> AcceptRentManagementRequest(long id)
        {
            var result = await _propertyManagementService.AcceptRentManagementRequest(id);
            return Json(result);
        }


        [HttpDelete]
        public async Task<IActionResult> RejectRentManagementRequest(long id)
        {
            var result = await _propertyManagementService.RejectRentManagementRequest(id);
            return Json(result);
        }


        public async Task<IActionResult> RentManagementRequestDetails(long id)
        {
            var result = await _propertyManagementService.RentManagementRequestDetails(id);

            if (result is null)
                return NotFound();

            return View(result);
        }
        #endregion



        #region Maintenance Management
        public async Task<IActionResult> MaintenanceManagement(RentalManagementOrderStatus status = RentalManagementOrderStatus.New)
        {
            var data = await _propertyManagementService.MaintainanceManagementRequests(status);
            return View(data);
        }
        #endregion

    }
}
