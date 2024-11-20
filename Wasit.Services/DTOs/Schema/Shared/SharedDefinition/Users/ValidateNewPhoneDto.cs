using FluentValidation;
using System.ComponentModel;
using Wasit.Core.Constants;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Users
{
    public class ValidateNewPhoneDto
    {
        [DisplayName("رقم الجوال الجديد")]
        public string NewPhone { get; set; }
    }


    public class ChangePhoneDtoValidator : AbstractValidator<ValidateNewPhoneDto>
    {
        public ChangePhoneDtoValidator(ICurrentUserService currentUser)
        {
            var lang = currentUser.Language;

            RuleFor(x => x.NewPhone)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<ValidateNewPhoneDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.NewPhone), ValidationTypesEnum.Required))
            .Matches(RegEx.SaudiPhone)
            .WithMessage(x => FluentValidationHelper.Message<ValidateNewPhoneDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.NewPhone), ValidationTypesEnum.PhoneNumber));
        }
    }
}
