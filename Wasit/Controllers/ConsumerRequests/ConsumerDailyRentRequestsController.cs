using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Models;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using Wasit.Services.Interfaces.Generic.ConsumerRequests;
using ListDailyRentRequestsPayload = Wasit.Services.DTOs.Schema.Shared.ConsumerRequests.ListDailyRentRequestsPayload;

namespace Wasit.API.Controllers.ConsumerRequests
{
    [ApiExplorerSettings(GroupName = "ConsumerRequests")]
    public class ConsumerDailyRentRequestsController : BaseController
    {
        private readonly IConsumerDailyRentRequestsService _consumerDailyRentRequestsService;

        public ConsumerDailyRentRequestsController(IConsumerDailyRentRequestsService consumerDailyRentRequestsService)
        {
            _consumerDailyRentRequestsService = consumerDailyRentRequestsService;
        }

        [HttpGet("ListRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<ConsumerDailyRentCategoryRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListRequests([FromQuery] ListDailyRentRequestsPayload model)
        {
            return _OK(await _consumerDailyRentRequestsService.ListRequests(UserId, model));
        }


        [HttpGet("RequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<ExtendedConsumerDailyRentCategoryRequestDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RequestInfo([Required] long requestId)
        {
            return _OK(await _consumerDailyRentRequestsService.RequestInfo(UserId, requestId));
        }


        [HttpDelete("CancelDailyRentRequest")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> CancelDailyRentRequest([Required] long requestId)
        {
            return _OK(await _consumerDailyRentRequestsService.CancelDailyRentRequest(UserId, requestId));
        }
    }
}
