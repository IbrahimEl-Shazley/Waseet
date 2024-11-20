using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Models;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentRentRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using Wasit.Services.Interfaces.Generic.ConsumerRequests;

namespace Wasit.API.Controllers.ConsumerRequests
{
    [ApiExplorerSettings(GroupName = "ConsumerRequests")]
    public class ConsumerEntertainmentRentRequestsController : BaseController
    {
        private readonly IConsumerEntertainmentRequestsService _consumerEntertainmentRequestsService;

        public ConsumerEntertainmentRentRequestsController(IConsumerEntertainmentRequestsService consumerEntertainmentRequestsService)
        {
            _consumerEntertainmentRequestsService = consumerEntertainmentRequestsService;
        }


        [HttpGet("ListRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<ConsumerEntertainmentCategoryRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListRequests([FromQuery] ListEntertainmentRequestsPayload model)
        {
            return _OK(await _consumerEntertainmentRequestsService.ListRequests(UserId, model));
        }
        

        [HttpGet("RequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<ExtendedConsumerEntertainmentCategoryRequestDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RequestInfo([Required] long requestId)
        {
            return _OK(await _consumerEntertainmentRequestsService.RequestInfo(UserId, requestId));
        }
        
        
        [HttpDelete("CancelEntertainmentRequest")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> CancelEntertainmentRequest([Required] long requestId)
        {
            return _OK(await _consumerEntertainmentRequestsService.CancelEntertainmentRequest(UserId, requestId));
        }


    }
}
