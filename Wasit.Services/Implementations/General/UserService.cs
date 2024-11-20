using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.Helpers.General;
using Wasit.Core.Helpers.IO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.SEC;
using Wasit.Services.DTOs.Schema.SEC.Broker;
using Wasit.Services.DTOs.Schema.SEC.Delegate;
using Wasit.Services.DTOs.Schema.SEC.Developer;
using Wasit.Services.DTOs.Schema.SEC.Owner;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Users;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;

namespace Wasit.Services.Implementations.General
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly UserManager<ApplicationDbUser> _userManager;

        public UserService(IServiceProvider serviceProvider, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            _userRepository = unitOfWork.Repository<IUserRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _currentUser = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
            _userManager = (UserManager<ApplicationDbUser>)serviceProvider.GetService(typeof(UserManager<ApplicationDbUser>));
        }


        public async Task<bool> ChangeNotify(string userId)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId) ??
                throw new BussinessRuleException("UserNotExist");

            user.AllowNotify = !user.AllowNotify;
            _userRepository.UpdateUser(user);
            await _unitOfWork.SaveChangeAsync();

            return user.AllowNotify;
        }


        public async Task<OwnerInfoDto> UpdateOwnerProfile(string userId, OwnerUpdateDto input)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId) ??
                throw new BussinessRuleException("UserNotExist");

            user = _mapper.Map(input, user);
            user.ImgProfile = input.ImgProfile != null ? IOHelper.Upload(input.ImgProfile, (int)FileName.Users) : user.ImgProfile;

            _userRepository.UpdateUser(user);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<OwnerInfoDto>(user);
        }


        public async Task<IndividualBrokerInfoDto> UpdateBrokerProfile(string userId, AccountType accountType, BrokerUpdateDto input)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId && x.AccountType == accountType) ??
               throw new BussinessRuleException("UserNotExist");

            user = _mapper.Map(input, user);
            user.ImgProfile = input.ImgProfile != null ? IOHelper.Upload(input.ImgProfile, (int)FileName.Users) : user.ImgProfile;
            _userRepository.UpdateUser(user);

            await _unitOfWork.SaveChangeAsync();

            dynamic model;
            _ = accountType switch
            {
                AccountType.Individual => model = _mapper.Map<IndividualBrokerInfoDto>(user),
                AccountType.Facility => model = _mapper.Map<FacilityBrokerInfoDto>(user)
            };

            return model;
        }


        public async Task<DelegateInfoDto> UpdateDelegateProfile(string userId, DelegateUpdateDto input)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId) ??
                 throw new BussinessRuleException("UserNotExist");

            user = _mapper.Map(input, user);
            user.ImgProfile = input.ImgProfile != null ? IOHelper.Upload(input.ImgProfile, (int)FileName.Users) : user.ImgProfile;

            _userRepository.UpdateUser(user);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<DelegateInfoDto>(user);
        }


        public async Task<DeveloperInfoDto> UpdateDeveloperProfile(string userId, DeveloperUpdateDto input)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId) ??
                 throw new BussinessRuleException("UserNotExist");

            user = _mapper.Map(input, user);
            user.ImgProfile = input.ImgProfile != null ? IOHelper.Upload(input.ImgProfile, (int)FileName.Users) : user.ImgProfile;
            user.CoverPhoto = input.CoverPhoto != null ? IOHelper.Upload(input.CoverPhoto, (int)FileName.CoverPhotos) : user.CoverPhoto;

            _userRepository.UpdateUser(user);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<DeveloperInfoDto>(user);
        }


        public async Task<bool> ConfirmCurrentPhone(string userId, ConfirmCurrentPhoneDto input)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId) ??
                 throw new BussinessRuleException("UserNotExist");

            if (user.PhoneNumber != input.CurrentPhone)
                throw new BussinessRuleException("TheCurrentPhoneIsIncorrectPleaseTryAgain");

            #region OTP
            string OTP = OTPHelper.OTP();
            // TODO: change to default verification method
            var verificationMethod = NotificationTypeEnum.SMS;
            await _notificationService.Send(new DTOs.General.Notification
            {
                NotificationCategory = NotificationCategoryEnum.ActivateAccount,
                To = verificationMethod == NotificationTypeEnum.Email ? user.Email : user.PhoneNumber,
                Input = OTP
            }, verificationMethod);
            user.Code = OTP;
            _userRepository.UpdateUser(user);
            #endregion

            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> ValidataOTP(string userId, UserVerifyDTO input)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId) ??
                throw new BussinessRuleException("UserNotExist");

            if (user.Code != input.OTP)
                throw new BussinessRuleException("ActivationCodeIsNotValidOrExpired");

            return true;
        }



        public async Task<string> ValidateNewPhone(string userId, ValidateNewPhoneDto input)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId) ??
                 throw new BussinessRuleException("UserNotExist");

            if (user.PhoneNumber == input.NewPhone)
                throw new BussinessRuleException("TheNewPhoneIsTheSameAsTheCurrentPhonePleaseTryAgain");

            if (await _userRepository.PhoneExistsBeforEdit(input.NewPhone, userId))
                throw new BussinessRuleException("ThePhoneIsAlreadyExistPleaseTryAgain");

            #region OTP
            string OTP = OTPHelper.OTP();
            // TODO: change to default verification method
            var verificationMethod = NotificationTypeEnum.SMS;
            await _notificationService.Send(new DTOs.General.Notification
            {
                NotificationCategory = NotificationCategoryEnum.ActivateAccount,
                To = verificationMethod == NotificationTypeEnum.Email ? user.Email : user.PhoneNumber,
                Input = OTP
            }, verificationMethod);
            user.Code = OTP;
            user.ActiveCode = false;
            _userRepository.UpdateUser(user);
            await _unitOfWork.SaveChangeAsync();
            #endregion

            return input.NewPhone;
        }


        public async Task<bool> ConfirmNewPhoneOTP(string userId, ConfirmNewPhoneOTPDto input)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId) ?? 
                throw new BussinessRuleException("UserNotExist");

            if (user.ActiveCode)
                throw new BussinessRuleException("UserAlreadyActivated");

            if (user.Code != input.OTP)
                throw new BussinessRuleException("ActivationCodeIsNotValidOrExpired");

            user.ActiveCode = true;
            user.PhoneNumber = input.NewPhone;
            _userRepository.UpdateUser(user);

            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> ChangePassword(string userId, ChangePasswordDto input)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId) ??
                 throw new BussinessRuleException("UserNotExist");

            if (!await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
                throw new BussinessRuleException("TheCurrentPasswordIsIncorrectPleaseTryAgain");

            if (await _userManager.CheckPasswordAsync(user, input.NewPassword))
                throw new BussinessRuleException("TheNewPasswordIsTheSameAsTheCurrentPasswordPleaseTryAgain");

            var result = await _userManager.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword);

            if (!result.Succeeded)
                throw new BussinessRuleException("Error");

            return await _unitOfWork.SaveChangeAsync();
        }


    }
}
