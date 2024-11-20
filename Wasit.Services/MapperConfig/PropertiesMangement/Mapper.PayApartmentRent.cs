using AutoMapper;
using Wasit.Core.Entities.PropertiesManagement;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.PropertiesManagement.PayApartmentRent;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapPayApartmentRent(ICurrentUserService currentUser)
        {
            CreateMap<EstateApartment, RentPayerApartmentInfoDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ApartmentName))
                .ForMember(dest => dest.UniqueNumber, opt => opt.MapFrom(src => src.RentalOrder.EstateNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.RentalOrder.Address(currentUser.Language)))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => MyConstants.DomainUrl + src.RentalOrder.EstateImage))
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.RentalOrder.User.User_Name))
                .ForMember(dest => dest.Apartment, opt => opt.MapFrom(src => new RentPayerApartmentItemInfoDto
                {
                    Id = src.Id,
                    Number = src.Number,
                    PaymentDeadline = EnumHelper.PaymentDeadline(src.PaymentDeadline, currentUser.Language),
                    Name = src.ApartmentName,
                    RentPrice = src.RentalPrice,
                    IsPaid = src.IsRentPaid,
                    Status = src.IsRentPaid ? currentUser.IsArabic ? "تم السداد" : "Paid" : currentUser.IsArabic ? "لم يتم السداد بعد" : "Unpaid yet"
                }));
        }
    }
}
