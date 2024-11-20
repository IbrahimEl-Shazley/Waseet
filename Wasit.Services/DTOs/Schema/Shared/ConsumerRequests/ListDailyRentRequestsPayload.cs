using FluentValidation;
using System.ComponentModel;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;

namespace Wasit.Services.DTOs.Schema.Shared.ConsumerRequests
{
    public class ListDailyRentRequestsPayload : BaseListRequestsPayload
    {
        [DisplayName("رقم الصفحة")]
        public override int PageNumber { get; set; }

        [DisplayName("الحالة")]
        public override int Status { get; set; }
    }

    public class ListDailyRentRequestsPayloadValidator : AbstractValidator<ListDailyRentRequestsPayload>
    {
        public ListDailyRentRequestsPayloadValidator(ICurrentUserService currentUser)
        {
            var lang = currentUser.Language;

            RuleFor(x => x.PageNumber)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<ListDailyRentRequestsPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PageNumber), ValidationTypesEnum.Required));

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<ListDailyRentRequestsPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Status), ValidationTypesEnum.Required))
                .Must(x => x == 1 || x == 2 || x == 3 || x == 4)
                .WithMessage(x => FluentValidationHelper.Message<ListDailyRentRequestsPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Status), ValidationTypesEnum.Status4));
        }
    }
}
