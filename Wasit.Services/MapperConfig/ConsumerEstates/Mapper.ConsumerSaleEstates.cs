using AAITHelper;
using AutoMapper;
using Wasit.Core.Entities.AddPriceToEstate;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;
using Wasit.Services.DTOs.Schema.Sale.UserPriceToEstate;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapConsumerSaleEstates(ICurrentUserService currentUser)
        {
            CreateMap<SaleEstate, ConsumerSaleEstateInfoDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EstateName))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.EstateNumber))
                .ForMember(dest => dest.Addresss, opt => opt.MapFrom(src => src.Address(currentUser.Language)))
                .ForMember(dest => dest.EstateType, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EstateType.NameAr : src.EstateType.NameEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.EstateDescription))
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.EstateFeatures))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.EstatePrice))
                .ForMember(dest => dest.PricingValue, opt => opt.MapFrom(src => src.PricingRequests.Any(x => x.IsPay) ? src.PricingRequests.LastOrDefault(x => x.IsPay && x.AdminPrice > 0).AdminPrice : 0))
                .ForMember(dest => dest.IsFavourite, opt => opt.MapFrom(src => src.Favorites.Any(x => x.UserId == currentUser.UserId && x.SaleEstateId == src.Id)))
                .ForMember(dest => dest.HasAddedPriceBefore, opt => opt.MapFrom(src => src.UserPriceToEstates.Any(x => x.UserId == currentUser.UserId && x.SaleEstateId == src.Id)))
                .ForMember(dest => dest.AveragePrice, opt => opt.MapFrom(src => src.UserPriceToEstates.Count != 0 ? src.UserPriceToEstates.Average(p => p.Price) : 0))
                .ForMember(dest => dest.PublisherInfo, opt => opt.MapFrom(src => new PublisherInfoDto
                {
                    Id = src.User.Id,
                    Name = src.User.User_Name,
                    ProfilePicture = MyConstants.DomainUrl + src.User.ImgProfile,
                    Type = EnumHelper.UserTypeName(src.User.UserType, currentUser.Language),
                    Rating = src.User.Rating,
                    IsVerified = src.User.IsVerified,
                    Phone = string.Equals(src.User.UserType, "Owner") ? string.Empty : src.User.PhoneNumber
                }))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => new BaseImageItemDto
                {
                    Id = i.Id,
                    Url = MyConstants.DomainUrl + i.Image
                }).ToHashSet()))
                .ForMember(dest => dest.Specs, opt => opt.MapFrom(src => src.SpecificationValues.Select(s => new SpecificationItemDto
                {
                    Id = s.Id,
                    Name = $"{s.EstateTypeSpecification.Specification.Name(currentUser.Language)}: {s.SpecificationValue}"
                })));


            CreateMap<P4AddPriceToEstate, AddPricePackageItemDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => currentUser.IsArabic ? src.NameAr : src.NameEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => currentUser.IsArabic ? src.DescriptionAr : src.DescriptionEn))
                .ForMember(dest => dest.Period, opt => opt.MapFrom(src => src.SubscriptionDays));


            CreateMap<P4AddPriceToEstate, S4AddPriceToEstate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PNameAr, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.PNameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.PSubscriptionDays, opt => opt.MapFrom(src => src.SubscriptionDays))
                .ForMember(dest => dest.SubscriptionDate, opt => opt.MapFrom(src => HelperDate.GetCurrentDate(3)))
                .ForMember(dest => dest.PDescriptionAr, opt => opt.MapFrom(src => src.DescriptionAr))
                .ForMember(dest => dest.PDescriptionEn, opt => opt.MapFrom(src => src.DescriptionEn))
                .ForMember(dest => dest.AddPriceCount, opt => opt.MapFrom(src => src.AddPriceCount))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.RemainingAddPriceCount, opt => opt.MapFrom(src => src.AddPriceCount))
                .ForMember(dest => dest.ExpireDate, opt => opt.MapFrom(src => HelperDate.GetCurrentDate(3).AddDays(src.SubscriptionDays)))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => currentUser.UserId))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

        }
    }
}
