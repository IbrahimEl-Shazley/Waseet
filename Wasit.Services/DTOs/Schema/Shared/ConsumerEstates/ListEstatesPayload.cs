using FluentValidation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.Shared.ConsumerEstates
{
    public class ListEstatesPayload
    {
        [DisplayName("رقم الصفحة")]
        [Required]
        public int PageNumber { get; set; } = 1;

        [DisplayName("القسم")]
        [Required]
        public CategoryType Category { get; set; }
        public long? EstateTypeId { get; set; }
        public string? PublisherId { get; set; }
        public string? Search { get; set; }
        public double? StartPrice { get; set; }
        public double? EndPrice { get; set; }
        public int? CityId { get; set; }
        public bool? OrderByDistanceDesc { get; set; }
        public bool? OrderByDistanceAsc { get; set; }
    }


    public class ListEstatesPayloadValidator : AbstractValidator<ListEstatesPayload>
    {
        public ListEstatesPayloadValidator(ICurrentUserService currentUser)
        {
            var lang = currentUser.Language;

            RuleFor(x => x.PageNumber)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<ListEstatesPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PageNumber), ValidationTypesEnum.Required));

            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<ListEstatesPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Category), ValidationTypesEnum.Required));
        }
    }
}
