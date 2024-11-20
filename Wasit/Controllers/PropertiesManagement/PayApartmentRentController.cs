using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Services.Interfaces.Generic.PropertiesManagement;

namespace Wasit.API.Controllers.PropertiesManagement
{
    [ApiExplorerSettings(GroupName = "PropertiesManagement")]
    public class PayApartmentRentController : BaseController
    {
        private readonly IPayApartmentRentService _payApartmentRentService;

        public PayApartmentRentController(IPayApartmentRentService payApartmentRentService)
        {
            _payApartmentRentService = payApartmentRentService;
        }


        [HttpGet("GetApartmentInfo")]
        public async Task<IActionResult> GetApartmentInfo([Required] int apartmentNo)
        {
            return _OK(await _payApartmentRentService.GetApartmentInfo(apartmentNo));
        }
        
        
        [HttpPost("PayRent")]
        public async Task<IActionResult> PayRent([Required] int apartmentId, [Required] TypePay paymentMethod) // To-do: Payment
        {
            return _OK(await _payApartmentRentService.PayRent(UserId, apartmentId, paymentMethod), "PaidSuccessfully");
        }
    }
}
