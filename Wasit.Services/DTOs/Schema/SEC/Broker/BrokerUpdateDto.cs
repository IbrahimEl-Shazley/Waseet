using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Wasit.Core.Constants;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.SEC.Broker
{
    public class BrokerUpdateDto
    {
        [DisplayName("الصورة الشخصيه")]
        public IFormFile? ImgProfile { get; set; }

        [DisplayName("اسم المستخدم")]
        public string UserName { get; set; }

        [DisplayName("رقم الهويةالوطنيه")]
        public string IDNumber { get; set; }

        [DisplayName("الحي")]
        public int RegionId { get; set; }

        [DisplayName("خط العرض")]
        public string Lat { get; set; }

        [DisplayName("خط الطول")]
        public string Lng { get; set; }

        [DisplayName("الموقع")]
        public string Location { get; set; }

        [DisplayName("نوع المنشأه")]
        public FacilityType? FacilityType { get; set; }

        [DisplayName("رقم وثيقة الوساطه")]
        public string BrokerageDocumentNo { get; set; }

        [DisplayName("رقم السجل التجاري")]
        public string? CommercialNo { get; set; }

        [DisplayName("رقم الترخيص")]
        public string? LicenseNo { get; set; }

        //البيانات الخاصه بالحساب البنكى
        [DisplayName("اسم البنك")]
        public string BankName { get; set; }

        [DisplayName("اسم صاحب الحساب")]
        public string AccOwnerName { get; set; }

        [DisplayName("رقم الحساب")]
        public string AccNumber { get; set; }

        [DisplayName("رقم الايبان")]
        public string IbanNumber { get; set; }
    }


    public class BrokerUpdateDtoValidator : AbstractValidator<BrokerUpdateDto>
    {
        public BrokerUpdateDtoValidator(ICurrentUserService currentUserService)
        {
            var lang = currentUserService.Language;

            RuleFor(x => x.IDNumber)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IDNumber), ValidationTypesEnum.Required))
            .Matches(RegEx.SaudiNationalID)
            .WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IDNumber), ValidationTypesEnum.IdentityNumber));

            RuleFor(x => x.RegionId)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.RegionId), ValidationTypesEnum.Required));

            RuleFor(x => x.Lat)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lat), ValidationTypesEnum.Required));

            RuleFor(x => x.Lng)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lng), ValidationTypesEnum.Required));

            RuleFor(x => x.Location)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Location), ValidationTypesEnum.Required));

            RuleFor(x => x.BrokerageDocumentNo)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.BrokerageDocumentNo), ValidationTypesEnum.Required));

            RuleFor(x => x.BankName)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.BankName), ValidationTypesEnum.Required));

            RuleFor(x => x.AccOwnerName)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.AccOwnerName), ValidationTypesEnum.Required));

            RuleFor(x => x.AccNumber)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.AccNumber), ValidationTypesEnum.Required));

            RuleFor(x => x.IbanNumber)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IbanNumber), ValidationTypesEnum.Required))
            .Matches(RegEx.IBAN)
            .WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IbanNumber), ValidationTypesEnum.IBAN));

            RuleFor(x => x.FacilityType)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.FacilityType), ValidationTypesEnum.Required));

            RuleFor(x => x.LicenseNo)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.LicenseNo), ValidationTypesEnum.Required));

            RuleFor(x => x.CommercialNo)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.CommercialNo), ValidationTypesEnum.Required))
                .Matches(RegEx.SaudiCommercialNo)
                .WithMessage(x => FluentValidationHelper.Message<BrokerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.CommercialNo), ValidationTypesEnum.CommercialNo));
        }
    }
}
