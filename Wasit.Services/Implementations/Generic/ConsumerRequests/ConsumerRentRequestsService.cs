using AAITHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.Interfaces.Rent;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.Rent.RentRatingRequest;
using Wasit.Services.DTOs.Schema.Rent.RentRequest;
using Wasit.Services.DTOs.Schema.Rent.RentReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.ConsumerRequests;
using Wasit.Services.Interfaces.Generic.Shared;
using ListRentRequestsPayload = Wasit.Services.DTOs.Schema.Shared.ConsumerRequests.ListRentRequestsPayload;

namespace Wasit.Services.Implementations.Generic.ConsumerRequests
{
    public class ConsumerRentRequestsService : BaseService, IConsumerRentRequestsService, IConsumerSharedRequestsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentEstateRepository _rentEstateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IGeneralService _generalService;

        public ConsumerRentRequestsService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _rentEstateRepository = uow.Repository<IRentEstateRepository>();
            _userRepository = uow.Repository<IUserRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
            _generalService = (IGeneralService)serviceProvider.GetService(typeof(IGeneralService));
        }



        public async Task<PageDTO<ConsumerRentCategoryRequestDto>> ListRequests(string userId, ListRentRequestsPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;

            IQueryable<dynamic> query = model.RequestType switch
            {
                RentRequestType.RentRequest => _rentEstateRepository.GetQuery<RentRequest>(x => x.UserId == userId && x.Status == (RentStatus)model.Status, false).OrderByDescending(x => x.Id),
                RentRequestType.RatingRequest => _rentEstateRepository.GetQuery<RentRatingRequest>(x => x.UserId == userId && x.RatingStatus == (RatingStatus)model.Status, false).OrderByDescending(x => x.Id),
                RentRequestType.ReservationRequest => _rentEstateRepository.GetQuery<RentReservationRequest>(x => x.UserId == userId && x.ReservationStatus == (ReservationStatus)model.Status, false).OrderByDescending(x => x.Id),
                _ => throw new BussinessRuleException("InvalidRequestType")
            };

            var data = await query.Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            return new PageDTO<ConsumerRentCategoryRequestDto>
            {
                CurrentPage = model.PageNumber,
                TotalCount = await query.CountAsync(),
                Count = data.Count,
                Data = _mapper.Map<List<ConsumerRentCategoryRequestDto>>(data)
            };
        }


        public async Task<ExtendedConsumerRentRequestInfoDto> RentRequestInfo(string userId, long requestId)
        {
            var request = await _rentEstateRepository.FirstOrDefaultAsync<RentRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<ExtendedConsumerRentRequestInfoDto>(request);
        }
        
        
        public async Task<ExtendedConsumerRatingRequestInfoForRentEstateDto> RatingRequestInfo(string userId, long requestId)
        {
            var request = await _rentEstateRepository.FirstOrDefaultAsync<RentRatingRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<ExtendedConsumerRatingRequestInfoForRentEstateDto>(request);
        }
              
        
        public async Task<ExtendedConsumerReservationRequestInfoForRentEstateDto> ReservationRequestInfo(string userId, long requestId)
        {
            var request = await _rentEstateRepository.FirstOrDefaultAsync<RentReservationRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<ExtendedConsumerReservationRequestInfoForRentEstateDto>(request);
        }


        public async Task<bool> CancelRentRequest(string userId, long requestId)
        {
            var request = await _rentEstateRepository.FirstOrDefaultAsync<RentRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            if (request.Status != RentStatus.New || request.IsAccepted)
                throw new BussinessRuleException("RentRequestStatusMustBeNewToBeCanceld");

            request.Status = RentStatus.Canceled;
            request.IsAccepted = false;

            _rentEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم الغاء طلب ايجار العقار رقم {request.RentEstate.EstateNumber} من قبل {request.User.User_Name}",
                TextEn = $"Rent request number {request.RentEstate.EstateNumber} has been canceled by {request.User.User_Name}",
                UserId = request.RentEstate.UserId,
                Type = NotifyTypes.CancelRentRequest,
                CategoryType = CategoryType.Rent,
                RouteId = request.RentEstateId
            });

            return true;
        }


        public async Task<bool> PayForRent(string userId, long requestId, TypePay paymentMethod)
        {
            var request = await _rentEstateRepository.FirstOrDefaultAsync<RentRequest>(x => x.Id == requestId && x.UserId == userId, true, x => x.RentEstate) ??
                throw new BussinessRuleException("RequestNotFound");

            var anyOtherRequestIsPaid = await _rentEstateRepository.GetQuery<RentRequest>(x => x.Id != requestId && x.RentEstateId == request.RentEstateId)
                .AnyAsync(x => x.Status == RentStatus.Current && x.IsPay);

            if (anyOtherRequestIsPaid)
                throw new BussinessRuleException("OtherRequestIsAlreadyPaid");

            if (request.Status != RentStatus.Current || !request.IsAccepted)
                throw new BussinessRuleException("RentRequestStatusMustBeCurrentToPayForRent");

            var totalMonths = request.YearCount * 12 + request.MonthCount;

            if (totalMonths >= 6)
                request.Deposit = request.RentEstate.MonthRentPrice * 6;
            else
                request.Deposit = request.RentEstate.MonthRentPrice * totalMonths;

            request.TypePay = paymentMethod;
            request.IsPay = true;
            request.PaymentDate = HelperDate.GetCurrentDate();
            request.RentEstate.PaidRentPrice = request.Deposit;

            _rentEstateRepository.Update(request);
            _rentEstateRepository.Update(request.RentEstate);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم دفع قيمة الايجار المستحق للعقار رقم {request.RentEstate.EstateNumber} من قبل {request.User.User_Name}",
                TextEn = $"{request.User.User_Name} has paid the required rent price of the property number {request.RentEstate.EstateNumber} ",
                UserId = request.RentEstate.UserId,
                Type = NotifyTypes.PayRent,
                CategoryType = CategoryType.Rent,
                RouteId = request.Id
            });

            return true;
        }


        public async Task<bool> ConfirmRecievingRentEstate(string userId, long requestId)
        {
            var request = await _rentEstateRepository.FirstOrDefaultAsync<RentRequest>(x => x.Id == requestId && x.UserId == userId) ??
               throw new BussinessRuleException("RequestNotFound");

            if (request.Status != RentStatus.Current && request.Deposit == 0)
                throw new BussinessRuleException("RentRequestStatusMustBeCurrentToConfirmRecievingRentEstate");

            request.IsEstateRented = true;
            request.Status = RentStatus.Finished;

            _rentEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم تأكيد استلام العقار رقم {request.RentEstate.EstateNumber} من قبل {request.User.User_Name}",
                TextEn = $"The property number {request.RentEstate.EstateNumber} has been received by {request.User.User_Name}",
                UserId = request.RentEstate.UserId,
                Type = NotifyTypes.ConfirmEstateIsReceived,
                CategoryType = CategoryType.Rent,
                RouteId = request.Id
            });

            return true;
        }


        // User => RentRequest + RentRatingRequest + SaleRatingRequest + DailyRentRequest + EntertainmentRequest
        // Estate = DailyRentRequest + EntertainmentRequest 
        public async Task<bool> RateOwner(string userId, long requestId, double rating)
        {
            var request = await _rentEstateRepository
                .FirstOrDefaultAsync<RentRequest>(x => x.Id == requestId && x.UserId == userId, true, x => x.RentEstate.User) ??
                throw new BussinessRuleException("RequestNotFound");

            if (request.Status != RentStatus.Finished)
                throw new BussinessRuleException("RentRequestStatusMustBeFinishedToRateOwner");

            request.OwnerRating = rating;
            _rentEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();


            request.RentEstate.User.Rating = await _generalService.CalcAverageUserRating(request.RentEstate.UserId);

            _userRepository.UpdateUser(request.RentEstate.User);

            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {request.User.User_Name} بتقييمك",
                TextEn = $"{request.User.User_Name} has rated you",
                UserId = request.RentEstate.UserId,
                Type = NotifyTypes.NewRatingForPublisher,
                CategoryType = CategoryType.Rent,
                RouteId = request.Id
            });

            return true;
        }

        public async Task<bool> RateReviewer(string userId, long requestId, double rating)
        {
            var request = await _rentEstateRepository.FirstOrDefaultAsync<RentRatingRequest>(x => x.Id == requestId && x.UserId == userId) ??
               throw new BussinessRuleException("RequestNotFound");

            if (request.RatingStatus != RatingStatus.Finished)
                throw new BussinessRuleException("RatingRequestStatusMustBeFinishedToRateReviewer");

            request.ReviewerRating = rating;
            _rentEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            request.Provider.Rating = await _generalService.CalcAverageUserRating(request.ProviderId);
            _userRepository.UpdateUser(request.Provider);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {request.User.User_Name} بتقييمك",
                TextEn = $"{request.User.User_Name} has rated you",
                UserId = request.ProviderId,
                Type = NotifyTypes.NewRatingForDelegate,
                CategoryType = CategoryType.Rent,
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
