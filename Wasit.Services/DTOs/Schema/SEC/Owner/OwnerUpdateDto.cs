using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.SEC.Owner
{
    public class OwnerUpdateDto
    {
        [DisplayName("الصورة الشخصيه")]
        public IFormFile? ImgProfile { get; set; }

        [DisplayName("اسم المستخدم")]
        public string UserName { get; set; }

        [DisplayName("رقم الهوية الوطنيه")]
        public string IDNumber { get; set; }

        [DisplayName("الحي")]
        public int RegionId { get; set; }

        [DisplayName("خط العرض")]
        public string Lat { get; set; }

        [DisplayName("خط الطول")]
        public string Lng { get; set; }

        [DisplayName("الموقع")]
        public string Location { get; set; }

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


    public class OwnerUpdateDtoValidator : AbstractValidator<OwnerUpdateDto>
    {

        public OwnerUpdateDtoValidator(ICurrentUserService currentUserService)
        {
            var lang = currentUserService.Language;

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.UserName), ValidationTypesEnum.Required));

            RuleFor(x => x.IDNumber)
           .NotEmpty()
           .WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IDNumber), ValidationTypesEnum.Required))
           .Matches("^[1|2]{1}[0-9]{9}$")
           .WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IDNumber), ValidationTypesEnum.IdentityNumber));

            RuleFor(x => x.RegionId)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.RegionId), ValidationTypesEnum.Required));

            RuleFor(x => x.Lat)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lat), ValidationTypesEnum.Required));

            RuleFor(x => x.Lng)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lng), ValidationTypesEnum.Required));

            RuleFor(x => x.Location)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Location), ValidationTypesEnum.Required));

            RuleFor(x => x.BankName)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.BankName), ValidationTypesEnum.Required));

            RuleFor(x => x.AccOwnerName)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.AccOwnerName), ValidationTypesEnum.Required));

            RuleFor(x => x.AccNumber)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.AccNumber), ValidationTypesEnum.Required));

            RuleFor(x => x.IbanNumber)
           .NotEmpty()
           .WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IbanNumber), ValidationTypesEnum.Required))
           .Matches(@"^SA[A-Z0-9]{22}$")
           .WithMessage(x => FluentValidationHelper.Message<OwnerUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IbanNumber), ValidationTypesEnum.IBAN));
        }
    }
}
