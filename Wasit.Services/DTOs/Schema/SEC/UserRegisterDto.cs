using FluentValidation;
using System.ComponentModel;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.SEC
{
    public class UserRegisterDTO
    {
        //[DisplayName("رقم الهوية")]
        //public string IdentityId { get; set; }
        [DisplayName("اسم المستخدم")]
        public string UserName { get; set; }

        [DisplayName("البريد الإلكترونى")]
        public string Email { get; set; }
        [DisplayName("رقم الجوال")]
        public string PhoneNumber { get; set; }
        [DisplayName("الرقم السري")]
        public string Password { get; set; }
        [DisplayName("Device Id")]
        public string DeviceId { get; set; }
        [DisplayName("نوع الجهاز")]
        public string DeviceType { get; set; }
        [DisplayName("اسم المشروع")]
        public string ProjectName { get; set; } = "N Base 6";

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }

    }




    public class UserRegisterValidator : AbstractValidator<UserRegisterDTO>
    {

        public UserRegisterValidator(ICurrentUserService currentUserService)
        {
            var lang = currentUserService.Language;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<UserRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Email), ValidationTypesEnum.Required))
                .MinimumLength(5).WithMessage(x => FluentValidationHelper.Message<UserRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Email), ValidationTypesEnum.MinLength, 5))
                .MaximumLength(50).WithMessage(x => FluentValidationHelper.Message<UserRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Email), ValidationTypesEnum.MaxLength, 50))
                .EmailAddress().WithMessage(x => FluentValidationHelper.Message<UserRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Email), ValidationTypesEnum.Email));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<UserRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Password), ValidationTypesEnum.Required))
                .MinimumLength(6).WithMessage(x => FluentValidationHelper.Message<UserRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Email), ValidationTypesEnum.MinLength, 6));

            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<UserRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PhoneNumber), ValidationTypesEnum.Required));
            //.Matches("^(009665|9665|\\+9665|05|5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$")
            //.WithMessage(x => FluentValidationHelper.Message<UserRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PhoneNumber), ValidationTypesEnum.PhoneNumber));

        }
    }

}
