using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;

namespace Wasit.API.Controllers.ConsumerEstates
{
    [ApiExplorerSettings(GroupName = "ConsumerEstates")]
    public class ConsumerSharedEstatesController : BaseController
    {
        private readonly Func<CategoryType, IConsumerSharedEstatesService> _ConsumerSharedEstatesService;
        private readonly IConsumerGeneralSharedService _consumerGeneralSharedService;

        public ConsumerSharedEstatesController(Func<CategoryType, IConsumerSharedEstatesService> consumerSharedEstatesService, IConsumerGeneralSharedService consumerGeneralSharedService)
        {
            _ConsumerSharedEstatesService = consumerSharedEstatesService;
            _consumerGeneralSharedService = consumerGeneralSharedService;
        }


        [HttpGet("ListEstates")]
        public async Task<IActionResult> ListEstates([FromQuery] ListEstatesPayload payload)
        {
            var service = _ConsumerSharedEstatesService(payload.Category);
            return _OK(await service.ListEstates(UserId, payload));
        }


        [HttpPost("AddOrRemoveFavouriteEstate")]
        public async Task<IActionResult> AddOrRemoveFavouriteEstate([Required] CategoryType category, [Required] long estateId)
        {
            var service = _ConsumerSharedEstatesService(category);
            return _OK(await service.AddOrRemoveFavouriteEstate(UserId, estateId));
        }


        [HttpPost("AssignDelegate")] // To-Do Payment
        public async Task<IActionResult> AssignDelegate([Required] CategoryType category, [Required] long estateId, [Required] TypePay paymentMethod)
        {
            var service = _ConsumerSharedEstatesService(category);
            return _OK(await service.AssignDelegate(estateId, paymentMethod, UserId));
        }    
        
        
        [HttpPost("AddReservationRequest")] // To-Do Payment
        public async Task<IActionResult> AddReservationRequest([Required] CategoryType category, [Required] long estateId, [Required] TypePay paymentMethod)
        {
            var service = _ConsumerSharedEstatesService(category);
            return _OK(await service.AddReservationRequest(Language, estateId, paymentMethod, UserId));
        }     
        
        
        [HttpGet("AllUserRatingsOnEstate")]
        public async Task<IActionResult> AllUserRatingsOnEstate([Required] CategoryType category, [Required] long estateId, [Required] int pageNumber = 1)
        {
            var service = _ConsumerSharedEstatesService(category);
            return _OK(await service.AllUserRatingsOnEstate(estateId, pageNumber));
        }
        
        
        [HttpPost("ReportComment")]
        public async Task<IActionResult> ReportComment([Required] CategoryType category, [Required] long requestId, [Required] string reason)
        {
            var service = _ConsumerSharedEstatesService(category);
            return _OK(await service.ReportComment(UserId, requestId, reason));
        }


        [HttpGet("ReservedDays")]
        public async Task<IActionResult> ReservedDays([Required] CategoryType category, [Required] long estateId)
        {
            var service = _ConsumerSharedEstatesService(category);
            return _OK(await service.ReservedDays(estateId));
        }


        /// <summary>
        /// Not Implemented Yet
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet("PublisherInvestmentOpportunities")]
        public async Task<IActionResult> PublisherInvestmentOpportunities([Required] string publisherId, [Required] int pageNumber = 1)
        {
            return _OK(await _consumerGeneralSharedService.PublisherInvestmentOpportunities(publisherId, pageNumber));
        }
        

        /// <summary>
        /// Not Implemented Yet
        /// </summary>
        /// <param name="publisherId"></param>
        /// <returns></returns>
        [HttpGet("GePublisherInfo")]
        public async Task<IActionResult> GePublisherInfo([Required] string publisherId)
        {
            return _OK(await _consumerGeneralSharedService.GePublisherInfo(publisherId));
        }
    }
}
