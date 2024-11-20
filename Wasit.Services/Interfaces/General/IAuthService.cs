using Wasit.Services.DTOs.Schema.SEC;

namespace Wasit.Services.Interfaces.General
{
    public interface IAuthService: IBaseService
    {
        Task<UserInfo> OwnerRegister(OwnerRegisterDTO OwnerDTO);
        Task<UserInfo> BrokerRegister(BrokerRegisterDTO BrokerDTO);
        Task<UserInfo> DelegateRegister(DelegateRegisterDTO DelegateDTO);
        Task<UserInfo> DeveloperRegister(DeveloperRegisterDTO DeveloperDTO);

        Task<dynamic> ActivateUser(string userId, UserVerifyDTO dto);
        Task<bool> ResendActivationOTP(string phone);

        Task<dynamic> Login(UserLoginDto userLoginDTO);
        Task<object> UserData(string userId);

        Task<bool> ForgetPassword(ForgetPasswordDTO dto);
        Task<bool> VaildateOTP(VaildateOTPDTO dto);
        Task<bool> ResetPassword(ResetPasswordDTO dto);

        Task<bool> Logout(LogoutDTO dto,string UserId);
        Task<bool> RemoveAccount(string userId);

        Task<string> GetOTP(string userId);
        Task<string> GetOTPAnon(string phone);
    }
}
