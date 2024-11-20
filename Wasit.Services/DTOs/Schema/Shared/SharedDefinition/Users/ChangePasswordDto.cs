using FluentValidation;
using System.ComponentModel;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.SEC;

namespace Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Users
{
    public class ChangePasswordDto
    {
        [DisplayName("كلمة المرور الحالية")]
        public string CurrentPassword { get; set; }

        [DisplayName("كلمة المرور الحالية")]
        public string NewPassword { get; set; }

        [DisplayName("كلمة المرور الحالية")]
        public string ConfirmNewPassword { get; set; }
    }


    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator(ICurrentUserService currentUser)
        {
            var lang = currentUser.Language;

            RuleFor(x => x.CurrentPassword)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<ChangePasswordDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.CurrentPassword), ValidationTypesEnum.Required))
           .MinimumLength(6).WithMessage(x => FluentValidationHelper.Message<ChangePasswordDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.CurrentPassword), ValidationTypesEnum.MinLength, 6));

            RuleFor(x => x.NewPassword)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<ChangePasswordDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.NewPassword), ValidationTypesEnum.Required))
           .MinimumLength(6).WithMessage(x => FluentValidationHelper.Message<ChangePasswordDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.CurrentPassword), ValidationTypesEnum.MinLength, 6));

            RuleFor(x => x.ConfirmNewPassword)
           .NotEmpty()
           .WithMessage(x => FluentValidationHelper.Message<UserRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.ConfirmNewPassword), ValidationTypesEnum.Required))
           .Equal(x => x.NewPassword)
           .WithMessage(x => FluentValidationHelper.Message<UserRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.ConfirmNewPassword), ValidationTypesEnum.ConfirmNewPassowrd));
        }
    }

}
