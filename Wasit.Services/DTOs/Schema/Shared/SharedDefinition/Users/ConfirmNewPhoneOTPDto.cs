using FluentValidation;
using Wasit.Core.Constants;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Users
{
    public class ConfirmNewPhoneOTPDto
    {
        public string OTP { get; set; }
        public string NewPhone { get; set; }
    }


    public class ConfirmNewPhoneOTPValidator : AbstractValidator<ConfirmNewPhoneOTPDto>
    {
        public ConfirmNewPhoneOTPValidator(ICurrentUserService currentUser)
        {
            var lang = currentUser.Language;

            RuleFor(x => x.OTP)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<ConfirmNewPhoneOTPDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.OTP), ValidationTypesEnum.Required))
                .MinimumLength(4).WithMessage(x => FluentValidationHelper.Message<ConfirmNewPhoneOTPDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.OTP), ValidationTypesEnum.MinLength, 4))
                .MaximumLength(4).WithMessage(x => FluentValidationHelper.Message<ConfirmNewPhoneOTPDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.OTP), ValidationTypesEnum.MaxLength, 4));

            RuleFor(x => x.NewPhone)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<ConfirmNewPhoneOTPDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.NewPhone), ValidationTypesEnum.Required))
            .Matches(RegEx.SaudiPhone)
            .WithMessage(x => FluentValidationHelper.Message<ConfirmNewPhoneOTPDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.NewPhone), ValidationTypesEnum.PhoneNumber));
        }
    }
}
