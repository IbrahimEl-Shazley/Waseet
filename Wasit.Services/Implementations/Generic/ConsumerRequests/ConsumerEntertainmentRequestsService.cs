using AAITHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.Interfaces.Entertainment;
using Wasit.Repositories.UnitOfWork;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentRentRequest;
using Wasit.Services.DTOs.Schema.Shared.ConsumerRequests;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.ConsumerRequests;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.Services.Implementations.Generic.ConsumerRequests
{
    public class ConsumerEntertainmentRequestsService : BaseService, IConsumerEntertainmentRequestsService, IConsumerSharedRequestsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntertainmentEstateRepository _entertainmentEstateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly INotificationService _notificationService;
        private readonly IGeneralService _generalService;

        public ConsumerEntertainmentRequestsService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _entertainmentEstateRepository = uow.Repository<IEntertainmentEstateRepository>();
            _userRepository = uow.Repository<IUserRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _currentUserService = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
            _generalService = (IGeneralService)serviceProvider.GetService(typeof(IGeneralService));
        }


        public async Task<PageDTO<ConsumerEntertainmentCategoryRequestDto>> ListRequests(string userId, ListEntertainmentRequestsPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;
            var query = _entertainmentEstateRepository
                .GetQuery<EntertainmentRequest>(x => x.UserId == userId && x.Status == (DailyRentStatus)model.Status, false)
                .OrderByDescending(x => x.Id);
            var data = await query.Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            var culture = DateTimeHelper.GetCulture(_currentUserService.Language);

            return new PageDTO<ConsumerEntertainmentCategoryRequestDto>
            {
                CurrentPage = model.PageNumber,
                TotalCount = await query.CountAsync(),
                Count = data.Count,
                Data = _mapper.Map<List<ConsumerEntertainmentCategoryRequestDto>>(data, o => o.Items["culture"] = culture)
            };
        }


        public async Task<ExtendedConsumerEntertainmentCategoryRequestDto> RequestInfo(string userId, long requestId)
        {
            var request = await _entertainmentEstateRepository.FirstOrDefaultAsync<EntertainmentRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            var culture = DateTimeHelper.GetCulture(_currentUserService.Language);

            return _mapper.Map<ExtendedConsumerEntertainmentCategoryRequestDto>(request, o => o.Items["culture"] = culture);
        }



        public async Task<bool> CancelEntertainmentRequest(string userId, long requestId)
        {
            var request = await _entertainmentEstateRepository.FirstOrDefaultAsync<EntertainmentRequest>(x => x.Id == requestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            if (request.Status != DailyRentStatus.New)
                throw new BussinessRuleException("EntertainmentRequestStatusMustBeNewToBeCanceld");

            request.Status = DailyRentStatus.Canceled;
            request.CancelDate = HelperDate.GetCurrentDate();

            var now = HelperDate.GetCurrentDate();
            if (now - request.ArrivalDate < TimeSpan.FromDays(2))
            {
                var cancelationCost = await _entertainmentEstateRepository
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

            _entertainmentEstateRepository.Update(request);
            _userRepository.UpdateUser(request.User);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {request.User.User_Name} بالغاء طلب الايجار اليومي للعقار رقم {request.EntertainmentEstate.EstateNumber}",
                TextEn = $"{request.User.User_Name} has canceled the daily rent request for the estate number {request.EntertainmentEstate.EstateNumber}",
                Type = NotifyTypes.CancelEntertainmentRentRequest,
                UserId = request.EntertainmentEstate.UserId,
                CategoryType = CategoryType.Entertainment,
                RouteId = request.Id
            });

            return true;
        }


        public async Task<bool> RateOwnerAndEstate(string userId, RateOwnerAndEstatePayload payload)
        {
            var request = await _entertainmentEstateRepository.FirstOrDefaultAsync<EntertainmentRequest>(x => x.Id == payload.RequestId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            if (request.Status != DailyRentStatus.Finished)
                throw new BussinessRuleException("EntertainmentRequestStatusMustBeNewToBeRated");

            request.UserRating = payload.OwnerRating;
            request.UserRatingDateTime = HelperDate.GetCurrentDate();
            request.EstateRating = payload.EstateRating;
            request.EstateComment = payload.Feedback;
            _entertainmentEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            request.EntertainmentEstate.User.Rating = await _generalService.CalcAverageUserRating(request.EntertainmentEstate.UserId);
            request.EntertainmentEstate.Rating = await _generalService.CalcAverageEstateRating(request.EntertainmentEstateId);
            _userRepository.UpdateUser(request.EntertainmentEstate.User);
            _entertainmentEstateRepository.Update(request.EntertainmentEstate);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {request.User.User_Name} بتقييمك",
                TextEn = $"User {request.User.User_Name} has rated you",
                UserId = request.EntertainmentEstate.UserId,
                Type = NotifyTypes.NewRatingForPublisher,
                CategoryType = CategoryType.Entertainment,
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
