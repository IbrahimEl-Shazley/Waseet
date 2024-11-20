using FluentValidation;
using System.ComponentModel;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.SEC
{
    public class ForgetPasswordDTO
    {
        [DisplayName("رقم الجوال")]
        public string PhoneNumber { get; set; }
    }
    public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordDTO>
    {
        public ForgetPasswordValidator(ICurrentUserService currentUserService)
        {
            var lang = currentUserService.Language;

            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<ForgetPasswordDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PhoneNumber), ValidationTypesEnum.Required))
            .Matches("^(5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$")
            .WithMessage(x => FluentValidationHelper.Message<ForgetPasswordDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PhoneNumber), ValidationTypesEnum.PhoneNumber));

        }

    }

}