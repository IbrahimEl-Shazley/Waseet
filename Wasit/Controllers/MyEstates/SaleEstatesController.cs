using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Core.Models;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Sale.PurchaseRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;
using Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.Interfaces.Generic.MyEstates;

namespace Wasit.API.Controllers.MyEstates
{
    [ApiExplorerSettings(GroupName = "MyEstates")]
    public class SaleEstatesController : BaseController
    {
        private readonly ISaleEstatesService _saleEstatesService;

        public SaleEstatesController(ISaleEstatesService saleEstatesService)
        {
            _saleEstatesService = saleEstatesService;
        }


        [HttpPost("AddNewSaleEstate")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> AddNewSaleEstate([FromForm] CreateSaleEstateDto model)
        {
            return _OK(await _saleEstatesService.AddNewSaleEstate(UserId, model));
        }


        [HttpGet("MySaleEstateInfo")]
        [ProducesResponseType(typeof(GlobalResponse<SaleEstateInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> MySaleEstateInfo([Required] long estateId)
        {
            return _OK(await _saleEstatesService.MySaleEstateInfo(UserId, estateId));
        }
        

        [HttpPut("EditSaleEstate")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> EditSaleEstate([FromForm] UpdateSaleEstateDto model)
        {
            return _OK(await _saleEstatesService.EditSaleEstate(UserId, model));
        }


        [HttpGet("GetSpecificationValues")]
        [ProducesResponseType(typeof(GlobalResponse<List<SpecificationValueDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> GetSpecificationValues([Required] long estateId)
        {
            return _OK(await _saleEstatesService.GetSpecificationValues(UserId, estateId));
        }


        [HttpGet("ListSaleReservationRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<SaleReservationRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListSaleReservationRequests([FromQuery] ListReservationRequestsPayload model)
        {
            return _OK(await _saleEstatesService.ListSaleReservationRequests(model));
        }


        [HttpGet("ReservationSaleRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<SaleReservationRequestInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ReservationSaleRequestInfo([Required] long requestId)
        {
            return _OK(await _saleEstatesService.ReservationSaleRequestInfo(requestId));
        }


        [HttpGet("ListSaleRatingRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<SaleRatingRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListSaleRatingRequests([FromQuery] ListRatingRequestsPayload model)
        {
            return _OK(await _saleEstatesService.ListSaleRatingRequests(model));
        }


        [HttpGet("SaleRatingRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<SaleRatingRequestInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> SaleRatingRequestInfo([Required] long requestId)
        {
            return _OK(await _saleEstatesService.SaleRatingRequestInfo(requestId));
        }


        [HttpGet("ListPurchaseRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<PurchaseRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListPurchaseRequests([FromQuery] ListPurchaseRequestsPayload model)
        {
            return _OK(await _saleEstatesService.ListPurchaseRequests(model));
        }
        
        
        [HttpGet("PurchaseRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<PurchaseRequestInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> PurchaseRequestInfo([Required] long requestId)
        {
            return _OK(await _saleEstatesService.PurchaseRequestInfo(requestId));
        }


        [HttpPatch("AcceptPurchaseRequest")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> AcceptPurchaseRequest([Required] long requestId)
        {
            return _OK(await _saleEstatesService.AcceptPurchaseRequest(requestId));
        }
        
        
        [HttpPatch("RejectPurchaseRequest")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RejectPurchaseRequest([Required] long requestId)
        {
            return _OK(await _saleEstatesService.RejectPurchaseRequest(requestId));
        }


        [HttpPatch("ConfirmEstateIsSold")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ConfirmEstateIsSold([Required] long estateId, [Required] double price, [Required] long requestId)
        {
            return _OK(await _saleEstatesService.ConfirmEstateIsSold(estateId, price, requestId));
        }
        
        
        [HttpPost("AddPricingRequest")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> AddPricingRequest([Required] long estateId, [Required] TypePay paymentMethod) // TODO: Payment
        {
            return _OK(await _saleEstatesService.AddPricingRequest(UserId, estateId, paymentMethod));
        }

    }
}
