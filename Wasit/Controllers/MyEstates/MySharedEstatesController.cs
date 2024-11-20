using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Core.Models;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.EstateCategories.EstateType;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.Interfaces.Generic.MyEstates;

namespace Wasit.API.Controllers.MyEstates
{
    [ApiExplorerSettings(GroupName = "MyEstates")]
    public class MySharedEstatesController : BaseController
    {
        private readonly Func<CategoryType, IMySharedEstatesService> _mySharedEstatesService;
        private readonly IGeneralMySharedEstatesService _generalMySharedEstatesService;

        public MySharedEstatesController(Func<CategoryType, IMySharedEstatesService> mySharedEstatesService, IGeneralMySharedEstatesService generalMySharedEstatesService)
        {
            _mySharedEstatesService = mySharedEstatesService;
            _generalMySharedEstatesService = generalMySharedEstatesService;
        }


        [HttpGet("ConfirmEstateDeedNumber")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ConfirmEstateDeedNumber([Required] CategoryType category, string estateDeedNumber)
        {
            var service = _mySharedEstatesService(category);
            return _OK(await service.ConfirmEstateDeedNumber(estateDeedNumber));
        }


        [HttpGet("GetEstateTypes")]
        [ProducesResponseType(typeof(GlobalResponse<List<EstateTypeDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> GetEstateTypes([Required] CategoryType category)
        {
            var service = _mySharedEstatesService(category);
            return _OK(await service.GetEstateTypes());
        }
        
        
        [HttpGet("ListMyEstates")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<BaseEstateDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListMyEstates([Required] CategoryType category, [Required] int pageNumber = 1)
        {
            var service = _mySharedEstatesService(category);
            return _OK(await service.ListMyEstates(UserId, pageNumber));
        }


        [HttpGet("GetSpecsForm")]
        [ProducesResponseType(typeof(GlobalResponse<IEnumerable<SpecificationFormItemDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> GetSpecsForm([Required] long estateTypeId)
        {
            var result = await _generalMySharedEstatesService.GetSpecsForm(estateTypeId);
            return _OK(result);
        }
        
        
        [HttpPatch("ChangeEstateVisibility")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ChangeEstateVisibility([Required] CategoryType category, [Required] long estateId)
        {
            var service = _mySharedEstatesService(category);
            return _OK(await service.ChangeEstateVisibility(estateId));
        }


        [HttpDelete("RemoveEstate")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RemoveEstate([Required] CategoryType category, [Required] long estateId)
        {
            var service = _mySharedEstatesService(category);
            return _OK(await service.RemoveEstate(estateId));
        }


        [HttpPost("ReportComment")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ReportComment([Required] CategoryType category, [Required] long ratingId, [Required] string reason)
        {
            var service = _mySharedEstatesService(category);
            return _OK(await service.ReportComment(UserId, ratingId, reason));
        }
        
        
        [HttpGet("ListEstateRatings")]
        [ProducesResponseType(typeof(GlobalResponse<PageDTO<BaseRatingItemDto>>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ListEstateRatings([Required] CategoryType category, [Required] long estateId, [Required] int pageNumber = 1)
        {
            var service = _mySharedEstatesService(category);
            return _OK(await service.ListEstateRatings(UserId, estateId, pageNumber));
        }


        [HttpPost("SelectReservationPeriod")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> SelectReservationPeriod([Required] CategoryType category, [Required] long estateId, [Required] DateTime startDate, [Required] DateTime endDate)
        {
            var service = _mySharedEstatesService(category);
            return _OK(await service.SelectReservationPeriod(UserId, estateId, startDate, endDate));
        }

        [HttpPost("RateRentee")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> RateRentee([Required] CategoryType category, [Required] long requestId, [Required] double rating)
        {
            var service = _mySharedEstatesService(category);
            return _OK(await service.RateRentee(UserId, requestId, rating));
        }

        /////////////////////////////////////////////// Not Finished Yet ///////////////////////////////////////////////

        [HttpPost("DownloadFinancialAccounts")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> DownloadFinancialAccounts([Required] CategoryType category, [Required] long estateId, [Required] DateTime startDate, [Required] DateTime endDate)
        {
            var service = _mySharedEstatesService(category);
            return _OK(await service.DownloadFinancialAccounts(UserId, estateId, startDate, endDate));
        }
    }
}
