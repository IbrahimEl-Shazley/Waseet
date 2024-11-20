using FluentValidation;
using System.ComponentModel;
using Wasit.Core.Constants;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.SEC
{
    public class UserLoginDto
    {
        [DisplayName("رقم الجوال")]
        public string PhoneNumber { get; set; }

        [DisplayName("الرقم السري")]
        public string Password { get; set; }

        [DisplayName("نوع الجهاز")]
        public string DeviceType { get; set; }

        [DisplayName("الرقم التعريفي للجهاز")]
        public string DeviceId { get; set; }

        public bool RememberMe { get; set; }
    }

    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator(ICurrentUserService currentUserService)
        {
            var lang = currentUserService.Language;

            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<UserLoginDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PhoneNumber), ValidationTypesEnum.Required))
            .Matches(RegEx.SaudiPhone)
            .WithMessage(x => FluentValidationHelper.Message<UserLoginDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PhoneNumber), ValidationTypesEnum.PhoneNumber));

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<UserLoginDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Password), ValidationTypesEnum.Required))
            .MinimumLength(6).WithMessage(x => FluentValidationHelper.Message<UserLoginDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Password), ValidationTypesEnum.MinLength, 6));

            RuleFor(x => x.DeviceType)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<UserLoginDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.DeviceType), ValidationTypesEnum.Required));

            RuleFor(x => x.DeviceId)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<UserLoginDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.DeviceId), ValidationTypesEnum.Required));
        }

    }

}
