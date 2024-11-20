using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Wasit.Core.Constants;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.SEC.Delegate;

namespace Wasit.Services.DTOs.Schema.SEC
{
    public class DeveloperRegisterDTO
    {
        [DisplayName("الصورة الشخصيه")]
        public IFormFile? ImgProfile { get; set; }

        [DisplayName("اسم المستخدم")]
        public string UserName { get; set; }

        [DisplayName("رقم الجوال")]
        public string PhoneNumber { get; set; }

        [DisplayName("البريد الالكتروني")]
        public string Email { get; set; }


        [DisplayName("الحي")]
        public int RegionId { get; set; }

        [DisplayName("خط العرض")]
        public string Lat { get; set; }
        [DisplayName("خط الطول")]
        public string Lng { get; set; }
        [DisplayName("الموقع")]
        public string Location { get; set; }

        [DisplayName("صورة الغلاف")]
        public IFormFile CoverPhoto { get; set; }

        [DisplayName("الوصف")]
        public string Description { get; set; }
        //البيانات الخاصه بالحساب البنكى
        [DisplayName("اسم البنك")]
        public string BankName { get; set; }
        [DisplayName("اسم صاحب الحساب")]
        public string AccOwnerName { get; set; }
        [DisplayName("رقم الحساب")]
        public string AccNumber { get; set; }
        [DisplayName("رقم الايبان")]
        public string IbanNumber { get; set; }


        [DisplayName("الرقم السري")]
        public string Password { get; set; }

    }




    public class DeveloperRegisterValidator : AbstractValidator<DeveloperRegisterDTO>
    {

        public DeveloperRegisterValidator(ICurrentUserService currentUserService)
        {
            var lang = currentUserService.Language;

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.UserName), ValidationTypesEnum.Required));

            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PhoneNumber), ValidationTypesEnum.Required))
            .Matches(RegEx.SaudiPhone)
            .WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PhoneNumber), ValidationTypesEnum.PhoneNumber));

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Email), ValidationTypesEnum.Required))
            //.MinimumLength(5).WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Email), ValidationTypesEnum.MinLength, 5))
            //.MaximumLength(50).WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Email), ValidationTypesEnum.MaxLength, 50))
            .Matches(RegEx.Email)
            .EmailAddress().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Email), ValidationTypesEnum.Email));

            RuleFor(x => x.RegionId)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.RegionId), ValidationTypesEnum.Required));

            RuleFor(x => x.Lat)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lat), ValidationTypesEnum.Required));

            RuleFor(x => x.Lng)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lng), ValidationTypesEnum.Required));

            RuleFor(x => x.Location)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Location), ValidationTypesEnum.Required));

            RuleFor(x => x.CoverPhoto)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.CoverPhoto), ValidationTypesEnum.Required));

            RuleFor(x => x.Description)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Description), ValidationTypesEnum.Required));

            RuleFor(x => x.BankName)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.BankName), ValidationTypesEnum.Required));

            RuleFor(x => x.AccOwnerName)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.AccOwnerName), ValidationTypesEnum.Required));

            RuleFor(x => x.AccNumber)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.AccNumber), ValidationTypesEnum.Required))
           .Matches(RegEx.SaudiBankAccountNo)
           .WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.AccNumber), ValidationTypesEnum.BankAccountNo));

            RuleFor(x => x.IbanNumber)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IbanNumber), ValidationTypesEnum.Required))
           .Matches(RegEx.IBAN)
           .WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IbanNumber), ValidationTypesEnum.IBAN));

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Password), ValidationTypesEnum.Required))
            .MinimumLength(6).WithMessage(x => FluentValidationHelper.Message<DeveloperRegisterDTO>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Password), ValidationTypesEnum.MinLength, 6));

        }
    }
}
