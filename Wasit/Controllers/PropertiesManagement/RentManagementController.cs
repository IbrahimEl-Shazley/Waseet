using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.PropertiesManagement.RentManagement;
using Wasit.Services.Interfaces.Generic.PropertiesManagement;

namespace Wasit.API.Controllers.PropertiesManagement
{
    [ApiExplorerSettings(GroupName = "PropertiesManagement")]
    public class RentManagementController : BaseController
    {
        private readonly IRentManagementService _rentManagementService;

        public RentManagementController(IRentManagementService rentManagementService)
        {
            _rentManagementService = rentManagementService;
        }


        [HttpPost("AddRentalManagementOrder")]
        public async Task<IActionResult> AddRentalManagementOrder([FromForm] AddRentalMangementOrderPayload payload)
        {
            return _OK(await _rentManagementService.AddRentalManagementOrder(payload, UserId));
        }
        
        
        [HttpPut("EditRentalManagementOrder")]
        public async Task<IActionResult> EditRentalManagementOrder([Required] long orderId, [FromForm] EditRentalMangementOrderPayload payload)
        {
            return _OK(await _rentManagementService.EditRentalManagementOrder(orderId, payload));
        }
        
        
        [HttpGet("ListMyManagedRentEstates")]
        public async Task<IActionResult> ListMyManagedRentEstates([Required] int pageNumber, [Required] RentalManagementOrderStatus status)
        {
            return _OK(await _rentManagementService.ListMyManagedRentEstates(UserId, pageNumber, status));
        }


        [HttpGet("ManagedRentEstateInfo")]
        public async Task<IActionResult> ManagedRentEstateInfo([Required] long orderId)
        {
            return _OK(await _rentManagementService.ManagedRentEstateInfo(UserId, orderId));
        }
        
        
        [HttpPatch("CancelRentManagementOrder")]
        public async Task<IActionResult> CancelRentManagementOrder([Required] long orderId)
        {
            return _OK(await _rentManagementService.CancelRentManagementOrder(UserId, orderId));
        }
        
        
        [HttpDelete("CancelContractForRentManagement")]
        public async Task<IActionResult> CancelContractForRentManagement([Required] long orderId)
        {
            return _OK(await _rentManagementService.CancelContractForRentManagement(UserId, orderId));
        }
    }
}
