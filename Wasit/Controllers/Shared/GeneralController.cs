using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wasit.Controllers;
using Wasit.Core.Helpers.IO;
using Wasit.Core.Models.IO;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.API.Controllers.Shared
{
    [ApiExplorerSettings(GroupName = "Shared")]
    public class GeneralController : BaseController
    {
        private readonly IGeneralService _generalService;

        public GeneralController(IGeneralService generalService)
        {
            _generalService = generalService;
        }


        [AllowAnonymous]
        [HttpGet("TermsAndConditions")]
        public async Task<IActionResult> TermsAndConditions(string userType)
        {
            return _OK(await _generalService.TermsAndConditions(Language, userType), null);
        }
        
        
        [AllowAnonymous]
        [HttpGet("AboutUs")]
        public async Task<IActionResult> AboutUs()
        {
            return _OK(await _generalService.AboutUs(Language), null);
        }
        
        
        [AllowAnonymous]
        [HttpPost("UploadImage")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult UploadImage([FromForm] UploadImageUsingApiDto model)
        {
            var result = IOHelper.Upload(model.image, model.fileName);
            return _OK(result, "");
        }
    }   
}
