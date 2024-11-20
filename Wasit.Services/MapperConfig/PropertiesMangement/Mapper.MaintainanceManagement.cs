using AutoMapper;
using Wasit.Core.Entities.PropertiesManagement;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.IO;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.PropertiesManagement.MaintainanceMangment;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapMaintainanceManagement(ICurrentUserService currentUser)
        {
            CreateMap<AddMaintainanceOrderPayload, MaintenanceOrder>()
                .ForMember(dest => dest.EstateNumber, opt => opt.MapFrom(src => src.UniqueNumber))
                .ForMember(dest => dest.EstateName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => RentalManagementOrderStatus.New))
                .ForMember(dest => dest.EstateArea, opt => opt.MapFrom(src => src.Area))
                .ForMember(dest => dest.TypePay, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => currentUser.UserId))
                .ForMember(dest => dest.EstateImage, opt => opt.MapFrom(src => IOHelper.Upload(src.Image, (int)FileName.Estates)))
                .ForMember(dest => dest.ServiceCost,
                    opt => opt.MapFrom((src, dest, destMember, context) => context.Items[nameof(MaintenanceOrder.ServiceCost)]));


            CreateMap<MaintenanceOrder, MaintainanceOrderItemDto>()
                .ForMember(dest => dest.UniqueNumber, opt => opt.MapFrom(src => src.EstateNumber))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EstateName))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => MyConstants.DomainUrl + src.EstateImage))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address(currentUser.Language)))
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Lng, opt => opt.MapFrom(src => src.Lng))
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => src.Provider.User_Name))
                .ForMember(dest => dest.ProviderPhone, opt => opt.MapFrom(src => src.Provider.PhoneNumber))
                .ForMember(dest => dest.IsRated, opt => opt.MapFrom(src => src.UserRate > 0));
        }
    }
}
