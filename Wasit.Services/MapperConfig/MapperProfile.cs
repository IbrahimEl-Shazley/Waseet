using AutoMapper;
using Wasit.Service.Interfaces.General;


namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public MapperProfile()
        {
        }
        public MapperProfile(ICurrentUserService currentUserService)
        {
            MapSecurity(currentUserService);
            MapLookups(currentUserService);
            MapEnums(currentUserService);
            MapCore(currentUserService);
            MapConfig(currentUserService);
            MapAcc(currentUserService);
            MapNotific(currentUserService);


            #region MyEstates
            MapSaleEstates(currentUserService);
            MapRentEstates(currentUserService);
            MapEntertainmentEstates(currentUserService);
            MapDailyRentEstates(currentUserService);
            MapMySharedEstates(currentUserService);
            #endregion

            #region ConsumerEstates
            MapConsumerSaleEstates(currentUserService);
            MapConsumerRentEstates(currentUserService);
            MapConsumerEntertainmentEstates(currentUserService);
            MapConsumerDailyRentEstates(currentUserService);
            MapConsumerSharedEstates(currentUserService);
            #endregion

            #region ConsumerRequests
            MapConsumerPurchaseRequests(currentUserService);
            MapConsumerRentRequests(currentUserService);
            MapConsumerEntertainmentRequests(currentUserService);
            MapConsumerDailyRentRequests(currentUserService);
            #endregion

            #region PropertiesManagement
            MapRentManagement(currentUserService);
            MapMaintainanceManagement(currentUserService);
            MapPayApartmentRent(currentUserService);
            #endregion

            #region EstateService
            MapEstateService(currentUserService);
            #endregion

            #region File
            //CreateMap<CloudinaryDotNet.Actions.ImageUploadResult, FileDTO>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SecureUrl.Segments[5]))
            //    .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.SecureUrl.AbsoluteUri))
            //    .ReverseMap();
            #endregion
        }

    }
}
