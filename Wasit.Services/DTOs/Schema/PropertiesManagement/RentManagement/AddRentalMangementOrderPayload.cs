using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.PropertiesManagement.RentManagement
{
    public class AddRentalMangementOrderPayload
    {
        [DisplayName("رقم الصك العقاري")]
        public string UniqueNumber { get; set; }

        [DisplayName("الاسم")]
        public string Name { get; set; }

        [DisplayName("الحي")]
        public long RegionId { get; set; }

        [DisplayName("الصورة")]
        public IFormFile Image { get; set; }

        [DisplayName("الشقق")]
        public List<AddApartmentDto> Apartments { get; set; } = new List<AddApartmentDto>();
    }


    public class AddRentalMangementOrderPayloadValidator : AbstractValidator<AddRentalMangementOrderPayload>
    {
        public AddRentalMangementOrderPayloadValidator(ICurrentUserService currentUser)
        {
            var lang = currentUser.Language;

            RuleFor(x => x.UniqueNumber)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<AddRentalMangementOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.UniqueNumber), ValidationTypesEnum.Required));

             RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<AddRentalMangementOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Name), ValidationTypesEnum.Required));

            RuleFor(x => x.RegionId)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<AddRentalMangementOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.RegionId), ValidationTypesEnum.Required));

            RuleFor(x => x.Image)
            .NotNull()
            .WithMessage(x => FluentValidationHelper.Message<AddRentalMangementOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Image), ValidationTypesEnum.Required));

            RuleFor(x => x.Apartments)
            .Must(x => x.Count > 0)
            .WithMessage(x => FluentValidationHelper.Message<AddRentalMangementOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Apartments), ValidationTypesEnum.Required));
        }

    }
}
