using AAITHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.ExtensionsMethods;
using Wasit.Core.Helpers;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.Interfaces.Sale;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.EstateCategories.EstateType;
using Wasit.Services.DTOs.Schema.Sale.PurchaseRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;
using Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.MyEstates;

namespace Wasit.Services.Implementations.Generic.MyEstates
{
    public class SaleEstateService : BaseService<SaleEstate, SaleEstateDto, CreateSaleEstateDto, UpdateSaleEstateDto>, IMySharedEstatesService, ISaleEstatesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleEstateRepository _saleEstateRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;

        public SaleEstateService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
            _saleEstateRepository = uow.Repository<ISaleEstateRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
            _userRepository = (IUserRepository)serviceProvider.GetService(typeof(IUserRepository));
        }


        public async Task<bool> ConfirmEstateDeedNumber(string estateDeedNumber)
        {
            var estateFound = await _saleEstateRepository
            .AnyAsync<SaleEstate>(s => s.EstateNumber == estateDeedNumber);

            if (estateFound)
                throw new BussinessRuleException("EstateAlreadyExists");

            return true;
        }


        public async Task<dynamic> GetEstateTypes()
        {
            var result = await _saleEstateRepository
               .GetQuery<CategoryEstateType>(x =>x.Category.Type == CategoryType.Sale && x.EstateType.IsActive, false, [x => x.EstateType, x => x.Category])
               .ToListAsync();

            return _mapper.Map<List<EstateTypeDto>>(result);
        }


        public async Task<dynamic> ListMyEstates(string userId, int pageNumber)
        {
            return await GetListWithPagingAsync(pageNumber, MyConstants.PageSize, estate => estate.UserId == userId, false, [estate => estate.Region.City, estate => estate.User]);
        }


        public async Task<bool> AddNewSaleEstate(string userId, CreateSaleEstateDto model)
        {
            if (await _saleEstateRepository.SaleEstateExists(model.UniqueNumber, userId))
                throw new BussinessRuleException("EstateAlreadyExists");

            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId, false) ??
                 throw new BussinessRuleException("UserNotFound");

            try
            {
                var saleEstate = _mapper.Map<SaleEstate>(model);
                var saleEstateImages = _mapper.Map<List<SaleEstateImage>>(model.Images);
                var saleEstateSpecs = _mapper.Map<List<SaleEstateSpecificationValue>>(model.AdditionalSpecs);

                await _unitOfWork.BeginTransactionAsync();

                saleEstate.Images = saleEstateImages;
                saleEstate.SpecificationValues = saleEstateSpecs;

                await _saleEstateRepository.AddAsync(saleEstate);
                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitAsync();

                await _notificationService.SendNotifyToDashboardAsync(new NotificationModel
                {
                    TextAr = "لديك طلب اضافة عقار جديد، يرجي مراجعته",
                    TextEn = "You have a new request to add a new property, please review it",
                    Type = NotifyTypes.AddNewEstate,
                    CategoryType = CategoryType.Sale,
                    RouteId = saleEstate.Id
                });

                return true;
            }
            catch
            {
                await _unitOfWork.RollBackAsync();
                throw new BussinessRuleException("InternalServerErrorOccured");
            }
        }


        public async Task<bool> EditSaleEstate(string userId, UpdateSaleEstateDto model)
        {
            var saleEstate = await _saleEstateRepository
                .FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId, true, [x => x.Images, x => x.SpecificationValues]) ??
                throw new BussinessRuleException("EstateNotFound");

            try
            {
                var updatedSaleEstate = _mapper.Map(model, saleEstate);
                var saleEstateImages = _mapper.Map<List<SaleEstateImage>>(model.Images);
                var saleEstateSpecs = _mapper.Map<List<SaleEstateSpecificationValue>>(model.AdditionalSpecs);

                await _unitOfWork.BeginTransactionAsync();

                if (model.Images.Any())
                {
                    saleEstateImages.ForEach(x => x.SaleEstateId = saleEstate.Id);
                    await _saleEstateRepository.AddRangeAsync(saleEstateImages);
                }

                if (model.ImagesToBeRemoved.Any())
                    _saleEstateRepository.RemoveRange(saleEstate.Images.Where(x => model.ImagesToBeRemoved.Contains(x.Id)).ToList());

                _saleEstateRepository.RemoveRange(saleEstate.SpecificationValues.ToList());
                updatedSaleEstate.SpecificationValues = saleEstateSpecs;

                _saleEstateRepository.Update(updatedSaleEstate);
                await _unitOfWork.SaveChangeAsync();

                await _unitOfWork.CommitAsync();

                return true;
            }
            catch
            {
                await _unitOfWork.RollBackAsync();
                throw new BussinessRuleException("InternalServerErrorOccured");
            }
        }


        public async Task<IEnumerable<SpecificationValueDto>> GetSpecificationValues(string userId, long estateId)
        {
            var specificationValues = await _saleEstateRepository.GetListAsync<SaleEstateSpecificationValue>(x => x.SaleEstateId == estateId, false);
            return _mapper.Map<List<SpecificationValueDto>>(specificationValues);
        }


        public async Task<SaleEstateInfoDto> MySaleEstateInfo(string userId, long estateId)
        {
            try
            {
                var saleEstate = await _saleEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId == userId
                , true, [x => x.UserPriceToEstates, x => x.Images, x => x.SpecificationValues]) ??
                    throw new BussinessRuleException("EstateNotFound");

                var cost = await _saleEstateRepository
                .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.AddPricingRequest && x.IsActive, false)
                .Select(x => x.ServiceCost)
                .FirstOrDefaultAsync();

                var result = _mapper.Map<SaleEstateInfoDto>(saleEstate);

                result.PricingServiceCost = cost * result.Area;

                return result;
            }
            catch
            {
                throw new BussinessRuleException("InternalServerErrorOccured");
            }
        }


        public async Task<PageDTO<SaleReservationRequestDto>> ListSaleReservationRequests(ListReservationRequestsPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;
            var result = await _saleEstateRepository
                .GetListWithPagingAsync<SaleReservationRequest>(skip, MyConstants.PageSize, x => x.SaleEstateId == model.EstateId && x.ReservationStatus == model.Status, false, x => x.User);
            return _mapper.Map<PageDTO<SaleReservationRequestDto>>(result);
        }


        public async Task<SaleReservationRequestInfoDto> ReservationSaleRequestInfo(long requestId)
        {
            var reservationRequest = await _saleEstateRepository.FirstOrDefaultAsync<SaleReservationRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<SaleReservationRequestInfoDto>(reservationRequest);
        }


        public async Task<PageDTO<SaleRatingRequestDto>> ListSaleRatingRequests(ListRatingRequestsPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;
            var result = await _saleEstateRepository
                .GetListWithPagingAsync<SaleRatingRequest>(skip, MyConstants.PageSize, x => x.SaleEstateId == model.EstateId && x.RatingStatus == model.Status, false, x => x.User);
            return _mapper.Map<PageDTO<SaleRatingRequestDto>>(result);
        }


        public async Task<SaleRatingRequestInfoDto> SaleRatingRequestInfo(long requestId)
        {
            var reservationRequest = await _saleEstateRepository.FirstOrDefaultAsync<SaleRatingRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<SaleRatingRequestInfoDto>(reservationRequest);
        }


        public async Task<PageDTO<PurchaseRequestDto>> ListPurchaseRequests(ListPurchaseRequestsPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;
            var result = await _saleEstateRepository
                .GetListWithPagingAsync<PurchaseRequest>(skip, MyConstants.PageSize, x => x.SaleEstateId == model.EstateId && x.Status != PurchaseStatus.Canceled, false, x => x.User);
            return _mapper.Map<PageDTO<PurchaseRequestDto>>(result);
        }

        public async Task<PurchaseRequestInfoDto> PurchaseRequestInfo(long requestId)
        {
            var purchaseRequest = await _saleEstateRepository.FirstOrDefaultAsync<PurchaseRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<PurchaseRequestInfoDto>(purchaseRequest);
        }

        public async Task<bool> AcceptPurchaseRequest(long requestId)
        {
            var purchaseRequest = await _saleEstateRepository.FirstOrDefaultAsync<PurchaseRequest>(x => x.Id == requestId, true, x => x.SaleEstate.PurchaseRequests) ??
                throw new BussinessRuleException("RequestNotFound");

            if (purchaseRequest.Status != PurchaseStatus.New)
                throw new BussinessRuleException("RequestCanNotBeAccepted");

            //if (otherRequests.Any(x => x.Status == PurchaseStatus.Current && x.Deposit > 0))
            //    throw new BussinessRuleException("RequestCanNotBeAcceptedAsThereIsACurrentPurchaseRequestWithDeposit");

            purchaseRequest.IsAccepted = true;
            purchaseRequest.Status = PurchaseStatus.Current;

            var otherRequests = purchaseRequest.SaleEstate.PurchaseRequests.Where(x => x.Id != requestId && x.Status != PurchaseStatus.Canceled).ToList();
            foreach (var request in otherRequests)
            {
                request.Status = PurchaseStatus.Finished;
            }
            _saleEstateRepository.Update(purchaseRequest);
            _saleEstateRepository.UpdateRange(otherRequests);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم قبول طلب شرائك لعقار رقم {purchaseRequest.SaleEstate.EstateNumber}",
                TextEn = $"Your purchase request for estate no. {purchaseRequest.SaleEstate.EstateNumber} has been accepted",
                UserId = purchaseRequest.UserId,
                Type = NotifyTypes.RequestAccepted,
                CategoryType = CategoryType.Sale,
                RouteId = requestId
            });

            return true;
        }


        public async Task<bool> RejectPurchaseRequest(long requestId)
        {
            var purchaseRequest = await _saleEstateRepository.FirstOrDefaultAsync<PurchaseRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

            if (purchaseRequest.Status != PurchaseStatus.New)
                throw new BussinessRuleException("RequestCanNotBeRejected");

            purchaseRequest.Status = PurchaseStatus.Finished;

            _saleEstateRepository.Update(purchaseRequest);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم رفض طلب شرائك لعقار رقم {purchaseRequest.SaleEstate.EstateNumber}",
                TextEn = $"Your purchase request for estate no. {purchaseRequest.SaleEstate.EstateNumber} has been rejected",
                UserId = purchaseRequest.UserId,
                Type = NotifyTypes.RequestRejected,
                CategoryType = CategoryType.Sale,
                RouteId = requestId
            });

            return true;
        }


        public async Task<bool> ConfirmEstateIsSold(long estateId, double price, long requestId)
        {
            var saleEstate = await _saleEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId) ??
                throw new BussinessRuleException("EstateNotFound");

            if (saleEstate.PurchaseRequests.Any(x => x.Id == requestId && !x.IsPay))
                throw new BussinessRuleException("EstateIsNotSold");

            if (saleEstate.PurchaseRequests.Any(x => x.Id == requestId && x.HasRefundRequest))
                throw new BussinessRuleException("ClientHasRefundRequest");

            saleEstate.IsSold = true;
            saleEstate.FinalEstatePrice = price;

            _saleEstateRepository.Update(saleEstate);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم تأكيد البيع للعقار رقم {saleEstate.EstateNumber} من قبل المعلن",
                TextEn = $"The sale of property no. {saleEstate.EstateNumber} has been confirmed by the advertiser",
                UserId = saleEstate.PurchaseRequests.FirstOrDefault(x => x.Id == requestId).UserId,
                Type = NotifyTypes.EstateIsSoldOrRentedConfirmation,
                CategoryType = CategoryType.Sale,
                RouteId = requestId
            });

            return true;
        }


        public async Task<bool> AddPricingRequest(string userId, long estateId, TypePay paymentMethod)
        {
            var cost = await _saleEstateRepository
                .GetQuery<Core.Entities.SettingTables.Service>(x => true, false)
                .Select(x => x.ServiceCost)
                .FirstOrDefaultAsync();

            var request = new PricingRequest
            {
                TypePay = paymentMethod,
                IsPay = true,
                ServiceCost = cost,
                SaleEstateId = estateId,
                CreatedById = userId,
                CreatedOn = HelperDate.GetCurrentDate()
            };

            await _saleEstateRepository.AddAsync(request);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyToDashboardAsync(new NotificationModel
            {
                TextAr = $"لديك طلب تثمين جديد",
                TextEn = $"You have a new pricing request",
                Type = NotifyTypes.NewPricingRequest,
                CategoryType = CategoryType.Sale,
                RouteId = request.Id
            });

            return true;
        }


        public async Task<bool> ChangeEstateVisibility(long estateId)
        {
            var saleEstate = await _saleEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId) ??
                throw new BussinessRuleException("EstateNotFound");

            saleEstate.IsShow = !saleEstate.IsShow;

            _saleEstateRepository.Update(saleEstate);
            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> RemoveEstate(long estateId)
        {
            var saleEstate = await _saleEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId) ??
                throw new BussinessRuleException("EstateNotFound");

            if (saleEstate.PurchaseRequests.Any(x => x.Status == PurchaseStatus.Current) ||
                saleEstate.ReservationRequests.Any(x => x.ReservationStatus == ReservationStatus.Current) ||
                saleEstate.RatingRequests.Any(x => x.RatingStatus == RatingStatus.New || x.RatingStatus == RatingStatus.Current))
                throw new BussinessRuleException("EstateCanNotBeRemoved");

            saleEstate.IsDeleted = true;

            _saleEstateRepository.Update(saleEstate);
            return await _unitOfWork.SaveChangeAsync();
        }


        #region Not Implemented Methods
        public Task<bool> ReportComment(string userId, long requestId, string reason)
        {
            throw new NotImplementedException();
        }

        public Task<PageDTO<BaseRatingItemDto>> ListEstateRatings(string userId, long estateId, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SelectReservationPeriod(string userId, long estateId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<string> DownloadFinancialAccounts(string userId, long estateId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RateRentee(string userId, long requestId, double rating)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
