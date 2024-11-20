using AutoMapper;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.IO;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.SEC;
using Wasit.Services.DTOs.Schema.SEC.Broker;
using Wasit.Services.DTOs.Schema.SEC.Delegate;
using Wasit.Services.DTOs.Schema.SEC.Developer;
using Wasit.Services.DTOs.Schema.SEC.Owner;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapSecurity(ICurrentUserService currentUserService)
        {
            CreateMap<OwnerRegisterDTO, ApplicationDbUser>()
                .ForMember(dest => dest.ImgProfile, opt => opt.MapFrom(x => x.ImgProfile != null ? IOHelper.Upload(x.ImgProfile, (int)FileName.Users) : "images/Users/default.jpg"))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(x => Enum.GetName(typeof(UserType), UserType.Owner)))
                .ForMember(des => des.User_Name, opt => opt.MapFrom(x => x.UserName))
                .ForMember(des => des.Lang, opt => opt.MapFrom(x => currentUserService.Language))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.PhoneNumber + "@Wasit.com"));

            CreateMap<BrokerRegisterDTO, ApplicationDbUser>()
                .ForMember(dest => dest.ImgProfile, opt => opt.MapFrom(x => x.ImgProfile != null ? IOHelper.Upload(x.ImgProfile, (int)FileName.Users) : "images/Users/default.jpg"))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(x => Enum.GetName(typeof(UserType), UserType.Broker)))
                .ForMember(des => des.User_Name, opt => opt.MapFrom(x => x.UserName))
                .ForMember(des => des.Lang, opt => opt.MapFrom(x => currentUserService.Language))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.PhoneNumber + "@Wasit.com"));

            CreateMap<DelegateRegisterDTO, ApplicationDbUser>()
                .ForMember(dest => dest.ImgProfile, opt => opt.MapFrom(x => x.ImgProfile != null ? IOHelper.Upload(x.ImgProfile, (int)FileName.Users) : "images/Users/default.jpg"))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(x => Enum.GetName(typeof(UserType), UserType.Delegate)))
                .ForMember(des => des.User_Name, opt => opt.MapFrom(x => x.UserName))
                .ForMember(des => des.Lang, opt => opt.MapFrom(x => currentUserService.Language))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.PhoneNumber + "@Wasit.com"));

            CreateMap<DeveloperRegisterDTO, ApplicationDbUser>()
                .ForMember(dest => dest.ImgProfile, opt => opt.MapFrom(x => x.ImgProfile != null ? IOHelper.Upload(x.ImgProfile, (int)FileName.Users) : "images/Users/default.jpg"))
                .ForMember(dest => dest.CoverPhoto, opt => opt.MapFrom(x => IOHelper.Upload(x.CoverPhoto, (int)FileName.CoverPhotos)))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(x => Enum.GetName(typeof(UserType), UserType.Developer)))
                .ForMember(des => des.User_Name, opt => opt.MapFrom(x => x.UserName))
                .ForMember(des => des.Lang, opt => opt.MapFrom(x => currentUserService.Language))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.PhoneNumber + "@Wasit.com"));

            CreateMap<UserRegisterDTO, ApplicationDbUser>()
                 .ForMember(dest => dest.DeviceIds, opt => opt.Ignore());

            CreateMap<ApplicationDbUser, UserInfo>();
            CreateMap<ApplicationDbUser, UserData>();

            CreateMap<ApplicationDbUser, UserProfileDto>()
                .IncludeAllDerived()
                .ForMember(dest => dest.IdentityNumber, opt => opt.MapFrom(src => src.IDNumber))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.ImgProfile}"))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Region.CityId))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => currentUserService.IsArabic ? src.Region.City.NameAr : src.Region.City.NameEn))
                .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => currentUserService.IsArabic ? src.Region.NameAr : src.Region.NameEn))
                .ForMember(dest => dest.AccountOwnerName, opt => opt.MapFrom(src => src.AccOwnerName))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccNumber))
                .ForMember(dest => dest.Iban, opt => opt.MapFrom(src => src.IbanNumber));
            CreateMap<ApplicationDbUser, OwnerInfoDto>();
            CreateMap<ApplicationDbUser, IndividualBrokerInfoDto>();
            CreateMap<ApplicationDbUser, FacilityBrokerInfoDto>();
            CreateMap<ApplicationDbUser, DelegateInfoDto>();
            CreateMap<ApplicationDbUser, DeveloperInfoDto>();


            CreateMap<OwnerUpdateDto, ApplicationDbUser>()
                .ForMember(dest => dest.ImgProfile, opt => opt.Ignore())
                .ForMember(des => des.User_Name, opt => opt.MapFrom(x => x.UserName));
            
            CreateMap<BrokerUpdateDto, ApplicationDbUser>()
                .ForMember(dest => dest.ImgProfile, opt => opt.Ignore())
                .ForMember(des => des.User_Name, opt => opt.MapFrom(x => x.UserName));
            
            CreateMap<DelegateUpdateDto, ApplicationDbUser>()
                .ForMember(dest => dest.ImgProfile, opt => opt.Ignore())
                .ForMember(des => des.User_Name, opt => opt.MapFrom(x => x.UserName));
            
            CreateMap<DeveloperUpdateDto, ApplicationDbUser>()
                .ForMember(dest => dest.ImgProfile, opt => opt.Ignore())
                .ForMember(dest => dest.CoverPhoto, opt => opt.Ignore())
                .ForMember(des => des.User_Name, opt => opt.MapFrom(x => x.UserName));

            CreateMap<UserLoginDto, DeviceId>()
                .ForMember(dest => dest.DeviceId_, opt => opt.MapFrom(x => x.DeviceId))
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom((src, dest, destMember, context) => context.Items[nameof(DeviceId.UserId)]));
        }



    }
}
