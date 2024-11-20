using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.SEC;
using Wasit.Services.DTOs.Schema.SEC.Broker;
using Wasit.Services.DTOs.Schema.SEC.Delegate;
using Wasit.Services.DTOs.Schema.SEC.Developer;
using Wasit.Services.DTOs.Schema.SEC.Owner;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Users;

namespace Wasit.Services.Interfaces.General
{
    public interface IUserService: IBaseService  
    {
        Task<bool> ChangeNotify(string userId);
        Task<OwnerInfoDto> UpdateOwnerProfile(string userId, OwnerUpdateDto input);
        Task<IndividualBrokerInfoDto> UpdateBrokerProfile(string userId, AccountType accountType, BrokerUpdateDto input);
        Task<DelegateInfoDto> UpdateDelegateProfile(string userId, DelegateUpdateDto input);
        Task<DeveloperInfoDto> UpdateDeveloperProfile(string userId, DeveloperUpdateDto input);
        Task<bool> ConfirmCurrentPhone(string userId, ConfirmCurrentPhoneDto input);
        Task<bool> ValidataOTP(string userId, UserVerifyDTO input);
        Task<string> ValidateNewPhone(string userId, ValidateNewPhoneDto input);
        Task<bool> ConfirmNewPhoneOTP(string userId, ConfirmNewPhoneOTPDto input);
        Task<bool> ChangePassword(string userId, ChangePasswordDto input);
    }
}
