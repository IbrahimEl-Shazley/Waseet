using FluentValidation;
using System.ComponentModel;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.SEC
{
    public class ResetPasswordDTO
    {
        [DisplayName("رقم الجوال")]
        public string PhoneNumber { get; set; }

        [DisplayName("رمز التحقق")]
        public string OTP { get; set; }

        [DisplayName("الرقم السري")]
        public string NewPassword { get; set; }
    }

    public class ResetPasswordValidator : AbstractValidator<ResetPasswordDTO>
    {
        public ResetPasswordValidator(ICurrentUserService currentUserService)
        {
            var lang = currentUserService.Language;

            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<ResetPasswordDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PhoneNumber), ValidationTypesEnum.Required))
            .Matches("^(5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$")
            .WithMessage(x => FluentValidationHelper.Message<ResetPasswordDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PhoneNumber), ValidationTypesEnum.PhoneNumber));

            RuleFor(x => x.OTP)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<ResetPasswordDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.OTP), ValidationTypesEnum.Required))
                .MinimumLength(4).WithMessage(x => FluentValidationHelper.Message<ResetPasswordDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.OTP), ValidationTypesEnum.MinLength, 4))
                .MaximumLength(4).WithMessage(x => FluentValidationHelper.Message<ResetPasswordDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.OTP), ValidationTypesEnum.MaxLength, 4));

            RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<ResetPasswordDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.NewPassword), ValidationTypesEnum.Required))
            .MinimumLength(6).WithMessage(x => FluentValidationHelper.Message<ResetPasswordDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.NewPassword), ValidationTypesEnum.MinLength, 6));

        }

    }

}
