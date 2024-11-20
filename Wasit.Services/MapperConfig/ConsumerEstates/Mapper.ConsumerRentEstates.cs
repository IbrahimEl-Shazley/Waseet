using AutoMapper;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapConsumerRentEstates(ICurrentUserService currentUser)
        {
            CreateMap<RentEstate, ConsumerRentEstateInfoDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EstateName))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.EstateNumber))
                .ForMember(dest => dest.Addresss, opt => opt.MapFrom(src => src.Address(currentUser.Language)))
                .ForMember(dest => dest.EstateType, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EstateType.NameAr : src.EstateType.NameEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.EstateDescription))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.MonthRentPrice))
                .ForMember(dest => dest.IsFavourite, opt => opt.MapFrom(src => src.Favorites.Any(x => x.UserId == currentUser.UserId && x.RentEstateId == src.Id)))

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


            ///////////////////////////////////////////////////

          
        }
    }
}
