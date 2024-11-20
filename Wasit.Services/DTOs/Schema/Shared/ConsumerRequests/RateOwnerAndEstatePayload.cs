using FluentValidation;
using System.ComponentModel;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.Shared.ConsumerRequests
{
    public class RateOwnerAndEstatePayload
    {
        [DisplayName("رقم الطلب")]
        public long RequestId { get; set; }

        [DisplayName("تقييم المالك")]
        public double OwnerRating { get; set; }

        [DisplayName("تقييم العقار")]
        public double EstateRating { get; set; }

        [DisplayName("ملاحظات")]
        public string Feedback { get; set; }

        [DisplayName("القسم")]
        public CategoryType Category { get; set; }
    }


    public class RateOwnerAndEstatePayloadValidator : AbstractValidator<RateOwnerAndEstatePayload>
    {
        public RateOwnerAndEstatePayloadValidator(ICurrentUserService currentUser)
        {
            var lang = currentUser.Language;

            RuleFor(x => x.RequestId)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<RateOwnerAndEstatePayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.RequestId), ValidationTypesEnum.Required));

            RuleFor(x => x.OwnerRating)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<RateOwnerAndEstatePayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.OwnerRating), ValidationTypesEnum.Required));

            RuleFor(x => x.EstateRating)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<RateOwnerAndEstatePayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.EstateRating), ValidationTypesEnum.Required));

            RuleFor(x => x.Feedback)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<RateOwnerAndEstatePayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Feedback), ValidationTypesEnum.Required));

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<RateOwnerAndEstatePayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Category), ValidationTypesEnum.Required));

            RuleFor(x => x.Category)
                .Must(x => x == CategoryType.DailyRent || x == CategoryType.Entertainment).WithMessage(x => FluentValidationHelper.Message<RateOwnerAndEstatePayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Category), ValidationTypesEnum.CategoryThreeAndFour));
        }
    }

}
