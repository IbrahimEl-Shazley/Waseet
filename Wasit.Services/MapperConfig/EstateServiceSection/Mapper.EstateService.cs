using AutoMapper;
using Microsoft.AspNetCore.Http;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EstateServiceSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers.IO;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.EstateService;
//using Wasit.Core.Entities.Schema.ACC;
//using Wasit.Services.Interfaces.General;
//using Wasit.Services.DTOs.Schema.ACC;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapEstateService(ICurrentUserService currentUserService)
        {
            CreateMap<P4ShowEstateService, EstateServicePackagesDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name(currentUserService.Language)))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Description(currentUserService.Language)));

        }
    }
}
