using AutoMapper;
using Wasit.Core.Entities.PropertiesManagement;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Core.Helpers.IO;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.PropertiesManagement.RentManagement;
using Wasit.Services.DTOs.Schema.Shared;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapRentManagement(ICurrentUserService currentUser)
        {
            CreateMap<AddRentalMangementOrderPayload, RentalManagementOrder>()
                .ForMember(dest => dest.EstateNumber, opt => opt.MapFrom(src => src.UniqueNumber))
                .ForMember(dest => dest.EstateName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => RentalManagementOrderStatus.New))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => currentUser.UserId))
                .ForMember(dest => dest.EstateImage, opt => opt.MapFrom(src => IOHelper.Upload(src.Image, (int)FileName.Estates)))
                .ForMember(dest => dest.EstateApartments, opt => opt.MapFrom(src => src.Apartments
                .Select(a => new EstateApartment
                {
                    ApartmentName = a.Name,
                    RentalPrice = a.RentPrice,
                    PaymentDeadline = a.PaymentDeadline,
                    Number = new Random().Next(100000, 999999)
                })));


            CreateMap<EditRentalMangementOrderPayload, RentalManagementOrder>()
                .ForMember(dest => dest.EstateName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.RegionId))
                .ForMember(dest => dest.EstateImage, opt => opt.Ignore());

            CreateMap<AddApartmentDto, EstateApartment>()
                .ForMember(dest => dest.ApartmentName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RentalPrice, opt => opt.MapFrom(src => src.RentPrice))
                .ForMember(dest => dest.PaymentDeadline, opt => opt.MapFrom(src => src.PaymentDeadline))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => new Random().Next(100000, 999999)));


            CreateMap<RentalManagementOrder, RentalManagementOrderItemDto>()
                .ForMember(dest => dest.UniqueNumber, opt => opt.MapFrom(src => src.EstateNumber))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EstateName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsCanceled ? currentUser.Language == Language.Ar ? "ملغي" : "Canceled" : string.Empty))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => MyConstants.DomainUrl + src.EstateImage))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address(currentUser.Language)));


            CreateMap<RentalManagementOrder, ManagedRentEstateInfoDto>()
                 .ForMember(dest => dest.UniqueNumber, opt => opt.MapFrom(src => src.EstateNumber))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EstateName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsCanceled ? currentUser.Language == Language.Ar ? "ملغي" : "Canceled" : string.Empty))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => MyConstants.DomainUrl + src.EstateImage))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address(currentUser.Language)))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => new SharedDto
                {
                    Id = src.RegionId,
                    Name = currentUser.IsArabic ? src.Region.NameAr : src.Region.NameEn
                }))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => new SharedDto
                {
                    Id = src.Region.CityId,
                    Name = currentUser.IsArabic ? src.Region.City.NameAr : src.Region.City.NameEn
                }))
                .ForMember(dest => dest.Apartments, opt => opt.MapFrom(src => src.EstateApartments.Select(a => new ApartmentItemDto
                {
                    Id = a.Id,
                    Name = a.ApartmentName,
                    Number = a.Number,
                    RentPrice = a.RentalPrice,
                    PaymentDeadline = EnumHelper.PaymentDeadline(a.PaymentDeadline, currentUser.Language),
                    Status = a.IsRentPaid ? currentUser.IsArabic ? "تم السداد" : "Paid" : currentUser.IsArabic ? "لم يتم السداد بعد" : "Unpaid yet"
                })));

        }
    }
}
