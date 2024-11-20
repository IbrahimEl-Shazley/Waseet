using AAITHelper;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.ExtensionsMethods;
using Wasit.Core.Helpers.General;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.SEC;
using Wasit.Services.DTOs.Schema.SEC.Broker;
using Wasit.Services.DTOs.Schema.SEC.Delegate;
using Wasit.Services.DTOs.Schema.SEC.Owner;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;

namespace Wasit.Services.Implementations.General
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly UserManager<ApplicationDbUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly INotificationService _notificationService;

        public AuthService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userManager = (UserManager<ApplicationDbUser>)serviceProvider.GetService(typeof(UserManager<ApplicationDbUser>));
            _configuration = (IConfiguration)serviceProvider.GetService(typeof(IConfiguration));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _userRepository = uow.Repository<IUserRepository>();
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
            _uow = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
        }

        public async Task<UserInfo> OwnerRegister(OwnerRegisterDTO OwnerDTO)
        {
            ApplicationDbUser user = new ApplicationDbUser();

            string EnglishPhoneNumber = HelperNumber.ConvertArabicNumberToEnglish(OwnerDTO.PhoneNumber);

            bool phoneExists = await _userRepository.PhoneExistsBeforeRegister(EnglishPhoneNumber);
            bool IdenetityNumberExists = await _userRepository.IdentityNumberExists(OwnerDTO.IDNumber);

            #region Validations
            if (phoneExists)
                throw new BussinessRuleException("ThisPhoneAlreadyExists");

            if (IdenetityNumberExists)
                throw new BussinessRuleException("ThisIdentityNumberAlreadyExists");
            #endregion
            #region Registration Transaction 
            try
            {
                _userRepository.BeginTrnsactionAsync();
                #region MapUser
                user = _mapper.Map(OwnerDTO, user);
                #endregion
                return await MainRegister(user, OwnerDTO.Password);
            }
            catch (Exception)
            {
                _userRepository.RollBackAsync();
                throw new BussinessRuleException("Can'tRegister");
            }
            #endregion 
        }

        public async Task<UserInfo> BrokerRegister(BrokerRegisterDTO BrokerDTO)
        {
            ApplicationDbUser user = new ApplicationDbUser();

            string EnglishPhoneNumber = HelperNumber.ConvertArabicNumberToEnglish(BrokerDTO.PhoneNumber);

            bool phoneExists = await _userRepository.PhoneExistsBeforeRegister(EnglishPhoneNumber);
            bool IdentityNumberExists = await _userRepository.IdentityNumberExists(BrokerDTO.IDNumber);

            #region Validations
            if (phoneExists)
                throw new BussinessRuleException("ThisPhoneAlreadyExists");

            if (IdentityNumberExists)
                throw new BussinessRuleException("ThisIdentityNumberAlreadyExists");

            if (BrokerDTO.AccountType == AccountType.Facility)
            {
                bool CommercialNumberExists = await _userRepository.CommercialNumberExists(BrokerDTO.CommercialNo);
                if (CommercialNumberExists)
                    throw new BussinessRuleException("CommercialNoAlreadyExists");
            }

            #endregion
            #region Registration Transaction 
            try
            {
                _userRepository.BeginTrnsactionAsync();
                #region MapUser
                user = _mapper.Map(BrokerDTO, user);
                #endregion
                return await MainRegister(user, BrokerDTO.Password);
            }
            catch (Exception ex)
            {
                _userRepository.RollBackAsync();
                throw new BussinessRuleException("Can'tRegister");
            }
            #endregion 
        }

        public async Task<UserInfo> DelegateRegister(DelegateRegisterDTO DelegateDTO)
        {
            ApplicationDbUser user = new ApplicationDbUser();

            string EnglishPhoneNumber = HelperNumber.ConvertArabicNumberToEnglish(DelegateDTO.PhoneNumber);

            bool phoneExists = await _userRepository.PhoneExistsBeforeRegister(EnglishPhoneNumber);
            bool IdenetityNumberExists = await _userRepository.IdentityNumberExists(DelegateDTO.IDNumber);

            #region Validations
            if (phoneExists)
                throw new BussinessRuleException("ThisPhoneAlreadyExists");

            if (IdenetityNumberExists)
                throw new BussinessRuleException("ThisIdentityNumberAlreadyExists");
            #endregion
            #region Registration Transaction 
            try
            {
                _userRepository.BeginTrnsactionAsync();
                #region MapUser
                user = _mapper.Map(DelegateDTO, user);
                #endregion
                return await MainRegister(user, DelegateDTO.Password);
            }
            catch (Exception ex)
            {
                _userRepository.RollBackAsync();
                throw new BussinessRuleException("Can'tRegister");
            }
            #endregion 
        }

        public async Task<UserInfo> DeveloperRegister(DeveloperRegisterDTO DeveloperDTO)
        {
            ApplicationDbUser user = new ApplicationDbUser();

            string EnglishPhoneNumber = HelperNumber.ConvertArabicNumberToEnglish(DeveloperDTO.PhoneNumber);

            bool phoneExists = await _userRepository.PhoneExistsBeforeRegister(EnglishPhoneNumber);
            bool mailExist = await _userRepository.MailExistsRegister(DeveloperDTO.Email);

            #region Validations
            if (phoneExists)
                throw new BussinessRuleException("ThisMobileAlreadyExists");

            if (mailExist)
                throw new BussinessRuleException("ThisEmailIsAlreadyExists");
            #endregion
            #region Registration Transaction 
            try
            {
                _userRepository.BeginTrnsactionAsync();
                #region MapUser
                user = _mapper.Map(DeveloperDTO, user);
                #endregion
                return await MainRegister(user, DeveloperDTO.Password);
            }
            catch (Exception ex)
            {
                _userRepository.RollBackAsync();
                throw new BussinessRuleException("Can'tRegister");
            }
            #endregion 
        }

        public async Task<UserInfo> MainRegister(ApplicationDbUser user, string password)
        {
            UserInfo userInfo = new UserInfo();
            string MobileRole = Enum.GetName(typeof(Roles), Roles.Mobile);
            try
            {

                #region Save User 
                var result = await _userManager.CreateAsync(user, password);
                #endregion
                if (!result.Succeeded)
                    throw new BussinessRuleException("Can'tRegister");

                #region AddRole
                IdentityResult RoleResult = await _userManager.AddToRoleAsync(user, MobileRole);
                #endregion


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
                await _uow.SaveChangeAsync();
                #endregion


                if (RoleResult.Succeeded)
                    _userRepository.CommitAsync();


            }
            catch (Exception ex)
            {
                _userRepository.RollBackAsync();
                throw new BussinessRuleException("Can'tRegister");
            }

            userInfo = _mapper.Map(user, userInfo);

            userInfo.IsAuthenticated = true;
            userInfo.Roles = new List<string> { MobileRole };
            userInfo.Token = GenerateToken(user);
            userInfo.ActiveCode = false;

            return userInfo;

        }

        public async Task<dynamic> ActivateUser(string userId, UserVerifyDTO dto)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId, true);
            if (user == null)
                throw new BussinessRuleException("UserNotExist");

            if (user.ActiveCode)
                throw new BussinessRuleException("UserAlreadyActivated");

            var validCode = user.Code == dto.OTP;
            if (!validCode)
                throw new BussinessRuleException("ActivationCodeIsNotValidOrExpired");

            user.ActiveCode = true;
            _userRepository.UpdateUser(user);
            await _uow.SaveChangeAsync();

            var userdata = await UserData(user.Id);
            userdata.Token = GenerateToken(user);

            return userdata;
        }


        public async Task<bool> ResendActivationOTP(string phone)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.PhoneNumber == phone, true);
            if (user == null)
                throw new BussinessRuleException("UserNotExist");

            string OTP = OTPHelper.OTP();
            user.Code = OTP;
            _userRepository.UpdateUser(user);
            return await _uow.SaveChangeAsync();
        }


        public async Task<dynamic> Login(UserLoginDto userLoginDTO)
        {
            var user = await _userRepository
                .GetUser<ApplicationDbUser>(u => u.PhoneNumber.Equals(userLoginDTO.PhoneNumber))
                .FirstOrDefaultAsync() ?? throw new BussinessRuleException("ThePhoneIsIncorrectPleaseTryAgain");

            if (!await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
                throw new BussinessRuleException("ThePasswordIsIncorrectPleaseTryAgain");

            if (!user.IsActive)
                throw new BussinessRuleException("UserIsBlocked");

            var deviceModel = _mapper.Map<DeviceId>(userLoginDTO, o => o.Items[nameof(DeviceId.UserId)] = user.Id);
            var oldDeviceIds = await _userRepository
                .GetUser<DeviceId>(x => x.DeviceId_ == deviceModel.DeviceId_ && x.UserId == user.Id)
                .ToListAsync();

            if (oldDeviceIds.Count != 0)
                foreach (var item in oldDeviceIds)
                    _userRepository.Remove(item);

            await _userRepository.AddEntityAsync(deviceModel);
            await _uow.SaveChangeAsync();

            var userdata = await UserData(user.Id);
            var token = GenerateToken(user, userLoginDTO.RememberMe);
            userdata.Token = token;

            return userdata;
        }


        public async Task<dynamic> UserData(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                return user.UserType switch
                {
                    "Owner" => GetOwnerInfo(user),
                    "Broker" => GetBrokerInfo(user),
                    "Delegate" => GetDelegateInfo(user),
                    "Developer" => GetDeveloperInfo(user),
                    _ => new BussinessRuleException("UserNotExist")
                };
            }
            catch
            {
                return new BussinessRuleException("UnexpectedFailurePLeaseTryAgainLater");
            }
        }

        private OwnerInfoDto GetOwnerInfo(ApplicationDbUser user)
        {
            var model = _mapper.Map<OwnerInfoDto>(user);
            return model;
        }

        private IndividualBrokerInfoDto GetBrokerInfo(ApplicationDbUser user)
        {
            dynamic model;
            _ = user.AccountType switch
            {
                AccountType.Individual => model = _mapper.Map<IndividualBrokerInfoDto>(user),
                AccountType.Facility => model = _mapper.Map<FacilityBrokerInfoDto>(user)
            };

            return model;
        }

        private DelegateInfoDto GetDelegateInfo(ApplicationDbUser user)
        {
            var model = _mapper.Map<DelegateInfoDto>(user);
            return model;
        }

        private OwnerInfoDto GetDeveloperInfo(ApplicationDbUser user)
        {
            var model = _mapper.Map<OwnerInfoDto>(user);
            return model;
        }


        public async Task<bool> ForgetPassword(ForgetPasswordDTO dto)
        {
            var user = await _userRepository.
                             GetUser<ApplicationDbUser>(u => u.PhoneNumber.Equals(dto.PhoneNumber))
                            .FirstOrDefaultAsync() ?? throw new BussinessRuleException("ThePhoneIsIncorrectPleaseTryAgain");

            string OTP = OTPHelper.OTP();
            user.Code = OTP;

            _userRepository.UpdateUser(user);
            return await _uow.SaveChangeAsync();
        }


        public async Task<bool> ResetPassword(ResetPasswordDTO dto)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.PhoneNumber == dto.PhoneNumber)
                                            ?? throw new BussinessRuleException("ThePhoneIsIncorrectPleaseTryAgain");

            var validToken = user.Code == dto.OTP;
            if (!validToken)
                throw new BussinessRuleException("ActivationCodeIsNotValidOrExpired");

            var changetoken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, changetoken, dto.NewPassword);
            if (!resetPasswordResult.Succeeded)
                throw new BussinessRuleException("Error");

            _userRepository.UpdateUser(user);

            return await _uow.SaveChangeAsync();
        }

        public async Task<bool> Logout(LogoutDTO dto, string UserId)
        {
            var UserDevices = await _userRepository.
                                UserFirstOrDefaultAsync<DeviceId>(u => u.UserId.Equals(UserId) && u.DeviceId_ == dto.DeviceId);

            if (UserDevices is not null)
                _userRepository.Remove(UserDevices);

            return await _uow.SaveChangeAsync();
        }

        public async Task<bool> RemoveAccount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.PhoneNumber = user.PhoneNumber + Guid.NewGuid().ToString();
            user.Email = user.Email + Guid.NewGuid().ToString();
            user.NormalizedEmail = user.NormalizedEmail + Guid.NewGuid().ToString();
            user.UserName = user.UserName + Guid.NewGuid().ToString();
            user.NormalizedUserName = user.NormalizedUserName + Guid.NewGuid().ToString();
            user.IsDeleted = true;
            _userRepository.UpdateUser(user);
            await _uow.SaveChangeAsync();

            return true;
        }


        public async Task<string> GetOTP(string userId)
        {
            //if (Hosting.EnvironmentName == Environments.Production.ToString())
            //    return "YOU CAN'T ACCESS OTP";
            return (await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId)).Code;
        }


        public async Task<string> GetOTPAnon(string phone)
        {
            //if (Hosting.EnvironmentName == Environments.Production.ToString())
            //    return "YOU CAN'T ACCESS OTP";
            return (await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.PhoneNumber == phone)).Code;
        }

        #region private

        private string GenerateToken(ApplicationDbUser user, bool rememberMe = true)
        {
            Claim[] claims = new[]
            {
                new Claim("userId", user.Id.ToString().Encrypt()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName.Encrypt()),
                new Claim(ClaimTypes.Role, user.UserType.Encrypt()),
            };

            SymmetricSecurityKey signatureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
            SigningCredentials credentials = new SigningCredentials(signatureKey, SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor accessTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = rememberMe ? DateTime.UtcNow.AddYears(1) : DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:ExpiryInMinutes"]))
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken accessToken = tokenHandler.CreateToken(accessTokenDescriptor);

            return tokenHandler.WriteToken(accessToken);

        }

        public async Task<bool> VaildateOTP(VaildateOTPDTO dto)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.PhoneNumber == dto.Phone)
                                            ?? throw new BussinessRuleException("ThePhoneIsIncorrectPleaseTryAgain");

            var validToken = user.Code == dto.Code;
            if (!validToken)
                throw new BussinessRuleException("ActivationCodeIsNotValidOrExpired");

            return true;
        }


        #endregion
    }
}
