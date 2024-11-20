using FluentValidation;
using System.ComponentModel;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.SEC
{
    public class LogoutDTO
    {
        [DisplayName("رقم الجهاز")]
        public string DeviceId { get; set; }

    }

    public class LogoutDTOValidator : AbstractValidator<LogoutDTO>
    {
        public LogoutDTOValidator(ICurrentUserService currentUserService)
        {
            var lang = currentUserService.Language;

            RuleFor(x => x.DeviceId)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<LogoutDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.DeviceId), ValidationTypesEnum.Required));
        }

    }

}
