using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Core.Models;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Rent.RentRatingRequest;
using Wasit.Services.DTOs.Schema.Rent.RentRequest;
using Wasit.Services.DTOs.Schema.Rent.RentReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using Wasit.Services.Interfaces.Generic.ConsumerRequests;
using ListRentRequestsPayload = Wasit.Services.DTOs.Schema.Shared.ConsumerRequests.ListRentRequestsPayload;

namespace Wasit.API.Controllers.ConsumerRequests
{
    [ApiExplorerSettings(GroupName = "ConsumerRequests")]
    public class ConsumerRentRequestsController : BaseController
    {
        private readonly IConsumerRentRequestsService _consumerRentRequestsService;

        public ConsumerRentRequestsController(IConsumerRentRequestsService consumerRentRequestsService)
        {
            _consumerRentRequestsService = consumerRentRequestsService;
        }


        [HttpGet("ListRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<ConsumerRentCategoryRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListRequests([FromQuery] ListRentRequestsPayload model)
        {
            return _OK(await _consumerRentRequestsService.ListRequests(UserId, model));
        }


        [HttpGet("RentRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<ExtendedConsumerRentRequestInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RentRequestInfo([Required] long requestId)
        {
            return _OK(await _consumerRentRequestsService.RentRequestInfo(UserId, requestId));
        }
        
        
        [HttpGet("RatingRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<ExtendedConsumerRatingRequestInfoForRentEstateDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RatingRequestInfo([Required] long requestId)
        {
            return _OK(await _consumerRentRequestsService.RatingRequestInfo(UserId, requestId));
        }
        
        
        [HttpGet("ReservationRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<ExtendedConsumerReservationRequestInfoForRentEstateDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ReservationRequestInfo([Required] long requestId)
        {
            return _OK(await _consumerRentRequestsService.ReservationRequestInfo(UserId, requestId));
        }


        [HttpDelete("CancelRentRequest")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> CancelRentRequest([Required] long requestId)
        {
            return _OK(await _consumerRentRequestsService.CancelRentRequest(UserId, requestId));
        }
        
        
        [HttpPatch("ConfirmRecievingRentEstate")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ConfirmRecievingRentEstate([Required] long requestId)
        {
            return _OK(await _consumerRentRequestsService.ConfirmRecievingRentEstate(UserId, requestId));
        }


        [HttpPost("PayForRent")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)] // To-Do Payment
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> PayForRent([Required] long requestId, [Required] TypePay paymentMethod)
        {
            return _OK(await _consumerRentRequestsService.PayForRent(UserId, requestId, paymentMethod));
        }
        
        
        [HttpPost("RateOwner")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RateOwner([Required] long requestId, [Required] double rating)
        {
            return _OK(await _consumerRentRequestsService.RateOwner(UserId, requestId, rating));
        }
    }
}
