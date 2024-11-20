using AutoMapper;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Helpers;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentEstate;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapConsumerSharedEstates(ICurrentUserService currentUser)
        {
            CreateMap<SaleEstateFavorite, SaleEstateDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SaleEstateId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SaleEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.SaleEstate.EstatePrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.SaleEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.SaleEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.SaleEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.SaleEstate.User.User_Name))
                    .ForMember(dest => dest.Type, opt => opt.Ignore());

            CreateMap<RentEstateFavorite, RentEstateDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RentEstateId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RentEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.RentEstate.MonthRentPrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.RentEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.RentEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.RentEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.RentEstate.User.User_Name))
                    .ForMember(dest => dest.Type, opt => opt.Ignore());

            CreateMap<DailyRentEstateFavorite, DailyRentEstateDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DailyRentEstateId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DailyRentEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.DailyRentEstate.DayRentPrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.DailyRentEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.DailyRentEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.DailyRentEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => currentUser.IsArabic ? src.DailyRentEstate.EstateType.NameAr : src.DailyRentEstate.EstateType.NameEn));

            CreateMap<EntertainmentEstateFavorite, EntertainmentEstateDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EntertainmentEstateId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EntertainmentEstate.EstateName))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.EntertainmentEstate.DayRentPrice))
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{MyConstants.DomainUrl}{src.EntertainmentEstate.Images.FirstOrDefault().Image}"))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.EntertainmentEstate.EstateNumber))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.EntertainmentEstate.Address(currentUser.Language)))
                    .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User.User_Name))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EntertainmentEstate.EstateType.NameAr : src.EntertainmentEstate.EstateType.NameEn));
        }
    }
}
