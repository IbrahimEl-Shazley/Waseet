using AutoMapper;
using System.Globalization;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentRentRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapConsumerEntertainmentRequests(ICurrentUserService currentUser)
        {
            CreateMap<EntertainmentRequest, ConsumerEntertainmentCategoryRequestDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EntertainmentEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.EntertainmentEstate.DayRentPrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.EntertainmentEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.EntertainmentEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.EntertainmentEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name));
                    //.ForMember(dest => dest.EstateId, opt => opt.MapFrom(src => src.EntertainmentEstateId))
                    //.ForMember(dest => dest.Type, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EntertainmentEstate.EstateType.NameAr : src.EntertainmentEstate.EstateType.NameEn))
                    //.ForMember(dest => dest.DaysCount, opt => opt.MapFrom(src => src.TotalDays))
                    //.ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate.ToString("d/M/yyyy")))
                    //.ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.LeaveDate.ToString("d/M/yyyy")))
                    //.ForMember(dest => dest.PublisherPhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                    //.ForMember(dest => dest.IsRated, opt => opt.MapFrom(src => src.UserRatingDateTime.HasValue))
                    //.ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.ArrivalDate.AddDays(src.TotalDays).ToString("yyyy-MM-dd HH:mm:ss")))
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
                    //    Image = MyConstants.DomainUrl + src.EntertainmentEstate.User.ImgProfile,
                    //    Name = src.EntertainmentEstate.User.User_Name,
                    //    Rating = src.OwnerRating,
                    //    TimeSpan = DateTimeHelper.GetTimeSpan(src.OwnerRatingDateTime.Value, context.Items["culture"] as CultureInfo)
                    //} : null));


            CreateMap<EntertainmentRequest, ExtendedConsumerEntertainmentCategoryRequestDto>()
                    .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.EntertainmentEstate))
                     .ForMember(dest => dest.Type, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EntertainmentEstate.EstateType.NameAr : src.EntertainmentEstate.EstateType.NameEn))
                     .ForMember(dest => dest.DaysCount, opt => opt.MapFrom(src => src.TotalDays))
                     .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate.ToString("d/M/yyyy")))
                     .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.LeaveDate.ToString("d/M/yyyy")))
                     .ForMember(dest => dest.PublisherPhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                     .ForMember(dest => dest.IsRated, opt => opt.MapFrom(src => src.UserRatingDateTime.HasValue))
                     .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.LeaveDate.Date.AddHours(9).ToString("yyyy-MM-dd HH:mm:ss")))
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
                         Image = MyConstants.DomainUrl + src.EntertainmentEstate.User.ImgProfile,
                         Name = src.EntertainmentEstate.User.User_Name,
                         Rating = src.OwnerRating,
                         TimeSpan = DateTimeHelper.GetTimeSpan(src.OwnerRatingDateTime.Value, context.Items["culture"] as CultureInfo)
                     } : null));

        }
    }
}
