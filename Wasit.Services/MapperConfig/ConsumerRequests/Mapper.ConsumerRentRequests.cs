using AutoMapper;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Rent.RentRatingRequest;
using Wasit.Services.DTOs.Schema.Rent.RentRequest;
using Wasit.Services.DTOs.Schema.Rent.RentReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapConsumerRentRequests(ICurrentUserService currentUser)
        {
            CreateMap<RentRequest, ConsumerRentCategoryRequestDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RentEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.RentEstate.MonthRentPrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.RentEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.RentEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.RentEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                    //.ForMember(dest => dest.EstateId, opt => opt.MapFrom(src => src.RentEstateId))
                    //.ForMember(dest => dest.RentInfo, opt => opt.MapFrom(src => new ConsumerRentRequestInfoDto
                    //{
                    //    Months = src.MonthCount,
                    //    Years = src.YearCount,
                    //    RentPrice = src.TotalPrice,
                    //    OwnerName = src.RentEstate.User.User_Name,
                    //    PaidAmount = src.RentEstate.PaidRentPrice,
                    //    IsPaid = src.IsPay,
                    //    IsRated = src.UserRate > 0,
                    //    Status = EnumHelper.RentRequestStatus(src, currentUser.Language)
                    //}))
                    .ForMember(dest => dest.Type, opt => opt.Ignore());
            //.ForMember(dest => dest.RatingInfo, opt => opt.Ignore())
            //.ForMember(dest => dest.ReservationInfo, opt => opt.Ignore());
            CreateMap<RentRequest, ExtendedConsumerRentRequestInfoDto>()
                    .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.RentEstate))
                    .ForMember(dest => dest.Months, opt => opt.MapFrom(src => src.MonthCount))
                    .ForMember(dest => dest.Years, opt => opt.MapFrom(src => src.YearCount))
                    .ForMember(dest => dest.RentPrice, opt => opt.MapFrom(src => src.TotalPrice))
                    .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.RentEstate.User.User_Name))
                    .ForMember(dest => dest.PaidAmount, opt => opt.MapFrom(src => src.RentEstate.PaidRentPrice))
                    .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPay))
                    .ForMember(dest => dest.IsRated, opt => opt.MapFrom(src => src.OwnerRating > 0))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => EnumHelper.RentRequestStatus(src, currentUser.Language)));


            CreateMap<RentRatingRequest, ConsumerRentCategoryRequestDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RentEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.RentEstate.MonthRentPrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.RentEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.RentEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.RentEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                    //.ForMember(dest => dest.EstateId, opt => opt.MapFrom(src => src.RentEstateId))
                    //.ForMember(dest => dest.RatingInfo, opt => opt.MapFrom(src => new ConsumerRatingRequestInfoForRentEstateDto
                    //{
                    //    Price = src.ServiceCost,
                    //    DelegateName = src.Provider == null ? currentUser.Language == Language.Ar ? "لم يحدد بعد" : "Not assigned yet" : src.Provider.User_Name,
                    //    DelegatePhone = src.Provider == null ? null : src.Provider.PhoneNumber,
                    //    DelegateReport = src.ReportUrl == null ? null : MyConstants.DomainUrl + src.ReportUrl,
                    //    IsRated = src.UserRate > 0
                    //}))
                    //.ForMember(dest => dest.RentInfo, opt => opt.Ignore())
                    //.ForMember(dest => dest.ReservationInfo, opt => opt.Ignore())
                    .ForMember(dest => dest.Type, opt => opt.Ignore());
            CreateMap<RentRatingRequest, ExtendedConsumerRatingRequestInfoForRentEstateDto>()
                    .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.RentEstate))
                    .ForMember(dest => dest.EvaluationPrice, opt => opt.MapFrom(src => src.ServiceCost))
                    .ForMember(dest => dest.DelegateName, opt => opt.MapFrom(src => src.Provider == null ? currentUser.Language == Language.Ar ? "لم يحدد بعد" : "Not assigned yet" : src.Provider.User_Name))
                    .ForMember(dest => dest.DelegatePhone, opt => opt.MapFrom(src => src.Provider == null ? null : src.Provider.PhoneNumber))
                    .ForMember(dest => dest.DelegateReport, opt => opt.MapFrom(src => src.ReportUrl == null ? null : MyConstants.DomainUrl + src.ReportUrl))
                    .ForMember(dest => dest.IsRated, opt => opt.MapFrom(src => src.ReviewerRating > 0));


            CreateMap<RentReservationRequest, ConsumerRentCategoryRequestDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RentEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.RentEstate.MonthRentPrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.RentEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.RentEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.RentEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                    //.ForMember(dest => dest.EstateId, opt => opt.MapFrom(src => src.RentEstateId))
                    //.ForMember(dest => dest.ReservationInfo, opt => opt.MapFrom(src => new ConsumerReservationRequestInfoForRentEstateDto
                    //{
                    //    DateTime = src.ReservationDate.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss")
                    //}))
                    //.ForMember(dest => dest.RentInfo, opt => opt.Ignore())
                    //.ForMember(dest => dest.RatingInfo, opt => opt.Ignore())
                    .ForMember(dest => dest.Type, opt => opt.Ignore());
            CreateMap<RentReservationRequest, ExtendedConsumerReservationRequestInfoForRentEstateDto>()
                    .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.RentEstate))
                    .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.ReservationDate.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss")));

        }
    }
}
