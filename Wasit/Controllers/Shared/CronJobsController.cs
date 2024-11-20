using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wasit.Controllers;
using Wasit.Services.Interfaces.Generic.MyEstates;

namespace Wasit.API.Controllers.Shared
{
    [ApiExplorerSettings(GroupName = "Shared")]
    public class CronJobsController : BaseController
    {
       private readonly IGeneralMySharedEstatesService _generalMySharedEstatesService;

        public CronJobsController(IGeneralMySharedEstatesService generalMySharedEstatesService)
        {
            _generalMySharedEstatesService = generalMySharedEstatesService;
        }


        [AllowAnonymous]
        [HttpGet("CheckReservationRequests")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> CheckReservationRequests()
        {
            return _OK(await _generalMySharedEstatesService.CheckReservationRequests());
        }


        [AllowAnonymous]
        [HttpGet("CheckShortTermRentRequests")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> CheckShortTermRentRequests()
        {
            return _OK(await _generalMySharedEstatesService.CheckShortTermRentRequests());
        }
        
        
        [AllowAnonymous]
        [HttpGet("CheckApartmentsRentStatus")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> CheckApartmentsRentStatus()
        {
            return _OK(await _generalMySharedEstatesService.CheckApartmentsRentStatus());
        }
        
        
        [AllowAnonymous]
        [HttpGet("CheckUnAcceptedEvaluationRequests")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> CheckUnAcceptedEvaluationRequests()
        {
            return _OK(await _generalMySharedEstatesService.CheckUnAcceptedEvaluationRequests());
        }


        [AllowAnonymous]
        [HttpGet("CheckLongTermRentRequests")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> CheckLongTermRentRequests()
        {
            return _OK(await _generalMySharedEstatesService.CheckLongTermRentRequests());
        }



        [AllowAnonymous]
        [HttpGet("CheckAllowedBookingPeriodForShortRentEstates")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> CheckAllowedBookingPeriodForShortRentEstates()
        {
            return _OK(await _generalMySharedEstatesService.CheckAllowedBookingPeriodForShortRentEstates());
        }
    }
}
