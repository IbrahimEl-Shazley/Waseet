using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;

namespace Wasit.API.Controllers.ConsumerEstates
{
    [ApiExplorerSettings(GroupName = "ConsumerEstates")]
    public class ConsumerFavouriteEstatesController : BaseController
    {
        private readonly IConsumerFavouriteEstatesService _consumerFavouriteEstatesService;

        public ConsumerFavouriteEstatesController(IConsumerFavouriteEstatesService consumerFavouriteEstatesService)
        {
            _consumerFavouriteEstatesService = consumerFavouriteEstatesService;
        }


        [HttpGet("ListFavouriteEstates")]
        public async Task<IActionResult> ListFavouriteEstates([Required] CategoryType category, long? estateTypeId, [Required] int pageNumber = 1)
        {
            return _OK(await _consumerFavouriteEstatesService.ListFavouriteEstates(UserId, category, estateTypeId, pageNumber));
        }
    }
}
