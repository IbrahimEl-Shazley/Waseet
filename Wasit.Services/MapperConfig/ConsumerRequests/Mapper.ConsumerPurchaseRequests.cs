using AutoMapper;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Sale.PurchaseRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapConsumerPurchaseRequests(ICurrentUserService currentUser)
        {
            CreateMap<PurchaseRequest, ConsumerPurchaseCategoryRequestDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SaleEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.SaleEstate.EstatePrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.SaleEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.SaleEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.SaleEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                    // .ForMember(dest => dest.EstateId, opt => opt.MapFrom(src => src.SaleEstateId))
                    //.ForMember(dest => dest.PurchaseInfo, opt => opt.MapFrom(src => new ConsumerPurchaseRequestInfoDto
                    //{
                    //    Deposit = src.SaleEstate.Deposit,
                    //    IsPaid = src.IsPay,
                    //    HasRefundRequest = src.HasRefundRequest 
                    //}))
                    .ForMember(dest => dest.Type, opt => opt.Ignore());
            //.ForMember(dest => dest.RatingInfo, opt => opt.Ignore())
            //.ForMember(dest => dest.ReservationInfo, opt => opt.Ignore());
            CreateMap<PurchaseRequest, ExtendedConsumerPurchaseRequestInfoDto>()
                    .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.SaleEstate))
                    .ForMember(dest => dest.Deposit, opt => opt.MapFrom(src => src.SaleEstate.Deposit))
                    .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPay))
                    .ForMember(dest => dest.HasRefundRequest, opt => opt.MapFrom(src => src.HasRefundRequest))
                    .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => src.SaleEstate.FinalEstatePrice));


            CreateMap<SaleRatingRequest, ConsumerPurchaseCategoryRequestDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SaleEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.SaleEstate.EstatePrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.SaleEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.SaleEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.SaleEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                   // .ForMember(dest => dest.EstateId, opt => opt.MapFrom(src => src.SaleEstateId))
                   //.ForMember(dest => dest.RatingInfo, opt => opt.MapFrom(src => new ConsumerRatingRequestInfoForSaleEstateDto
                   //{
                   //    EvaluationPrice = src.ServiceCost,
                   //    DelegateName = src.Provider == null ? currentUser.Language == Language.Ar ? "لم يحدد بعد" : "Not assigned yet" : src.Provider.User_Name,
                   //    DelegatePhone = src.Provider == null ? null : src.Provider.PhoneNumber,
                   //    DelegateReport = src.ReportUrl == null ? null : MyConstants.DomainUrl + src.ReportUrl,
                   //    IsRated = src.UserRate > 0
                   //}))
                    //.ForMember(dest => dest.PurchaseInfo, opt => opt.Ignore())
                    //.ForMember(dest => dest.ReservationInfo, opt => opt.Ignore())
                    .ForMember(dest => dest.Type, opt => opt.Ignore());
            CreateMap<SaleRatingRequest, ExtendedConsumerRatingRequestInfoForSaleEstateDto>()
                    .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.SaleEstate))
                    .ForMember(dest => dest.EvaluationPrice, opt => opt.MapFrom(src => src.ServiceCost))
                    .ForMember(dest => dest.DelegateName, opt => opt.MapFrom(src => src.Provider == null ? currentUser.Language == Language.Ar ? "لم يحدد بعد" : "Not assigned yet" : src.Provider.User_Name))
                    .ForMember(dest => dest.DelegatePhone, opt => opt.MapFrom(src => src.Provider == null ? null : src.Provider.PhoneNumber))
                    .ForMember(dest => dest.IsRated, opt => opt.MapFrom(src => src.UserRating > 0));


            CreateMap<SaleReservationRequest, ConsumerPurchaseCategoryRequestDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SaleEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.SaleEstate.EstatePrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.SaleEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.SaleEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.SaleEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                   // .ForMember(dest => dest.EstateId, opt => opt.MapFrom(src => src.SaleEstateId))
                   //.ForMember(dest => dest.ReservationInfo, opt => opt.MapFrom(src => new ConsumerReservationRequestInfoForSaleEstateDto
                   //{
                   //    DateTime = src.ReservationDate.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                   //}))
                   // .ForMember(dest => dest.PurchaseInfo, opt => opt.Ignore())
                   // .ForMember(dest => dest.RatingInfo, opt => opt.Ignore())
                    .ForMember(dest => dest.Type, opt => opt.Ignore());
            CreateMap<SaleReservationRequest, ExtendedConsumerReservationRequestInfoForSaleEstateDto>()
                    .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.SaleEstate))
                    .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.ReservationDate.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")));

        }
    }
}
