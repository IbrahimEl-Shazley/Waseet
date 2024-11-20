using AutoMapper;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.Helpers.IO;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.EstateCategories.Category;
using Wasit.Services.DTOs.Schema.LOOKUP.Cities;
using Wasit.Services.DTOs.Schema.LOOKUP.Regions;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapLookups(ICurrentUserService currentUser)
        {
            CreateMap<City, CityDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => currentUser.IsArabic ? src.NameAr : src.NameEn));
            CreateMap<UpdateCityDto, City>();

            CreateMap<Region, RegionDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => currentUser.IsArabic ? src.NameAr : src.NameEn));
            CreateMap<UpdateRegionDto, Region>();

            CreateMap<Category, CategoryDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => currentUser.IsArabic ? src.NameAr : src.NameEn))
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateCategoryDto, Category>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => IOHelper.Upload(src.Image, (int)FileName.Category)));
        }
    }
}
