using AutoMapper;
using System.Globalization;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapConsumerDailyRentRequests(ICurrentUserService currentUser)
        {
            var culture = DateTimeHelper.GetCulture(currentUser.Language);

            CreateMap<DailyRentRequest, ConsumerDailyRentCategoryRequestDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DailyRentEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.DailyRentEstate.DayRentPrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.DailyRentEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.DailyRentEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.DailyRentEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name));
                    //.ForMember(dest => dest.EstateId, opt => opt.MapFrom(src => src.DailyRentEstateId))
                    //.ForMember(dest => dest.Type, opt => opt.MapFrom(src => currentUser.IsArabic ? src.DailyRentEstate.EstateType.NameAr : src.DailyRentEstate.EstateType.NameEn))
                    //.ForMember(dest => dest.IsRated, opt => opt.MapFrom(src => src.UserRatingDateTime.HasValue))
                    //.ForMember(dest => dest.DaysCount, opt => opt.MapFrom(src => src.TotalDays))
                    //.ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate.ToString("d/M/yyyy")))
                    //.ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.LeaveDate.ToString("d/M/yyyy")))
                    //.ForMember(dest => dest.PublisherPhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                    //.ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.CreatedOn.Value.AddHours(24).ToString("yyyy-MM-dd hh:mm:ss")))
                    //.ForMember(dest => dest.CancelationDate, opt => opt.MapFrom(src => src.CancelDate.ToString("d/M/yyyy")))
                    //.ForMember(dest => dest.MyRating, opt => opt.MapFrom((src, dest, destMember, context) => src.UserRatingDateTime.HasValue ? new RatingInfoDto
                    //{
                    //    Id = src.Id,
                    //    Image = MyConstants.DomainUrl + src.User.ImgProfile,
                    //    Name = src.User.User_Name,
                    //    Rating = src.UserRating,
                    //    TimeSpan = DateTimeHelper.GetTimeSpan(src.UserRatingDateTime.Value, context.Items["culture"] as CultureInfo)
                    //} : null))
                    //.ForMember(dest => dest.OwnerRating, opt => opt.MapFrom((src, dest, destMember, context) => src.OwnerRatingDateTime.HasValue ? new RatingInfoDto
                    //{
                    //    Id = src.Id,
                    //    Image = MyConstants.DomainUrl + src.DailyRentEstate.User.ImgProfile,
                    //    Name = src.DailyRentEstate.User.User_Name,
                    //    Rating = src.OwnerRating,
                    //    TimeSpan = DateTimeHelper.GetTimeSpan(src.OwnerRatingDateTime.Value, context.Items["culture"] as CultureInfo)
                    //} : null));


            CreateMap<DailyRentRequest, ExtendedConsumerDailyRentCategoryRequestDto>()
                    .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.DailyRentEstate))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => currentUser.IsArabic ? src.DailyRentEstate.EstateType.NameAr : src.DailyRentEstate.EstateType.NameEn))
                    .ForMember(dest => dest.IsRated, opt => opt.MapFrom(src => src.UserRatingDateTime.HasValue))
                    .ForMember(dest => dest.DaysCount, opt => opt.MapFrom(src => src.TotalDays))
                    .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate.ToString("d/M/yyyy")))
                    .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.LeaveDate.ToString("d/M/yyyy")))
                    .ForMember(dest => dest.PublisherPhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                    .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.LeaveDate.ToString("yyyy-MM-dd HH:mm:ss")))
                    .ForMember(dest => dest.CancelationDate, opt => opt.MapFrom(src => src.CancelDate.ToString("d/M/yyyy")))
                    .ForMember(dest => dest.MyRating, opt => opt.MapFrom((src, dest, destMember, context) => src.UserRatingDateTime.HasValue ? new RatingInfoDto
                    {
                        Id = src.Id,
                        Image = MyConstants.DomainUrl + src.User.ImgProfile,
                        Name = src.User.User_Name,
                        Rating = src.UserRating,
                        TimeSpan = DateTimeHelper.GetTimeSpan(src.UserRatingDateTime.Value, context.Items["culture"] as CultureInfo)
                    } : null))
                    .ForMember(dest => dest.OwnerRating, opt => opt.MapFrom((src, dest, destMember, context) => src.OwnerRatingDateTime.HasValue ? new RatingInfoDto
                    {
                        Id = src.Id,
                        Image = MyConstants.DomainUrl + src.DailyRentEstate.User.ImgProfile,
                        Name = src.DailyRentEstate.User.User_Name,
                        Rating = src.OwnerRating,
                        TimeSpan = DateTimeHelper.GetTimeSpan(src.OwnerRatingDateTime.Value, context.Items["culture"] as CultureInfo)
                    } : null));

        }
    }
}
