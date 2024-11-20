using AAITHelper;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Core.Helpers.IO;
using Wasit.Core.Models.DTO;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentRequest;
using Wasit.Services.DTOs.Schema.Shared;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapDailyRentEstates(ICurrentUserService currentUser)
        {
            CreateMap<CreateDailyRentEstateDto, DailyRentEstate>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => currentUser.UserId))
              .ForMember(dest => dest.EstateName, opt => opt.MapFrom(src => src.NameAr))
              .ForMember(dest => dest.EstateDescription, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.Developable, opt => opt.MapFrom(src => src.IsDevelopable))
              .ForMember(dest => dest.DayRentPrice, opt => opt.MapFrom(src => src.Price))
              .ForMember(dest => dest.EstateNumber, opt => opt.MapFrom(src => src.UniqueNumber))
              .ForMember(dest => dest.EstateFeatures, opt => opt.MapFrom(src => src.Features))
              .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => currentUser.UserId))
              .ForMember(dest => dest.IsShow, opt => opt.MapFrom(src => true))
              .ForMember(dest => dest.EstateArea, opt => opt.MapFrom(src => src.Area));

            CreateMap<IFormFile, DailyRentEstateImage>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => IOHelper.Upload(src, (int)FileName.Estates)));

            CreateMap<SpecificationKeyValueDto, DailyRentEstateSpecificationValue>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.EstateTypeSpecificationId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.SpecificationValue, opt => opt.MapFrom(src => src.Value));

            CreateMap<DailyRentEstate, DailyRentEstateInfoDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EstateName))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => new SharedDto
                {
                    Id = src.Region.CityId,
                    Name = currentUser.IsArabic ? src.Region.City.NameAr : src.Region.City.NameEn
                }))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => new SharedDto
                {
                    Id = src.RegionId,
                    Name = currentUser.IsArabic ? src.Region.NameAr : src.Region.NameEn
                }))
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.EstateFeatures))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.EstateNumber))
                .ForMember(dest => dest.IsVisible, opt => opt.MapFrom(src => src.IsShow))
                .ForMember(dest => dest.Addresss, opt => opt.MapFrom(src => src.Address(currentUser.Language)))
                .ForMember(dest => dest.EstateType, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EstateType.NameAr : src.EstateType.NameEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.EstateDescription))
                .ForMember(dest => dest.IsDevelopable, opt => opt.MapFrom(src => src.Developable))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.DayRentPrice))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.EstateArea))
                .ForMember(dest => dest.RequestsCount, opt => opt.MapFrom(src => src.Requests.Count()))
                .ForMember(dest => dest.Ratings, opt => opt.MapFrom((src, dest, destMember, context) => src.Requests
                .Where(r => r.UserRatingDateTime != null)
                .OrderByDescending(r => r.UserRatingDateTime)
                .Select(r => new BaseRatingItemDto
                {
                    Id = r.Id,
                    Name = r.User.User_Name,
                    Image = MyConstants.DomainUrl + r.User.ImgProfile,
                    Comment = r.EstateComment,
                    Rating = r.EstateRating,
                    TimeSpan = DateTimeHelper.GetTimeSpan(r.UserRatingDateTime.Value, context.Items["culture"] as CultureInfo)
                }).Take(3).ToHashSet()))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => new BaseImageItemDto
                {
                    Id = i.Id,
                    Url = MyConstants.DomainUrl + i.Image
                }).ToHashSet()))
                .ForMember(dest => dest.Specs, opt => opt.MapFrom(src => src.SpecificationValues.Select(s => new SpecificationItemDto
                {
                    Id = s.Id,
                    Name = $"{s.EstateTypeSpecification.Specification.Name(currentUser.Language)}: {s.SpecificationValue}",
                    Icon = $"{MyConstants.DomainUrl}{s.EstateTypeSpecification.Specification.Icon}"
                })));


            CreateMap<UpdateDailyRentEstateDto, DailyRentEstate>()
              .ForMember(dest => dest.EstateName, opt => opt.MapFrom(src => src.NameAr))
              .ForMember(dest => dest.EstateDescription, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.Developable, opt => opt.MapFrom(src => src.IsDevelopable))
              .ForMember(dest => dest.DayRentPrice, opt => opt.MapFrom(src => src.Price))
              .ForMember(dest => dest.EstateFeatures, opt => opt.MapFrom(src => src.Features))
              .ForMember(dest => dest.IsShow, opt => opt.Ignore())
              .ForMember(dest => dest.EstateArea, opt => opt.MapFrom(src => src.Area))
              .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => HelperDate.GetCurrentDate(3)))
              .ForMember(dest => dest.UpdatedById, opt => opt.MapFrom(src => currentUser.UserId))
              .ForMember(dest => dest.Images, opt => opt.Ignore())
              .ForMember(dest => dest.SpecificationValues, opt => opt.Ignore());

            CreateMap<SpecificationKeyValueDto, DailyRentEstateSpecificationValue>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.EstateTypeSpecificationId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.SpecificationValue, opt => opt.MapFrom(src => src.Value));


            CreateMap<DailyRentRequest, DailyRentRequestDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.User_Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => string.Empty));
            CreateMap<PageDTO<DailyRentRequest>, PageDTO<DailyRentRequestDto>>()
                 .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

            CreateMap<DailyRentRequest, DailyRentRequestInfoDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.DailyRentEstate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => string.Empty))
                .ForMember(dest => dest.StatusEnum, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => EnumHelper.EntertainmentRentRequestStatus(src.Status, currentUser.Language)))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.User_Name))
                .ForMember(dest => dest.DaysCount, opt => opt.MapFrom(src => src.TotalDays))
                .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.LeaveDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.IsRated, opt => opt.MapFrom(src => src.OwnerRating > 0))
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.LeaveDate.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dest => dest.UserRating, opt => opt.MapFrom((src, dest, destMember, context) => src.UserRatingDateTime.HasValue ? new RatingInfoDto
                {
                    Id = src.Id,
                    Image = MyConstants.DomainUrl + src.User.ImgProfile,
                    Name = src.User.User_Name,
                    Rating = src.UserRating,
                    TimeSpan = DateTimeHelper.GetTimeSpan(src.UserRatingDateTime.Value, context.Items["culture"] as CultureInfo)
                } : null));
        }
    }
}
