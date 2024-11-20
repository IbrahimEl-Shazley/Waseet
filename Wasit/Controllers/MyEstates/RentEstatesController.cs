using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Models;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;
using Wasit.Services.DTOs.Schema.Rent.RentRatingRequest;
using Wasit.Services.DTOs.Schema.Rent.RentRequest;
using Wasit.Services.DTOs.Schema.Rent.RentReservationRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.Interfaces.Generic.MyEstates;

namespace Wasit.API.Controllers.MyEstates
{
    [ApiExplorerSettings(GroupName = "MyEstates")]
    public class RentEstatesController : BaseController
    {
        private readonly IRentEstatesService _rentEstatesService;

        public RentEstatesController(IRentEstatesService rentEstatesService)
        {
            _rentEstatesService = rentEstatesService;
        }

        [HttpPost("AddNewRentEstate")]
        [ProducesResponseType(typeof(GlobalResponse<long>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> AddNewRentEstate([FromForm] CreateRentEstateDto model)
        {
            return _OK(await _rentEstatesService.AddNewRentEstate(UserId, model));
        }


        [HttpGet("MyRentEstateInfo")]
        [ProducesResponseType(typeof(GlobalResponse<RentEstateDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> MyRentEstateInfo([Required] long estateId)
        {
            return _OK(await _rentEstatesService.MyRentEstateInfo(UserId, estateId));
        }


        [HttpPut("EditRentEstate")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> EditRentEstate([FromForm] UpdateRentEstateDto model)
        {
            return _OK(await _rentEstatesService.EditRentEstate(UserId, model));
        }


        [HttpGet("GetSpecificationValues")]
        [ProducesResponseType(typeof(GlobalResponse<List<SpecificationValueDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> GetSpecificationValues([Required] long estateId)
        {
            return _OK(await _rentEstatesService.GetSpecificationValues(UserId, estateId));
        }


        [HttpGet("ListRentReservationRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<RentReservationRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListRentReservationRequests([FromQuery] ListReservationRequestsPayload model)
        {
            return _OK(await _rentEstatesService.ListRentReservationRequests(model));
        }


        [HttpGet("ReservationRentRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<RentReservationRequestInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ReservationRentRequestInfo([Required] long requestId)
        {
            return _OK(await _rentEstatesService.ReservationRentRequestInfo(requestId));
        }


        [HttpGet("ListRentRatingRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<RentRatingRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListRentRatingRequests([FromQuery] ListRatingRequestsPayload model)
        {
            return _OK(await _rentEstatesService.ListRentRatingRequests(model));
        }


        [HttpGet("RentRatingRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<RentRatingRequestInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RentRatingRequestInfo([Required] long requestId)
        {
            return _OK(await _rentEstatesService.RentRatingRequestInfo(requestId));
        }


        [HttpGet("ListRentRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<RentRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListRentRequests([FromQuery] ListRentRequestsPayload model)
        {
            return _OK(await _rentEstatesService.ListRentRequests(model));
        }


        [HttpGet("RentRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<RentRequestInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RentRequestInfo([Required] long requestId)
        {
            return _OK(await _rentEstatesService.RentRequestInfo(requestId));
        }


        [HttpPatch("AcceptRentRequest")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> AcceptRentRequest([Required] long requestId)
        {
            return _OK(await _rentEstatesService.AcceptRentRequest(requestId));
        }


        [HttpPatch("RejectRentRequest")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RejectRentRequest([Required] long requestId)
        {
            return _OK(await _rentEstatesService.RejectRentRequest(requestId));
        }


        [HttpPatch("ConfirmEstateIsRented")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ConfirmEstateIsRented([Required] long estateId, [Required] long requestId)
        {
            return _OK(await _rentEstatesService.ConfirmEstateRented(estateId, requestId));
        }

    }
}
