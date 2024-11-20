using AAITHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.ExtensionsMethods;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.Interfaces.Daily;
using Wasit.Repositories.UnitOfWork;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;

namespace Wasit.Services.Implementations.Generic.ConsumerEstates
{
    public class ConsumerDailyRentEstatesService : BaseService<DailyRentEstate, DailyRentEstateDto, CreateDailyRentEstateDto, UpdateDailyRentEstateDto>, IConsumerDailyRentEstatesService, IConsumerSharedEstatesService
    {
        private readonly IDailyRentEstateRepository _dailyRentEstateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly INotificationService _notificationService;

        public ConsumerDailyRentEstatesService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
            _dailyRentEstateRepository = uow.Repository<IDailyRentEstateRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _userRepository = (IUserRepository)serviceProvider.GetService(typeof(IUserRepository));
            _currentUserService = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
        }


        public async Task<dynamic> ListEstates(string userId, ListEstatesPayload payload)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id.Equals(userId)) ??
               throw new BussinessRuleException("UserNotExist");

            IEnumerable<DailyRentEstate> data;
            var skip = (payload.PageNumber - 1) * MyConstants.PageSize;

            var query = GetQuery(x => x.UserId != userId && x.IsShow && x.IsReviewed, false,
                [x => x.Region.City, x => x.EstateType, x => x.User]);

            if (payload.EstateTypeId.HasValue && payload.EstateTypeId != 0)
                query = query.Where(x => x.EstateTypeId == payload.EstateTypeId);

            if (!string.IsNullOrWhiteSpace(payload.PublisherId))
                query = query.Where(x => x.UserId == payload.PublisherId);

            if (!string.IsNullOrWhiteSpace(payload.Search))
                query = query.Where(x => x.EstateName.Contains(payload.Search) || x.EstateNumber.Contains(payload.Search));

            if (payload.CityId.HasValue)
                query = query.Where(x => x.Region.CityId == payload.CityId);

            if (payload.StartPrice.HasValue)
                query = query.Where(x => x.DayRentPrice >= payload.StartPrice);

            if (payload.EndPrice.HasValue)
                query = query.Where(x => x.DayRentPrice <= payload.EndPrice);

            data = await query.OrderByDescending(x => x.Id).Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            if (payload.OrderByDistanceDesc.HasValue)
                data = data.OrderByDescending(x => GeneralHelper.GetDistance(x.Lat, x.Lng, user.Lat, user.Lng));

            if (payload.OrderByDistanceAsc.HasValue)
                data = data.OrderBy(x => GeneralHelper.GetDistance(x.Lat, x.Lng, user.Lat, user.Lng));

            var result = new PageDTO<DailyRentEstateDto>
            {
                CurrentPage = payload.PageNumber,
                Count = data.Count(),
                TotalCount = await query.CountAsync(),
                Data = _mapper.Map<List<DailyRentEstateDto>>(data)
            };

            return result;
        }


        public async Task<ConsumerDailyRentEstateInfoDto> DailyRentEstateInfo(long estateId)
        {
            try
            {
                var dailyRentEstate = await _dailyRentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId, false, [x => x.Images, x => x.SpecificationValues, x => x.Requests]) ??
                    throw new BussinessRuleException("EstateNotFound");

                var culture = DateTimeHelper.GetCulture(_currentUserService.Language);

                //var managementPhoneNumber = await _saleEstateRepository
                //            .GetQuery<Setting>(x => true, false)
                //            .Select(x => x.ManagementPhoneNumber)
                //            .FirstOrDefaultAsync();

                var result = _mapper.Map<ConsumerDailyRentEstateInfoDto>(dailyRentEstate, o => o.Items["culture"] = culture);
                result.ManagementPhoneNumber = "0000000000000000";

                return result;
            }
            catch
            {
                throw new BussinessRuleException("InternalServerErrorOccured");
            }
        }


        public async Task<bool> AddOrRemoveFavouriteEstate(string userId, long estateId)
        {
            var estate = await _dailyRentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId, false, x => x.Favorites) ??
                                       throw new BussinessRuleException("EstateNotFound");

            if (estate.Favorites.Any(x => x.DailyRentEstateId == estateId && x.UserId == userId))
                await _dailyRentEstateRepository.RemoveAsync<DailyRentEstateFavorite>(x => x.DailyRentEstateId == estateId && x.UserId == userId);
            else
                await _dailyRentEstateRepository.AddAsync(new DailyRentEstateFavorite
                {
                    DailyRentEstateId = estateId,
                    UserId = userId
                });

            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> AddDailyRentRequest(string userId, long estateId, DateTime startDate, DateTime endDate, TypePay paymentMethod)
        {
            var estate = await _dailyRentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId != userId, false) ??
                throw new BussinessRuleException("EstateNotFound");

            if (!estate.BookingFrom.HasValue || !estate.BookingTo.HasValue)
                throw new BussinessRuleException("EstateNotAvailableForBooking");

            if (endDate < startDate)
                throw new BussinessRuleException("EndDateMustBeGreaterThanStartDate");

            if (!estate.IsDatesInRange(startDate, endDate))
                throw new BussinessRuleException("EstateNotAvailableForBookingInTheseDays");

            var now = HelperDate.GetCurrentDate();
            var days = endDate.Subtract(startDate).Days + 1;
            var request = new DailyRentRequest
            {
                IsPay = true,
                TypePay = paymentMethod,
                DailyRentEstateId = estateId,
                Status = startDate.Date == now.Date ? DailyRentStatus.Current : DailyRentStatus.New,
                UserId = userId,
                ArrivalDate = now.Date == startDate.Date ? now : startDate,
                LeaveDate = endDate.AddHours(12),
                TotalDays = days,
                PaymentDate = now,
                TotalPrice = days * estate.DayRentPrice
            };  

            await _dailyRentEstateRepository.AddAsync(request);
            await _unitOfWork.SaveChangeAsync();

            var userName = await _dailyRentEstateRepository.GetQuery<DailyRentRequest>(x => x.Id == request.Id)
                .Select(x => x.User.User_Name)
                .FirstOrDefaultAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {userName} بايجار العقار الخاص بك رقم {estate.EstateNumber} لمدة {days} يوم",
                TextEn = $"{userName} has rented your estate number {estate.EstateNumber} for {days} days",
                UserId = estate.UserId,
                Type = NotifyTypes.NewDailyRentRequest,
                CategoryType = CategoryType.DailyRent,
                RouteId = request.Id
            });

            return true;
        }


        public async Task<PageDTO<BaseRatingItemDto>> AllUserRatingsOnEstate(long estateId, int pageNumber)
        {
            var skip = MyConstants.PageSize * (pageNumber - 1);
            var query = _dailyRentEstateRepository.GetQuery<DailyRentRequest>(x => x.DailyRentEstateId == estateId && x.UserRatingDateTime.HasValue, false)
                .OrderByDescending(x => x.Id);
            var data = await query.Skip(skip).Take(MyConstants.PageSize).ToListAsync();
            var culture = DateTimeHelper.GetCulture(_currentUserService.Language);

            var result = new PageDTO<BaseRatingItemDto>
            {
                CurrentPage = pageNumber,
                Count = data.Count,
                TotalCount = await query.CountAsync(),
                Data = _mapper.Map<List<BaseRatingItemDto>>(data, o => o.Items["culture"] = culture)
            };

            return result;
        }


        public async Task<bool> ReportComment(string userId, long requestId, string reason)
        {
            var request = await _dailyRentEstateRepository.FirstOrDefaultAsync<DailyRentRequest>(x => x.Id == requestId) ??
                           throw new BussinessRuleException("RequestNotFound");

            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == userId) ??
                       throw new BussinessRuleException("UserNotFound");

            var report = new ReportDailyRentComment
            {
                DailyRentRequestId = requestId,
                ReasonForReport = reason,
                ReporterId = userId
            };

            await _dailyRentEstateRepository.AddAsync(report);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyToDashboardAsync(new NotificationModel
            {
                TextAr = $"قام المستخدم {user.User_Name} بالابلاغ عن تعليق مسيء",
                TextEn = $"User {user.User_Name} has reported a violated comment",
                Type = NotifyTypes.NewReport,
                CategoryType = CategoryType.DailyRent,
                RouteId = report.Id
            });

            return true;
        }


        public async Task<List<string>> ReservedDays(long estateId)
        {
            var now = HelperDate.GetCurrentDate(3).Date;
            var dailyRentRequests = await _dailyRentEstateRepository
                .GetQuery<DailyRentRequest>(x => x.DailyRentEstateId == estateId && x.ArrivalDate.Date >= now, false)
                .Select(x => new
                {
                    arrivalDate = x.ArrivalDate,
                    departureDate = x.LeaveDate
                }).ToListAsync();

            var result = new List<DateTime>();
            foreach (var request in dailyRentRequests)
            {
                var list = DateTimeHelper.GetDatesBetween(request.arrivalDate, request.departureDate);
                result.AddRange(list);
            }

            return result.Select(x => x.ToString("dd/MM/yyyy")).ToList();
        }

        public Task<bool> AssignDelegate(long estateId, TypePay paymentMethod, string userId)
        {
            throw new NotImplementedException();
        }


        public Task<bool> AddReservationRequest(Language lang, long estateId, TypePay paymentMethod, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
