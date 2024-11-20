using FluentValidation;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.SEC
{
    public class UserVerifyDTO
    {
        public string OTP { get; set; }
    }


    public class UserVerifyValidator : AbstractValidator<UserVerifyDTO>
    {
        public UserVerifyValidator(ICurrentUserService currentUserService)
        {
            var lang = currentUserService.Language;

            RuleFor(x => x.OTP)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<UserVerifyDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.OTP), ValidationTypesEnum.Required))
                .MinimumLength(4).WithMessage(x => FluentValidationHelper.Message<UserVerifyDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.OTP), ValidationTypesEnum.MinLength, 4))
                .MaximumLength(4).WithMessage(x => FluentValidationHelper.Message<UserVerifyDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.OTP), ValidationTypesEnum.MaxLength, 4));
        }

    }
}
