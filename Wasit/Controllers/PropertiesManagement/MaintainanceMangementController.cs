using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.PropertiesManagement.MaintainanceMangment;
using Wasit.Services.Interfaces.Generic.PropertiesManagement;

namespace Wasit.API.Controllers.PropertiesManagement
{
    [ApiExplorerSettings(GroupName = "PropertiesManagement")]

    public class MaintainanceMangementController : BaseController
    {
        private readonly IMaintainanceMangementService _maintainanceMangementService;

        public MaintainanceMangementController(IMaintainanceMangementService maintainanceMangementService)
        {
            _maintainanceMangementService = maintainanceMangementService;
        }


        [HttpPost("AddMaintainanceOrder")]
        public async Task<IActionResult> AddMaintainanceOrder([FromForm] AddMaintainanceOrderPayload payload) // To-do: Payment
        {
            return _OK(await _maintainanceMangementService.AddMaintainanceOrder(UserId, payload));
        }
        
        
        [HttpGet("ListMyManagedMaintainanceEstates")]
        public async Task<IActionResult> ListMyManagedMaintainanceEstates([Required] int pageNumber, [Required] RentalManagementOrderStatus status)
        {
            return _OK(await _maintainanceMangementService.ListMyManagedMaintainanceEstates(UserId, pageNumber, status));
        }
        
        
        [HttpPost("RateMaintainanceOrder")]
        public async Task<IActionResult> RateMaintainanceOrder([Required] long orderId, [Required] double rating)
        {
            return _OK(await _maintainanceMangementService.RateMaintainanceOrder(UserId, orderId, rating));
        }


    }
}
