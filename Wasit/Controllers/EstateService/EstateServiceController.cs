using Microsoft.AspNetCore.Mvc;
using Wasit.Controllers;
using Wasit.Core.Entities.EstateServiceSection;
using Wasit.Core.Models;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.Interfaces.Generic.EstateServiceSection;

namespace Wasit.API.Controllers.StateService
{
    [ApiExplorerSettings(GroupName = "EstatesServices")]
    public class EstateServiceController : BaseController
    {
        private readonly IEstateServiceService _estateServiceService;

        public EstateServiceController(IEstateServiceService estateServiceService)
        {
            _estateServiceService = estateServiceService;
        }

        [HttpPost("ShowEstatesServicePackages")]
        [ProducesResponseType(typeof(GlobalResponse<bool>), 200)]
        [ProducesResponseType(typeof(GlobalResponse), 400)]
        public async Task<IActionResult> ShowEstatesServicePackages()
        {
            return _OK(await _estateServiceService.ServicesPackages());
        }

    }
}
