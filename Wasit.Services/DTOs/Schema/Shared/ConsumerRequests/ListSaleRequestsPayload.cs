﻿using FluentValidation;
using System.ComponentModel;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Validation;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;

namespace Wasit.Services.DTOs.Schema.Shared.ConsumerRequests
{
    public class ListSaleRequestsPayload : BaseListRequestsPayload
    {
        [DisplayName("نوع الطلب")]
        public SaleRequestType RequestType { get; set; }
    }

    public class SaleRequestsPayloadValidator : AbstractValidator<ListSaleRequestsPayload>
    {
        public SaleRequestsPayloadValidator(ICurrentUserService currentUser)
        {
            var lang = currentUser.Language;

            RuleFor(x => x.PageNumber)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<ListSaleRequestsPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.PageNumber), ValidationTypesEnum.Required));

            RuleFor(x => x.RequestType)
                .NotEmpty()
                .WithMessage(x => FluentValidationHelper.Message<ListSaleRequestsPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.RequestType), ValidationTypesEnum.Required));

            RuleFor(x => x.Status)
                .Must(x => x == 1 || x == 2 || x == 3)
                .When(x => x.RequestType == SaleRequestType.PurchaseRequest || x.RequestType == SaleRequestType.RatingRequest)
                .WithMessage(x => FluentValidationHelper.Message<ListSaleRequestsPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Status), ValidationTypesEnum.Status3));

            RuleFor(x => x.Status)
                .Must(x => x == 2 || x == 3)
                .When(x => x.RequestType == SaleRequestType.ReservationRequest)
                .WithMessage(x => FluentValidationHelper.Message<ListSaleRequestsPayload>(lang, MyConstants.ValidationLocalizationPath, nameof(x.Status), ValidationTypesEnum.Status2));
        }
    }
}
