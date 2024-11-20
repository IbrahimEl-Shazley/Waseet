using AAITHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.Interfaces.Daily;
using Wasit.Repositories.UnitOfWork;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.ConsumerRequests;
using Wasit.Services.Interfaces.Generic.Shared;
using ListDailyRentRequestsPayload = Wasit.Services.DTOs.Schema.Shared.ConsumerRequests.ListDailyRentRequestsPayload;

namespace Wasit.Services.Implementations.Generic.ConsumerRequests
{
    public class ConsumerDailyRentRequestsService : BaseService, IConsumerDailyRentRequestsService, IConsumerSharedRequestsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDailyRentEstateRepository _dailyRentEstateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly INotificationService _notificationService;
        private readonly IGeneralService _generalService;

        public ConsumerDailyRentRequestsService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _dailyRentEstateRepository = uow.Repository<IDailyRentEstateRepository>();
            _userRepository = uow.Repository<IUserRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _currentUserService = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
            _generalService = (IGeneralService)serviceProvider.GetService(typeof(IGeneralService));
        }


        public async Task<PageDTO<ConsumerDailyRentCategoryRequestDto>> ListRequests(string userId, ListDailyRentRequestsPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;
            var query = _dailyRentEstateRepository
                .GetQuery<DailyRentRequest>(x => x.UserId == userId && x.Status == (DailyRentStatus)model.Status, false)
                .OrderByDescending(x => x.Id);
            var data = await query.Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            var culture = DateTimeHelper.GetCulture(_currentUserService.Language);

            return new PageDTO<ConsumerDailyRentCategoryRequestDto>
            {
                CurrentPage = model.PageNumber,
                TotalCount = await query.CountAsync(),
                Count = data.Count,
                Data = _mapper.Map<List<ConsumerDailyRentCategoryRequestDto>>(data, o => o.Items["culture"] = culture)
            };
        }


        public async Task<ExtendedConsumerDailyRentCategoryRequestDto> RequestInfo(string userId, long requestId)
        {
            var request = await _dailyRentEstateRepository.FirstOrDefaultAsync<DailyRentRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            var culture = DateTimeHelper.GetCulture(_currentUserService.Language);

            return _mapper.Map<ExtendedConsumerDailyRentCategoryRequestDto>(request, o => o.Items["culture"] = culture);
        }


        public async Task<bool> CancelDailyRentRequest(string userId, long requestId)
        {
            var request = await _dailyRentEstateRepository.FirstOrDefaultAsync<DailyRentRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            if (request.Status != DailyRentStatus.New)
                throw new BussinessRuleException("DailyRentRequestStatusMustBeNewToBeCanceld");

            request.Status = DailyRentStatus.Canceled;
            request.CancelDate = HelperDate.GetCurrentDate();

            var now = HelperDate.GetCurrentDate();
            if (request.ArrivalDate - now < TimeSpan.FromDays(2))
            {
                var cancelationCost = await _dailyRentEstateRepository
                    .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.CancelEntertainmentRequest, false)
                    .Select(x => x.ServiceCost)
                    .SingleOrDefaultAsync();

                request.CancelStatus = CancelStatus.PartialRefund;
                request.User.Wallet += (request.TotalPrice - ((cancelationCost / 100) * request.TotalPrice));
            }
            else
            {
                request.CancelStatus = CancelStatus.FullRefund;
                request.User.Wallet += request.TotalPrice;
            }

            _dailyRentEstateRepository.Update(request);
            _userRepository.UpdateUser(request.User);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {request.User.User_Name} بالغاء طلب الايجار اليومي للعقار رقم {request.DailyRentEstate.EstateNumber}",
                TextEn = $"{request.User.User_Name} has canceled the daily rent request for the estate number {request.DailyRentEstate.EstateNumber}",
                Type = NotifyTypes.CancelDailyRentRequest,
                UserId = request.DailyRentEstate.UserId,
                CategoryType = CategoryType.DailyRent,
                RouteId = request.Id
            });

            return true;
        }


        public async Task<bool> RateOwnerAndEstate(string userId, RateOwnerAndEstatePayload payload)
        {
            var request = await _dailyRentEstateRepository.FirstOrDefaultAsync<DailyRentRequest>(x => x.Id == payload.RequestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            if (request.Status != DailyRentStatus.Finished)
                throw new BussinessRuleException("DailyRentRequestStatusMustBeNewToBeRated");

            request.UserRating = payload.OwnerRating;
            request.UserRatingDateTime = HelperDate.GetCurrentDate();
            request.EstateRating = payload.EstateRating;
            request.EstateComment = payload.Feedback;
            _dailyRentEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            request.DailyRentEstate.User.Rating = await _generalService.CalcAverageUserRating(request.DailyRentEstate.UserId);
            request.DailyRentEstate.Rating = await _generalService.CalcAverageEstateRating(request.DailyRentEstateId);
            _userRepository.UpdateUser(request.DailyRentEstate.User);
            _dailyRentEstateRepository.Update(request.DailyRentEstate);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {request.User.User_Name} بتقييمك",
                TextEn = $"User {request.User.User_Name} has rated you",
                UserId = request.DailyRentEstate.UserId,
                Type = NotifyTypes.NewRatingForPublisher,
                CategoryType = CategoryType.DailyRent,
                RouteId = request.Id
            });

            return true;
        }

        public Task<bool> RateReviewer(string userId, long requestId, double rating)
        {
            throw new NotImplementedException();
        }

    }
}
