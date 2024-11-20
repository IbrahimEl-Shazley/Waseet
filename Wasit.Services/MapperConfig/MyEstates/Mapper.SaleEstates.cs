using AAITHelper;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Core.Helpers.IO;
using Wasit.Core.Models.DTO;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Sale.PurchaseRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;
using Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest;
using Wasit.Services.DTOs.Schema.Shared;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.MapperConfig
{
    public partial class MapperProfile : Profile
    {
        public void MapSaleEstates(ICurrentUserService currentUser)
        {
            CreateMap<SaleEstate, SaleEstateInfoDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EstateName))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => new SharedDto 
                {
                    Id = src.Region.CityId,
                    Name = currentUser.IsArabic ? src.Region.City.NameAr : src.Region.City.NameEn
                }))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => new SharedDto
                {
                    Id = src.RegionId,
                    Name = currentUser.IsArabic ? src.Region.NameAr : src.Region.NameEn
                }))
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.EstateFeatures))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.EstateNumber))
                .ForMember(dest => dest.IsVisible, opt => opt.MapFrom(src => src.IsShow))
                .ForMember(dest => dest.Addresss, opt => opt.MapFrom(src => src.Address(currentUser.Language)))
                .ForMember(dest => dest.EstateType, opt => opt.MapFrom(src => currentUser.IsArabic ? src.EstateType.NameAr : src.EstateType.NameEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.EstateDescription))
                .ForMember(dest => dest.IsDevelopable, opt => opt.MapFrom(src => src.Developable))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.EstatePrice))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.EstateArea))
                .ForMember(dest => dest.FinalSalePrice, opt => opt.MapFrom(src => src.FinalEstatePrice))
                .ForMember(dest => dest.AveragePrice, opt => opt.MapFrom(src => src.UserPriceToEstates.Count != 0 ? src.UserPriceToEstates.Average(p => p.Price) : 0))
                .ForMember(dest => dest.ReservationRequestsCount, opt => opt.MapFrom(src => src.ReservationRequests.Count()))
                .ForMember(dest => dest.EvaluationRequestsCount, opt => opt.MapFrom(src => src.RatingRequests.Where(x => x.RatingStatus != RatingStatus.New).Count()))
                .ForMember(dest => dest.PurchaseRequestsCount, opt => opt.MapFrom(src => src.PurchaseRequests.Where(x => x.Status != PurchaseStatus.Canceled).Count()))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => new BaseImageItemDto
                {
                    Id = i.Id,
                    Url = MyConstants.DomainUrl + i.Image
                }).ToHashSet()))
                .ForMember(dest => dest.Specs, opt => opt.MapFrom(src => src.SpecificationValues.Select(s => new SpecificationItemDto
                {
                    Id = s.Id,
                    Name = $"{s.EstateTypeSpecification.Specification.Name(currentUser.Language)}: {s.SpecificationValue}",
                    Icon = $"{MyConstants.DomainUrl}{s.EstateTypeSpecification.Specification.Icon}"
                })));

            CreateMap<SaleReservationRequest, SaleReservationRequestDto>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.User_Name))
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => EnumHelper.ReservationRequestStatus(src.ReservationStatus, currentUser.Language)));
            CreateMap<PageDTO<SaleReservationRequest>, PageDTO<SaleReservationRequestDto>>()
                 .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

            CreateMap<SaleRatingRequest, SaleRatingRequestDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.User_Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => EnumHelper.EvaluationRequestStatus(src.RatingStatus, currentUser.Language)));
            CreateMap<PageDTO<SaleRatingRequest>, PageDTO<SaleRatingRequestDto>>()
                 .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

            CreateMap<PurchaseRequest, PurchaseRequestDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.User_Name))
                .ForMember(dest => dest.IsAccepted, opt => opt.MapFrom(src => src.IsAccepted))
                .ForMember(dest => dest.FinalEstatePrice, opt => opt.MapFrom(src => src.SaleEstate.FinalEstatePrice))
                .ForMember(dest => dest.IsRejected, opt => opt.MapFrom(src => !src.IsAccepted && src.Status == PurchaseStatus.Finished))
                .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPay))
                .ForMember(dest => dest.Status, opt => opt
                    .MapFrom(src => EnumHelper.PublisherPurchaseRequestStatus(src, currentUser.Language)));
            CreateMap<PageDTO<PurchaseRequest>, PageDTO<PurchaseRequestDto>>()
                 .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

            CreateMap<PurchaseRequest, PurchaseRequestInfoDto>()
                .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.SaleEstate))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.User_Name))
                .ForMember(dest => dest.IsAccepted, opt => opt.MapFrom(src => src.IsAccepted))
                .ForMember(dest => dest.FinalEstatePrice, opt => opt.MapFrom(src => src.SaleEstate.FinalEstatePrice))
                .ForMember(dest => dest.IsRejected, opt => opt.MapFrom(src => !src.IsAccepted && src.Status == PurchaseStatus.Finished))
                .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPay))
                .ForMember(dest => dest.StatusEnum, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Status, opt => opt
                    .MapFrom(src => EnumHelper.PublisherPurchaseRequestStatus(src, currentUser.Language)));


            CreateMap<SaleReservationRequest, SaleReservationRequestInfoDto>()
                .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.SaleEstate))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.User_Name))
                .ForMember(dest => dest.StatusEnum, opt => opt.MapFrom(src => src.ReservationStatus))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => EnumHelper.ReservationRequestStatus(src.ReservationStatus, currentUser.Language)))
                .ForMember(dest => dest.RequestDateTime, opt => opt.MapFrom(src => src.ReservationDate.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")));

            CreateMap<SaleRatingRequest, SaleRatingRequestInfoDto>()
                .ForMember(dest => dest.EstateInfo, opt => opt.MapFrom(src => src.SaleEstate))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.User_Name))
                .ForMember(dest => dest.EvaluatuionPrice, opt => opt.MapFrom(src => src.ServiceCost))
                .ForMember(dest => dest.DelegateName, opt => opt.MapFrom(src => src.Provider.User_Name))
                .ForMember(dest => dest.StatusEnum, opt => opt.MapFrom(src => src.RatingStatus))
                .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.ReportUrl));

            CreateMap<CreateSaleEstateDto, SaleEstate>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => currentUser.UserId))
              .ForMember(dest => dest.EstateName, opt => opt.MapFrom(src => src.NameAr))
              .ForMember(dest => dest.EstateDescription, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.Developable, opt => opt.MapFrom(src => src.IsDevelopable))
              .ForMember(dest => dest.EstatePrice, opt => opt.MapFrom(src => src.Price))
              .ForMember(dest => dest.EstateNumber, opt => opt.MapFrom(src => src.UniqueNumber))
              .ForMember(dest => dest.EstateFeatures, opt => opt.MapFrom(src => src.Features))
              .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => currentUser.UserId))
              .ForMember(dest => dest.IsShow, opt => opt.MapFrom(src => true))
              .ForMember(dest => dest.EstateArea, opt => opt.MapFrom(src => src.Area));

            CreateMap<IFormFile, SaleEstateImage>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => IOHelper.Upload(src, (int)FileName.Estates)));

            CreateMap<SpecificationKeyValueDto, SaleEstateSpecificationValue>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.EstateTypeSpecificationId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.SpecificationValue, opt => opt.MapFrom(src => src.Value));

            CreateMap<UpdateSaleEstateDto, SaleEstate>()
              .ForMember(dest => dest.EstateName, opt => opt.MapFrom(src => src.NameAr))
              .ForMember(dest => dest.EstateDescription, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.Developable, opt => opt.MapFrom(src => src.IsDevelopable))
              .ForMember(dest => dest.EstatePrice, opt => opt.MapFrom(src => src.Price))
              .ForMember(dest => dest.EstateFeatures, opt => opt.MapFrom(src => src.Features))
              .ForMember(dest => dest.IsShow, opt => opt.Ignore())
              .ForMember(dest => dest.EstateArea, opt => opt.MapFrom(src => src.Area))
              .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => HelperDate.GetCurrentDate(3)))
              .ForMember(dest => dest.UpdatedById, opt => opt.MapFrom(src => currentUser.UserId))
              .ForMember(dest => dest.Images, opt => opt.Ignore())
              .ForMember(dest => dest.SpecificationValues, opt => opt.Ignore());
        }
    }
}
