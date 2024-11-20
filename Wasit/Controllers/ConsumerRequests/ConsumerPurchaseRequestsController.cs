using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Core.Models;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Sale.PurchaseRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using Wasit.Services.Interfaces.Generic.ConsumerRequests;

namespace Wasit.API.Controllers.ConsumerRequests
{
    [ApiExplorerSettings(GroupName = "ConsumerRequests")]
    public class ConsumerPurchaseRequestsController : BaseController
    {
        private readonly IConsumerPurchaseRequestsService _consumerPurchaseRequestsService;

        public ConsumerPurchaseRequestsController(IConsumerPurchaseRequestsService consumerPurchaseRequestsService)
        {
            _consumerPurchaseRequestsService = consumerPurchaseRequestsService;
        }


        [HttpGet("ListRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<ConsumerPurchaseCategoryRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListRequests([FromQuery] ListSaleRequestsPayload model)
        {
            return _OK(await _consumerPurchaseRequestsService.ListRequests(UserId, model));
        }
        
        
        [HttpGet("PurchaseRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<ExtendedConsumerPurchaseRequestInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> PurchaseRequestInfo([Required] long requestId)
        {
            return _OK(await _consumerPurchaseRequestsService.PurchaseRequestInfo(UserId, requestId));
        }


        [HttpGet("RatingRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<ExtendedConsumerRatingRequestInfoForSaleEstateDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RatingRequestInfo([Required] long requestId)
        {
            return _OK(await _consumerPurchaseRequestsService.RatingRequestInfo(UserId, requestId));
        }


        [HttpGet("ReservationRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<ExtendedConsumerReservationRequestInfoForSaleEstateDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ReservationRequestInfo([Required] long requestId)
        {
            return _OK(await _consumerPurchaseRequestsService.ReservationRequestInfo(UserId, requestId));
        }


        [HttpDelete("CancelPurchaseRequest")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> CancelPurchaseRequest([Required] long requestId)
        {
            return _OK(await _consumerPurchaseRequestsService.CancelPurchaseRequest(UserId, requestId));
        }
        
        
        [HttpPost("PayDeposit")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> PayDeposit([Required] long requestId, [Required] TypePay paymentMethod)
        {
            return _OK(await _consumerPurchaseRequestsService.PayDeposit(UserId, requestId, paymentMethod));
        }
        
        
        [HttpPatch("ConfirmRecievingSaleEstate")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ConfirmRecievingSaleEstate([Required] long requestId)
        {
            return _OK(await _consumerPurchaseRequestsService.ConfirmRecievingSaleEstate(UserId, requestId));
        }
        
        
        [HttpPost("AddRefundRequest")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> AddRefundRequest([Required] long requestId, [Required] string reason)
        {
            return _OK(await _consumerPurchaseRequestsService.AddRefundRequest(UserId, requestId, reason));
        }

    }
}
