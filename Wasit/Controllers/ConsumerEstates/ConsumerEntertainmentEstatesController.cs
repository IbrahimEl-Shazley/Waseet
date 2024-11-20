using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;

namespace Wasit.API.Controllers.ConsumerEstates
{
    [ApiExplorerSettings(GroupName = "ConsumerEstates")]
    public class ConsumerEntertainmentEstatesController : BaseController
    {
        private readonly IConsumerEntertainmentEstatesService _consumerEntertainmentEstatesService;

        public ConsumerEntertainmentEstatesController(IConsumerEntertainmentEstatesService consumerEntertainmentEstatesService)
        {
            _consumerEntertainmentEstatesService = consumerEntertainmentEstatesService;
        }


        [HttpGet("GetEntertainmentEstateInfo")]
        public async Task<IActionResult> GetEntertainmentEstateInfo([Required] long estateId)
        {
            return _OK(await _consumerEntertainmentEstatesService.EntertainmentEstateInfo(estateId));
        }


        [HttpPost("AddEntertainmentRentRequest")]
        public async Task<IActionResult> AddEntertainmentRentRequest([Required] long estateId, [Required] DateTime startDate, [Required] DateTime endDate, [Required] TypePay paymentMethod) // To-Do Payment
        {
            return _OK(await _consumerEntertainmentEstatesService.AddEntertainmentRentRequest(UserId, estateId, startDate, endDate, paymentMethod));
        }
    }
}
