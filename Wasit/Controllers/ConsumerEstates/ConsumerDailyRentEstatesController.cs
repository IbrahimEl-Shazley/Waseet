using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;

namespace Wasit.API.Controllers.ConsumerEstates
{
    [ApiExplorerSettings(GroupName = "ConsumerEstates")]
    public class ConsumerDailyRentEstatesController : BaseController
    {
        private readonly IConsumerDailyRentEstatesService _consumerDailyRentEstatesService;

        public ConsumerDailyRentEstatesController(IConsumerDailyRentEstatesService consumerDailyRentEstatesService)
        {
            _consumerDailyRentEstatesService = consumerDailyRentEstatesService;
        }


        [HttpGet("GetDailyRentEstateInfo")]
        public async Task<IActionResult> GetDailyRentEstateInfo([Required] long estateId)
        {
            return _OK(await _consumerDailyRentEstatesService.DailyRentEstateInfo(estateId));
        }


        [HttpPost("AddDailyRentRequest")]
        public async Task<IActionResult> AddDailyRentRequest([Required] long estateId, [Required] DateTime startDate, [Required] DateTime endDate, [Required] TypePay paymentMethod) // To-Do Payment
        {
            return _OK(await _consumerDailyRentEstatesService.AddDailyRentRequest(UserId, estateId, startDate, endDate, paymentMethod));
        }
    }
}
