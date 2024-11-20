using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;

namespace Wasit.API.Controllers.ConsumerEstates
{
    [ApiExplorerSettings(GroupName = "ConsumerEstates")]
    public class ConsumerRentEstatesController : BaseController
    {
        private readonly IConsumerRentEstatesService _consumerRentEstatesService;


        public ConsumerRentEstatesController(IConsumerRentEstatesService consumerRentEstatesService)
        {
            _consumerRentEstatesService = consumerRentEstatesService;
        }


        [HttpGet("GetRentEstateInfo")]
        public async Task<IActionResult> GetRentEstateInfo([Required] long rentEstateId)
        {
            return _OK(await _consumerRentEstatesService.RentEstateInfo(rentEstateId));
        }


        [HttpPost("AddRentRequest")]
        public async Task<IActionResult> AddRentRequest([Required] long estateId, [Required] DateTime startDate, [Required] int monthCount)
        {
            return _OK(await _consumerRentEstatesService.AddRentRequest(UserId, estateId, startDate, monthCount));
        }
    }
}
