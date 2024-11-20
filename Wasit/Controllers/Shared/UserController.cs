using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.SEC;
using Wasit.Services.DTOs.Schema.SEC.Broker;
using Wasit.Services.DTOs.Schema.SEC.Delegate;
using Wasit.Services.DTOs.Schema.SEC.Developer;
using Wasit.Services.DTOs.Schema.SEC.Owner;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Users;
using Wasit.Services.Interfaces.General;

namespace Wasit.API.Controllers.Shared
{
    [ApiExplorerSettings(GroupName = "Shared")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPatch("ChangeNotify")]
        public async Task<IActionResult> ChangeNotify()
        {
            return _OK(await _userService.ChangeNotify(UserId));
        }


        [HttpPut("UpdateOwnerProfile")]
        public async Task<IActionResult> UpdateOwnerProfile([FromForm] OwnerUpdateDto input)
        {
            return _OK(await _userService.UpdateOwnerProfile(UserId, input));
        }


        [HttpPut("UpdateBrokerProfile")]
        public async Task<IActionResult> UpdateBrokerProfile([Required] AccountType accountType, [FromForm] BrokerUpdateDto input)
        {
            return _OK(await _userService.UpdateBrokerProfile(UserId, accountType, input));
        }


        [HttpPut("UpdateDelegateProfile")]
        public async Task<IActionResult> UpdateDelegateProfile([FromForm] DelegateUpdateDto input)
        {
            return _OK(await _userService.UpdateDelegateProfile(UserId, input));
        }


        [HttpPut("UpdateDeveloperProfile")]
        public async Task<IActionResult> UpdateDeveloperProfile([FromForm] DeveloperUpdateDto input)
        {
            return _OK(await _userService.UpdateDeveloperProfile(UserId, input));
        }


        [HttpPost("ConfirmCurrentPhone")]
        public async Task<IActionResult> ConfirmCurrentPhone([FromQuery] ConfirmCurrentPhoneDto input)
        {
            return _OK(await _userService.ConfirmCurrentPhone(UserId, input), "UserVerifiedSuccessfully");
        }


        [HttpPost("ValidataOTP")]
        public async Task<IActionResult> ValidataOTP([FromBody] UserVerifyDTO input)
        {
            return _OK(await _userService.ValidataOTP(UserId, input));
        }


        [HttpPost("ValidateNewPhone")]
        public async Task<IActionResult> ValidateNewPhone([FromQuery] ValidateNewPhoneDto input)
        {
            return _OK(new
            {
                NewPhone = await _userService.ValidateNewPhone(UserId, input)
            }, "NotificationSendSuccesfullyToVerificationMethod");
        }


        [HttpPost("ConfirmNewPhoneOTP")]
        public async Task<IActionResult> ConfirmNewPhoneOTP([FromBody] ConfirmNewPhoneOTPDto input)
        {
            return _OK(await _userService.ConfirmNewPhoneOTP(UserId, input), "ItemUpdatedSuccessfully");
        }
        
        
        [HttpPatch("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto input)
        {
            return _OK(await _userService.ChangePassword(UserId, input), "ItemUpdatedSuccessfully");
        }


    }
}
