using FluentValidation;
using System.ComponentModel;
using Wasit.Core.Constants;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Users
{
    public class ConfirmCurrentPhoneDto
    {
        [DisplayName("رقم الجوال")]
        public string CurrentPhone { get; set; }
    }


    public class ConfirmCurrentPhoneDtoValidator : AbstractValidator<ConfirmCurrentPhoneDto>
    {
        public ConfirmCurrentPhoneDtoValidator(ICurrentUserService currentUser)
        {
            var lang = currentUser.Language;

            RuleFor(x => x.CurrentPhone)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<ConfirmCurrentPhoneDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.CurrentPhone), ValidationTypesEnum.Required))
            .Matches(RegEx.SaudiPhone)
            .WithMessage(x => FluentValidationHelper.Message<ConfirmCurrentPhoneDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.CurrentPhone), ValidationTypesEnum.PhoneNumber));
        }
    }
}
