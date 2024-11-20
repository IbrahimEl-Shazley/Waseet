using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Models;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentEstate;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentRentRequest;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.Interfaces.Generic.MyEstates;

namespace Wasit.API.Controllers.MyEstates
{
    [ApiExplorerSettings(GroupName = "MyEstates")]
    public class EntertainmentEstatesController : BaseController
    {
        private readonly IEntertainmentEstatesService _entertainmentEstatesService;

        public EntertainmentEstatesController(IEntertainmentEstatesService entertainmentEstatesService)
        {
            _entertainmentEstatesService = entertainmentEstatesService;
        }


        [HttpPost("AddNewEntertainmentEstate")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> AddNewEntertainmentEstate([FromForm]CreateEntertainmentEstateDto model)
        {
            return _OK(await _entertainmentEstatesService.AddNewEntertainmentEstate(UserId, model));
        }


        [HttpGet("MyEntertainmentEstateInfo")]
        [ProducesResponseType(typeof(GlobalResponse<EntertainmentEstateInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> MyEntertainmentEstateInfo([Required] long estateId)
        {
            return _OK(await _entertainmentEstatesService.MyEntertainmentEstateInfo(UserId, estateId));
        }


        [HttpPut("EditEntertainmentEstate")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> EditEntertainmentEstate([FromForm] UpdateEntertainmentEstateDto model)
        {
            return _OK(await _entertainmentEstatesService.EditEntertainmentEstate(UserId, model));
        }


        [HttpGet("GetSpecificationValues")]
        [ProducesResponseType(typeof(GlobalResponse<List<SpecificationValueDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> GetSpecificationValues([Required] long estateId)
        {
            return _OK(await _entertainmentEstatesService.GetSpecificationValues(UserId, estateId));
        }

        [HttpGet("ListEntertainmentRentRequests")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<EntertainmentRentRequestDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListEntertainmentRentRequests([FromQuery] ListEntertainmentRentRequestPayload model)
        {
            return _OK(await _entertainmentEstatesService.ListEntertainmentRentRequests(model));
        }


        [HttpGet("EntertainmentRentRequestInfo")]
        [ProducesResponseType(typeof(GlobalResponse<EntertainmentRentRequestInfoDto>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> EntertainmentRentRequestInfo([Required] long requestId)
        {
            return _OK(await _entertainmentEstatesService.EntertainmentRentRequestInfo(requestId));
        }
    }
}
