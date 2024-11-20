using FluentValidation;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates
{
    public class CreateDailyRentEstateDto : BaseCreateEstateDto
    {
        public class CreateDailyRentEstateValidator : AbstractValidator<CreateDailyRentEstateDto>
        {
            public CreateDailyRentEstateValidator(ICurrentUserService currentUser)
            {
                var lang = currentUser.Language;

                RuleFor(x => x.UniqueNumber)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.UniqueNumber), ValidationTypesEnum.Required));

                RuleFor(x => x.NameAr)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.NameAr), ValidationTypesEnum.Required));

                RuleFor(x => x.NameEn)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.NameEn), ValidationTypesEnum.Required));

                RuleFor(x => x.EstateTypeId)
                    .NotEmpty()
                    .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.EstateTypeId), ValidationTypesEnum.Required));

                RuleFor(x => x.RegionId)
                    .NotEmpty()
                    .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.RegionId), ValidationTypesEnum.Required));

                RuleFor(x => x.Lat)
                    .NotEmpty()
                    .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lat), ValidationTypesEnum.Required));

                RuleFor(x => x.Lng)
                    .NotEmpty()
                    .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lng), ValidationTypesEnum.Required));

                RuleFor(x => x.Location)
                    .NotEmpty()
                    .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Location), ValidationTypesEnum.Required));

                RuleFor(x => x.Area)
                    .NotEmpty()
                    .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Area), ValidationTypesEnum.Required));

                RuleFor(x => x.Description)
                    .NotEmpty()
                    .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Description), ValidationTypesEnum.Required));

                RuleFor(x => x.Features)
                    .NotEmpty()
                    .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Features), ValidationTypesEnum.Required));

                RuleFor(x => x.IsDevelopable)
                   .NotNull()
                   .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IsDevelopable), ValidationTypesEnum.Required));

                RuleFor(x => x.Price)
                    .NotEmpty()
                    .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Price), ValidationTypesEnum.Required));

                RuleFor(x => x.Images)
                    .Must(x => x.Count > 0)
                    .WithMessage(x => FluentValidationHelper.Message<CreateDailyRentEstateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Images), ValidationTypesEnum.Required));
            }
        }
    }
}
