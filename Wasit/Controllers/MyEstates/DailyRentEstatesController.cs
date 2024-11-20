using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Models;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentRequest;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.Interfaces.Generic.MyEstates;

namespace Wasit.API.Controllers.MyEstates
{
    [ApiExplorerSettings(GroupName = "MyEstates")]
    public class DailyRentEstatesController : BaseController
    {
        private readonly IDailyRentEstatesService _dailyRentEstatesService;

        public DailyRentEstatesController(IDailyRentEstatesService dailyRentEstatesService)
        {
            _dailyRentEstatesService = dailyRentEstatesService;
        }


        [HttpPost("AddNewDailyRentEstate")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> AddNewDailyRentEstate([FromForm] CreateDailyRentEstateDto model)
        {
            return _OK(await _dailyRentEstatesService.AddNewDailyRentEstate(UserId, model));
        }


        [HttpGet("MyDailyRentEstateInfo")]
        [ProducesResponseType(typeof(GlobalResponse<DailyRentEstateInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> MyDailyRentEstateInfo([Required] long estateId)
        {
            return _OK(await _dailyRentEstatesService.MyDailyRentEstateInfo(UserId, estateId));
        }


        [HttpPut("EditDailyRentEstate")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> EditDailyRentEstate([FromForm] UpdateDailyRentEstateDto model)
        {
            return _OK(await _dailyRentEstatesService.EditDailyRentEstate(UserId, model));
        }


        [HttpGet("GetSpecificationValues")]
        [ProducesResponseType(typeof(GlobalResponse<List<SpecificationValueDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> GetSpecificationValues([Required] long estateId)
        {
            return _OK(await _dailyRentEstatesService.GetSpecificationValues(UserId, estateId));
        }

        [HttpGet("ListDailyRentRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<DailyRentRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListDailyRentRequests([FromQuery] ListDailyRentRequestsPayload model)
        {
            return _OK(await _dailyRentEstatesService.ListDailyRentRequests(model));
        }


        [HttpGet("DailyRentRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<DailyRentRequestInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> DailyRentRequestInfo([Required] long requestId)
        {
            return _OK(await _dailyRentEstatesService.DailyRentRequestInfo(requestId));
        }
    }
}
