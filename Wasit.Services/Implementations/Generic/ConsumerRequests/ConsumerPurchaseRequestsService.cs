using AAITHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Ocsp;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces.Sale;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.Sale.PurchaseRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.ConsumerRequests;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.Services.Implementations.Generic.ConsumerRequests
{
    public class ConsumerPurchaseRequestsService : BaseService, IConsumerPurchaseRequestsService, IConsumerSharedRequestsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleEstateRepository _saleEstateRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IGeneralService _generalService;

        public ConsumerPurchaseRequestsService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _saleEstateRepository = uow.Repository<ISaleEstateRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
            _generalService = (IGeneralService)serviceProvider.GetService(typeof(IGeneralService));
        }


        public async Task<PageDTO<ConsumerPurchaseCategoryRequestDto>> ListRequests(string userId, ListSaleRequestsPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;

            IQueryable<dynamic> query = model.RequestType switch
            {
                SaleRequestType.PurchaseRequest => _saleEstateRepository.GetQuery<PurchaseRequest>(x => x.UserId == userId && x.Status == (PurchaseStatus)model.Status, false).OrderByDescending(x => x.Id),
                SaleRequestType.RatingRequest => _saleEstateRepository.GetQuery<SaleRatingRequest>(x => x.UserId == userId && x.RatingStatus == (RatingStatus)model.Status, false).OrderByDescending(x => x.Id),
                SaleRequestType.ReservationRequest => _saleEstateRepository.GetQuery<SaleReservationRequest>(x => x.UserId == userId && x.ReservationStatus == (ReservationStatus)model.Status, false).OrderByDescending(x => x.Id),
                _ => throw new BussinessRuleException("InvalidRequestType")
            };

            var data = await query.Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            return new PageDTO<ConsumerPurchaseCategoryRequestDto>
            {
                CurrentPage = model.PageNumber,
                TotalCount = await query.CountAsync(),
                Count = data.Count,
                Data = _mapper.Map<List<ConsumerPurchaseCategoryRequestDto>>(data)
            };
        }


        public async Task<ExtendedConsumerPurchaseRequestInfoDto> PurchaseRequestInfo(string userId, long requestId)
        {
            var request = await _saleEstateRepository.FirstOrDefaultAsync<PurchaseRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<ExtendedConsumerPurchaseRequestInfoDto>(request);
        }

        public async Task<ExtendedConsumerRatingRequestInfoForSaleEstateDto> RatingRequestInfo(string userId, long requestId)
        {
            var request = await _saleEstateRepository.FirstOrDefaultAsync<SaleRatingRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<ExtendedConsumerRatingRequestInfoForSaleEstateDto>(request);
        }

        public async Task<ExtendedConsumerReservationRequestInfoForSaleEstateDto> ReservationRequestInfo(string userId, long requestId)
        {
            var request = await _saleEstateRepository.FirstOrDefaultAsync<SaleReservationRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<ExtendedConsumerReservationRequestInfoForSaleEstateDto>(request);
        }

        public async Task<bool> CancelPurchaseRequest(string userId, long requestId)
        {
            var request = await _saleEstateRepository.FirstOrDefaultAsync<PurchaseRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            if (request.Status != PurchaseStatus.New || request.IsAccepted)
                throw new BussinessRuleException("PurchaseRequestStatusMustBeNewToBeCanceld");

            request.Status = PurchaseStatus.Canceled;
            request.IsAccepted = false;

            _saleEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم الغاء طلب شراء العقار رقم {request.SaleEstate.EstateNumber} من قبل {request.User.User_Name}",
                TextEn = $"Purchase request number {request.SaleEstate.EstateNumber} has been canceled by {request.User.User_Name}",
                UserId = request.SaleEstate.UserId,
                Type = NotifyTypes.CancelPurchaseRequest,
                CategoryType = CategoryType.Sale,
                RouteId = request.SaleEstateId
            });

            return true;
        }


        public async Task<bool> PayDeposit(string userId, long requestId, TypePay paymentMethod)
        {
            var request = await _saleEstateRepository.FirstOrDefaultAsync<PurchaseRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            var anyOtherRequestIsPaid = await _saleEstateRepository.GetQuery<PurchaseRequest>(x => x.Id != requestId && x.SaleEstateId == request.SaleEstateId)
                .AnyAsync(x => x.Status == PurchaseStatus.Current && x.IsPay);

            if (anyOtherRequestIsPaid)
                throw new BussinessRuleException("OtherRequestIsAlreadyPaid");

            if (request.Status != PurchaseStatus.Current || !request.IsAccepted)
                throw new BussinessRuleException("PurchaseRequestStatusMustBeCurrentToPayDeposit");

            request.TypePay = paymentMethod;
            request.Deposit = request.SaleEstate.Deposit;
            request.IsPay = true;
            request.PaymentDate = HelperDate.GetCurrentDate();

            _saleEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم دفع قيمة العربون للعقار رقم {request.SaleEstate.EstateNumber} من قبل {request.User.User_Name}",
                TextEn = $"{request.User.User_Name} has paid the deposit of the property number {request.SaleEstate.EstateNumber} ",
                UserId = request.SaleEstate.UserId,
                Type = NotifyTypes.PayDposit,
                CategoryType = CategoryType.Sale,
                RouteId = request.Id
            });

            return true;
        }


        public async Task<bool> ConfirmRecievingSaleEstate(string userId, long requestId)
        {
            var request = await _saleEstateRepository.FirstOrDefaultAsync<PurchaseRequest>(x => x.Id == requestId && x.UserId == userId) ??
               throw new BussinessRuleException("RequestNotFound");

            if (request.Status != PurchaseStatus.Current && request.Deposit == 0)
                throw new BussinessRuleException("PurchaseRequestStatusMustBeCurrentToConfirmRecievingSaleEstate");

            request.IsEstateReceived = true;
            request.Status = PurchaseStatus.Finished;

            _saleEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم تأكيد استلام العقار رقم {request.SaleEstate.EstateNumber} من قبل {request.User.User_Name}",
                TextEn = $"The property number {request.SaleEstate.EstateNumber} has been received by {request.User.User_Name}",
                UserId = request.SaleEstate.UserId,
                Type = NotifyTypes.ConfirmEstateIsReceived,
                CategoryType = CategoryType.Sale,
                RouteId = request.Id
            });

            return true;
        }


        public async Task<bool> AddRefundRequest(string userId, long requestId, string reason)
        {
            var request = await _saleEstateRepository.FirstOrDefaultAsync<PurchaseRequest>(x => x.Id == requestId && x.UserId == userId) ??
               throw new BussinessRuleException("RequestNotFound");

            if (request.SaleEstate.IsSold || (request.Status != PurchaseStatus.Current && !request.IsPay))
                throw new BussinessRuleException("PurchaseRequestStatusMustBeCurrentToAddRefundRequest");

            if (request.HasRefundRequest)
                throw new BussinessRuleException("RequestAlreadySent");

            request.HasRefundRequest = true;
            request.RefundReason = reason;

            _saleEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyToDashboardAsync(new NotificationModel
            {
                TextAr = $"قام {request.User.User_Name} بطلب استرجاع قيمة العربون للعقار رقم {request.SaleEstate.EstateNumber}",
                TextEn = $"{request.User.User_Name} has sent a refund request for the property number {request.SaleEstate.EstateNumber}",
                Type = NotifyTypes.NewRefundRequest,
                CategoryType = CategoryType.Sale,
                RouteId = request.Id
            });

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {request.User.User_Name} بطلب استرجاع قيمة العربون للعقار رقم {request.SaleEstate.EstateNumber}",
                TextEn = $"{request.User.User_Name} has sent a refund request for the property number {request.SaleEstate.EstateNumber}",
                UserId = request.SaleEstate.UserId,
                Type = NotifyTypes.NewRefundRequest,
                CategoryType = CategoryType.Sale,
                RouteId = request.Id
            });

            return true;
        }


        public async Task<bool> RateReviewer(string userId, long requestId, double rating)
        {
            var request = await _saleEstateRepository.FirstOrDefaultAsync<SaleRatingRequest>(x => x.Id == requestId && x.UserId == userId) ??
               throw new BussinessRuleException("RequestNotFound");

            if (request.RatingStatus != RatingStatus.Finished)
                throw new BussinessRuleException("RatingRequestStatusMustBeFinishedToRateReviewer");

            request.UserRating = rating;
            _saleEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            request.Provider.Rating = await _generalService.CalcAverageUserRating(request.ProviderId);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {request.User.User_Name} بتقييمك",
                TextEn = $"{request.User.User_Name} has rated you",
                UserId = request.ProviderId,
                Type = NotifyTypes.NewRatingForDelegate,
                CategoryType = CategoryType.Sale,
                RouteId = request.Id
            });

            return true;
        }

        public Task<bool> RateOwnerAndEstate(string userId, RateOwnerAndEstatePayload payload)
        {
            throw new NotImplementedException();
        }
    }
}
