using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Services.DTOs.Schema.SEC;
using Wasit.Services.Interfaces.General;

namespace Wasit.Controllers
{
    [ApiExplorerSettings(GroupName = "Auth")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("OwnerRegisteration")]
        [ProducesResponseType(typeof(UserInfo), 200)]
        public async Task<IActionResult> OwnerRegisteration([FromForm] OwnerRegisterDTO ownerDto)
        {
            return _OK(await _authService.OwnerRegister(ownerDto), "OwnerRegisteredSuccessfully");
        }

        [AllowAnonymous]
        [HttpPost("BrokerRegisteration")]
        [ProducesResponseType(typeof(UserInfo), 200)]
        public async Task<IActionResult> BrokerRegisteration([FromForm] BrokerRegisterDTO brokerDto)
        {
            return _OK(await _authService.BrokerRegister(brokerDto), "BrokerRegisteredSuccessfully");
        }

        [AllowAnonymous]
        [HttpPost("DelegateRegisteration")]
        [ProducesResponseType(typeof(UserInfo), 200)]
        public async Task<IActionResult> DelegateRegisteration([FromForm] DelegateRegisterDTO delegateDto)
        {
            return _OK(await _authService.DelegateRegister(delegateDto), "DelegateRegisteredSuccessfully");
        }

        [AllowAnonymous]
        [HttpPost("DeveloperRegisteration")]
        [ProducesResponseType(typeof(UserInfo), 200)]
        public async Task<IActionResult> DeveloperRegisteration([FromForm] DeveloperRegisterDTO developerDto)
        {
            return _OK(await _authService.DeveloperRegister(developerDto), "DeveloperRegisteredSuccessfully");
        }

        [AllowAnonymous]
        [HttpPost("ActivateUser")]
        [ProducesResponseType(typeof(UserData), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> ActivateUser([FromForm] UserVerifyDTO verifyDTO)
        {
            return _OK(await _authService.ActivateUser(UserId, verifyDTO), "UserVerifiedSuccessfully");
        }

        [AllowAnonymous]
        [HttpPost("ResendActivationOTP")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> ResendActivationOTP([FromQuery] string phone)
        {
            return _OK(await _authService.ResendActivationOTP(phone), "NotificationSendSuccesfullyToVerificationMethod");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserData), 200)]
        [ProducesResponseType(typeof(string), 403)]
        public async Task<IActionResult> Login([FromForm] UserLoginDto userLoginDTO)
        {
            var result = await _authService.Login(userLoginDTO);
            if (result.ActiveCode)
                return _OK(result, "LoggedInSuccessfully");

            return _Forbidden(new { result.Token, userLoginDTO.PhoneNumber }, "CodeIsNotActivated");
        }


        [HttpPost("UserData")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UserData()
        {
            var result = await _authService.UserData(UserId);
            return _OK(result);
        }



        [AllowAnonymous]
        [HttpPost("ForgetPassword")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> ForgetPassword([FromForm] ForgetPasswordDTO forgetPasswordDTO)
        {
            return _OK(await _authService.ForgetPassword(forgetPasswordDTO), "NotificationSendSuccesfullyToVerificationMethod");
        }


        [AllowAnonymous]
        [HttpPost("VaildateOTP")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> VaildateOTP([FromForm] VaildateOTPDTO dto)
        {
            return _OK(await _authService.VaildateOTP(dto), "OTPIsValid");
        }


        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDTO dto)
        {
            return _OK(await _authService.ResetPassword(dto), "PasswordResetedSuccessfully");
        }

        [HttpPost("Logout")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Logout([FromForm] LogoutDTO dto)
        {
            return _OK(await _authService.Logout(dto, UserId), "LoggedOutSuccessfully");
        }

        [HttpDelete("RemoveAccount")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> RemoveAccount()
        {
            return _OK(await _authService.RemoveAccount(UserId), "AccountWasRemovedSuccessfully");
        }


        //for test     
        [HttpGet("GetOTP")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetOTP()
        {
            return _OK(await _authService.GetOTP(UserId));
        }

        //for test     
        [AllowAnonymous]
        [HttpGet("GetOTPAnon")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetOTPAnon([RegularExpression("^(5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$")] string phone)
        {
            return _OK(await _authService.GetOTPAnon(phone));
        }
    }
}
