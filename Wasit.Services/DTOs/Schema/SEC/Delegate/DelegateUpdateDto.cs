﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Wasit.Core.Constants;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;

namespace Wasit.Services.DTOs.Schema.SEC.Delegate
{
    public class DelegateUpdateDto
    {
        [DisplayName("الصورة الشخصيه")]
        public IFormFile? ImgProfile { get; set; }

        [DisplayName("اسم المستخدم")]
        public string UserName { get; set; }

        [DisplayName("رقم الهوية الوطنية")]
        public string IDNumber { get; set; }

        [DisplayName("الحي")]
        public int RegionId { get; set; }

        [DisplayName("خط العرض")]
        public string Lat { get; set; }

        [DisplayName("خط الطول")]
        public string Lng { get; set; }

        [DisplayName("الموقع")]
        public string Location { get; set; }

        [DisplayName("الرقم الوظيفي")]
        public string WorkingNo { get; set; }

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



    public class DelegateUpdateDtoValidator : AbstractValidator<DelegateUpdateDto>
    {

        public DelegateUpdateDtoValidator(ICurrentUserService currentUserService)
        {
            var lang = currentUserService.Language;

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.UserName), ValidationTypesEnum.Required));

            RuleFor(x => x.IDNumber)
            .NotEmpty()
            .WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IDNumber), ValidationTypesEnum.Required))
            .Matches(RegEx.SaudiNationalID)
            .WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IDNumber), ValidationTypesEnum.IdentityNumber));

            RuleFor(x => x.RegionId)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.RegionId), ValidationTypesEnum.Required));

            RuleFor(x => x.Lat)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lat), ValidationTypesEnum.Required));

            RuleFor(x => x.Lng)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Lng), ValidationTypesEnum.Required));

            RuleFor(x => x.Location)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Location), ValidationTypesEnum.Required));

            RuleFor(x => x.WorkingNo)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.WorkingNo), ValidationTypesEnum.Required));

            RuleFor(x => x.BankName)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.BankName), ValidationTypesEnum.Required));

            RuleFor(x => x.AccOwnerName)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.AccOwnerName), ValidationTypesEnum.Required));

            RuleFor(x => x.AccNumber)
           .NotEmpty().WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.AccNumber), ValidationTypesEnum.Required))
           .Matches(RegEx.SaudiBankAccountNo)
           .WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.AccNumber), ValidationTypesEnum.BankAccountNo));

            RuleFor(x => x.IbanNumber)
           .NotEmpty()
           .WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IbanNumber), ValidationTypesEnum.Required))
           .Matches(RegEx.IBAN)
           .WithMessage(x => FluentValidationHelper.Message<DelegateUpdateDto>(lang, MyConstants.ValidationLocalizationPath, nameof(x.IbanNumber), ValidationTypesEnum.IBAN));
        }
    }
}
