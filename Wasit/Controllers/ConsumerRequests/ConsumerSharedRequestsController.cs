using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using Wasit.Services.Interfaces.Generic.ConsumerRequests;

namespace Wasit.API.Controllers.ConsumerRequests
{
    [ApiExplorerSettings(GroupName = "ConsumerRequests")]
    public class ConsumerSharedRequestsController : BaseController
    {
        private readonly Func<CategoryType, IConsumerSharedRequestsService> _consumerSharedRequestsService;

        public ConsumerSharedRequestsController(Func<CategoryType, IConsumerSharedRequestsService> consumerSharedRequestsService)
        {
            _consumerSharedRequestsService = consumerSharedRequestsService;
        }


        [HttpPost("RateOwnerAndEstate")]
        public async Task<IActionResult> RateOwnerAndEstate([FromQuery] RateOwnerAndEstatePayload payload)
        {
            var service = _consumerSharedRequestsService(payload.Category);
            return _OK(await service.RateOwnerAndEstate(UserId, payload));
        }


        [HttpPost("RateReviewer")]
        public async Task<IActionResult> RateReviewer([Required] CategoryType category, [Required] long requestId, [Required] double rating)
        {
            var service = _consumerSharedRequestsService(category);
            return _OK(await service.RateReviewer(UserId, requestId, rating));
        }
    }
}
