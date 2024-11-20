using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.PropertiesManagement.MaintainanceMangment
{
    public class AddMaintainanceOrderPayload
    {
        [DisplayName("رقم الصك العقاري")]
        public string UniqueNumber { get; set; }

        [DisplayName("اسم العقار")]
        public string Name { get; set; }

        [DisplayName("الحي")]
        public long RegionId { get; set; }

        [DisplayName("خط العرض")]
        public string Lat { get; set; }

        [DisplayName("خط الطول")]
        public string Lng { get; set; }

        [DisplayName("موقع العقار")]
        public string Location { get; set; }

        [DisplayName("مساحة العقار")]
        public double Area { get; set; }

        [DisplayName("صورة العقار")]
        public IFormFile Image { get; set; }
        
        [DisplayName("وسيلة الدفع")]
        public TypePay PaymentMethod { get; set; }
    }


    public class AddMaintainanceOrderPayloadValidator : AbstractValidator<AddMaintainanceOrderPayload>
    {
        public AddMaintainanceOrderPayloadValidator(ICurrentUserService currentUser)
        {
            var lang = currentUser.Language;

            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<AddMaintainanceOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Name), ValidationTypesEnum.Required));

            RuleFor(x => x.RegionId)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<AddMaintainanceOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.RegionId), ValidationTypesEnum.Required));

            RuleFor(x => x.Lat)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<AddMaintainanceOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lat), ValidationTypesEnum.Required));

            RuleFor(x => x.Lng)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<AddMaintainanceOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lng), ValidationTypesEnum.Required));

            RuleFor(x => x.Location)
           .NotEmpty()
           .WithMessage(x => FluentValidationHelper.Message<AddMaintainanceOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Location), ValidationTypesEnum.Required));

            RuleFor(x => x.Area)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<AddMaintainanceOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Area), ValidationTypesEnum.Required));

            RuleFor(x => x.Image)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<AddMaintainanceOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Image), ValidationTypesEnum.Required));
            
            RuleFor(x => x.PaymentMethod)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<AddMaintainanceOrderPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PaymentMethod), ValidationTypesEnum.Required));
        }

    }
}
