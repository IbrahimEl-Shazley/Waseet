using AutoMapper;
using System.Globalization;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentEstate;
using Wasit.Services.DTOs.Schema.EstateCategories.EstateType;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapMySharedEstates(ICurrentUserService currentUser)
        {
            CreateMap<CategoryEstateType, EstateTypeDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EstateType.NameAr : src.EstateType.NameEn))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EstateTypeId));

            CreateMap<SaleEstate, SaleEstateDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.EstatePrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                    .ForMember(dest => dest.Type, opt => opt.Ignore());

            CreateMap<RentEstate, RentEstateDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.MonthRentPrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                    .ForMember(dest => dest.Type, opt => opt.Ignore());

            CreateMap<DailyRentEstate, DailyRentEstateDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.DayRentPrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EstateType.NameAr : src.EstateType.NameEn));

            CreateMap<EntertainmentEstate, EntertainmentEstateDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.DayRentPrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EstateType.NameAr : src.EstateType.NameEn));

            CreateMap<EstateTypeSpecification, SpecificationFormItemDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => currentUser.IsArabic ? src.Specification.NameAr : src.Specification.NameEn));

            CreateMap<SaleEstateSpecificationValue, SpecificationValueDto>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.SpecificationValue))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EstateTypeSpecificationId))
                .ForMember(dest => dest.IsRequired, opt => opt.MapFrom(src => src.EstateTypeSpecification.IsRequired))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EstateTypeSpecification.Specification.NameAr : src.EstateTypeSpecification.Specification.NameEn));

            CreateMap<RentEstateSpecificationValue, SpecificationValueDto>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.SpecificationValue))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EstateTypeSpecificationId))
                .ForMember(dest => dest.IsRequired, opt => opt.MapFrom(src => src.EstateTypeSpecification.IsRequired))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EstateTypeSpecification.Specification.NameAr : src.EstateTypeSpecification.Specification.NameEn));

            CreateMap<DailyRentEstateSpecificationValue, SpecificationValueDto>()
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.SpecificationValue))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EstateTypeSpecificationId))
               .ForMember(dest => dest.IsRequired, opt => opt.MapFrom(src => src.EstateTypeSpecification.IsRequired))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EstateTypeSpecification.Specification.NameAr : src.EstateTypeSpecification.Specification.NameEn));

            CreateMap<EntertainmentEstateSpecificationValue, SpecificationValueDto>()
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.SpecificationValue))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EstateTypeSpecificationId))
               .ForMember(dest => dest.IsRequired, opt => opt.MapFrom(src => src.EstateTypeSpecification.IsRequired))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EstateTypeSpecification.Specification.NameAr : src.EstateTypeSpecification.Specification.NameEn));

            CreateMap<EntertainmentRequest, BaseRatingItemDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.User_Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.User.ImgProfile}"))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.EstateRating))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.EstateComment))
                .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom((src, dest, destMember, context) =>
                DateTimeHelper.GetTimeSpan(src.UserRatingDateTime.Value, context.Items["culture"] as CultureInfo)));

            CreateMap<DailyRentRequest, BaseRatingItemDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.User_Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.User.ImgProfile}"))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.EstateRating))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.EstateComment))
                .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom((src, dest, destMember, context) =>
                DateTimeHelper.GetTimeSpan(src.UserRatingDateTime.Value, context.Items["culture"] as CultureInfo)));
        }

    }
}
