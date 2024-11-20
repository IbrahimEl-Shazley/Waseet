using AAITHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.Interfaces.Rent;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;

namespace Wasit.Services.Implementations.Generic.ConsumerEstates
{
    public class ConsumerRentEstatesService : BaseService<RentEstate, RentEstateDto, CreateRentEstateDto, UpdateRentEstateDto>, IConsumerRentEstatesService, IConsumerSharedEstatesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentEstateRepository _rentEstateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public ConsumerRentEstatesService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
            _rentEstateRepository = uow.Repository<IRentEstateRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _userRepository = (IUserRepository)serviceProvider.GetService(typeof(IUserRepository));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
        }


        public async Task<dynamic> ListEstates(string userId, ListEstatesPayload payload)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id.Equals(userId)) ??
                            throw new BussinessRuleException("UserNotExist");

            IEnumerable<RentEstate> data;
            var skip = (payload.PageNumber - 1) * MyConstants.PageSize;

            var query = GetQuery(x => x.UserId != userId && x.IsShow && x.IsReviewed, false,
                [x => x.Region.City, x => x.User]);

            if (payload.EstateTypeId.HasValue && payload.EstateTypeId != 0)
                query = query.Where(x => x.EstateTypeId == payload.EstateTypeId);

            if (!string.IsNullOrWhiteSpace(payload.PublisherId))
                query = query.Where(x => x.UserId == payload.PublisherId);

            if (!string.IsNullOrWhiteSpace(payload.Search))
                query = query.Where(x => x.EstateName.Contains(payload.Search) || x.EstateNumber.Contains(payload.Search));

            if (payload.CityId.HasValue)
                query = query.Where(x => x.Region.CityId == payload.CityId);

            if (payload.StartPrice.HasValue)
                query = query.Where(x => x.MonthRentPrice >= payload.StartPrice);

            if (payload.EndPrice.HasValue)
                query = query.Where(x => x.MonthRentPrice <= payload.EndPrice);

            data = await query.OrderByDescending(x => x.Id).Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            if (payload.OrderByDistanceDesc.HasValue)
                data = data.OrderByDescending(x => GeneralHelper.GetDistance(x.Lat, x.Lng, user.Lat, user.Lng));

            if (payload.OrderByDistanceAsc.HasValue)
                data = data.OrderBy(x => GeneralHelper.GetDistance(x.Lat, x.Lng, user.Lat, user.Lng));

            var result = new PageDTO<RentEstateDto>
            {
                CurrentPage = payload.PageNumber,
                Count = data.Count(),
                TotalCount = await query.CountAsync(),
                Data = _mapper.Map<List<RentEstateDto>>(data)
            };

            return result;
        }


        public async Task<ConsumerRentEstateInfoDto> RentEstateInfo(long estateId)
        {
            try
            {
                var rentEstate = await _rentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId, false, [x => x.Images, x => x.SpecificationValues]) ??
                    throw new BussinessRuleException("EstateNotFound");

                var addReservationRequestCost = await _rentEstateRepository
                            .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.AddReservationRequest && x.IsActive, false)
                            .Select(x => x.ServiceCost)
                            .FirstOrDefaultAsync();

                var assignDelegateCost = await _rentEstateRepository
                           .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.AddRatingRequest && x.IsActive, false)
                           .Select(x => x.ServiceCost)
                           .FirstOrDefaultAsync();

                //var managementPhoneNumber = await _saleEstateRepository
                //            .GetQuery<Setting>(x => true, false)
                //            .Select(x => x.ManagementPhoneNumber)
                //            .FirstOrDefaultAsync();

                var result = _mapper.Map<ConsumerRentEstateInfoDto>(rentEstate);
                result.ManagementPhoneNumber = "0000000000000000";
                result.AssignDelegateCost = assignDelegateCost * rentEstate.EstateArea;
                result.AddReservationRequestCost = addReservationRequestCost;

                return result;
            }
            catch
            {
                throw new BussinessRuleException("InternalServerErrorOccured");
            }
        }


        public async Task<bool> AddOrRemoveFavouriteEstate(string userId, long estateId)
        {
            var estate = await _rentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId, false, x => x.Favorites) ??
                           throw new BussinessRuleException("EstateNotFound");


            if (estate.Favorites.Any(x => x.RentEstateId == estateId && x.UserId == userId))
                await _rentEstateRepository.RemoveAsync<RentEstateFavorite>(x => x.UserId == userId && x.RentEstateId == estateId);
            else
                await _rentEstateRepository.AddAsync(new RentEstateFavorite
                {
                    RentEstateId = estateId,
                    UserId = userId
                });

            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> AssignDelegate(long estateId, TypePay paymentMethod, string userId)
        {
            var estate = await _rentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId != userId, false) ??
                            throw new BussinessRuleException("EstateNotFound");

            if (estate.IsReserved && estate.ReservationRequests.Any(x => x.UserId != userId && x.ReservationStatus == ReservationStatus.Current))
                throw new BussinessRuleException("EstateAlreadyReserved");


            if (estate.RatingRequests.Any(x => x.UserId == userId && x.RatingStatus != RatingStatus.Finished))
                throw new BussinessRuleException("RequestAlreadySent");

            var cost = await _rentEstateRepository
                            .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.AddRatingRequest, false)
                            .Select(x => x.ServiceCost)
                            .FirstOrDefaultAsync();
            var request = new RentRatingRequest
            {
                IsPay = true,
                RentEstateId = estateId,
                TypePay = paymentMethod,
                ServiceCost = cost * estate.EstateArea,
                UserId = userId,
                RatingStatus = RatingStatus.New
            };

            await _rentEstateRepository.AddAsync(request);
            await _unitOfWork.SaveChangeAsync();

            var allDelegates = await _userRepository
                .GetUser<ApplicationDbUser>(x => (x.UserType == "Broker" || x.UserType == "Delegate") && x.IsActive && !x.IsDeleted, false)
                .ToListAsync();
            var eligibleDelegates = allDelegates.Where(x => x.IsEligibleDelegate(estate));

            foreach (var user in eligibleDelegates)
            {
                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"لديك طلب تقييم جديد للعقار رقم {estate.EstateNumber}",
                    TextEn = $"You have a new evaluation request for estate number {estate.EstateNumber}",
                    UserId = user.Id,
                    Type = NotifyTypes.NewEvaluationRequest,
                    CategoryType = CategoryType.Rent,
                    RouteId = request.Id
                });
            }

            return true;
        }


        public async Task<bool> AddReservationRequest(Language lang, long estateId, TypePay paymentMethod, string userId)
        {
            var estate = await _rentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId != userId, false) ??
                throw new BussinessRuleException("EstateNotFound");

            if (estate.IsReserved)
                throw new BussinessRuleException("EstateAlreadyReserved");

            var cost = await _rentEstateRepository
                            .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.AddReservationRequest, false)
                            .Select(x => x.ServiceCost)
                            .FirstOrDefaultAsync();

            var request = new RentReservationRequest
            {
                IsPay = true,
                RentEstateId = estateId,
                TypePay = paymentMethod,
                ServiceCost = cost,
                UserId = userId,
                ReservationStatus = ReservationStatus.Current,
                ReservationDate = HelperDate.GetCurrentDate()
            };

            await _rentEstateRepository.AddAsync(request);
            estate.IsReserved = true;
            _rentEstateRepository.Update(estate);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم حجز عقارك رقم {estate.EstateNumber} لمدة 24 ساعة",
                TextEn = $"Your estate with number {estate.EstateNumber} has been reserved for 24 hours",
                UserId = estate.UserId,
                Type = NotifyTypes.NewReservationRequest,
                CategoryType = CategoryType.Rent,
                RouteId = request.Id
            });

            return true;
        }


        public async Task<bool> AddRentRequest(string userId, long estateId, DateTime startDate, int monthCount)
        {
            var estate = await _rentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId != userId, false) ??
                throw new BussinessRuleException("EstateNotFound");

            if (estate.IsReserved && estate.ReservationRequests.Any(x => x.UserId != userId && x.ReservationStatus == ReservationStatus.Current && x.ReservationDate.Date == startDate.Date))
                throw new BussinessRuleException("EstateAlreadyReserved");

            if (monthCount < 3)
                throw new BussinessRuleException("MonthCountMustBeMoreThan3");

            var now = HelperDate.GetCurrentDate();
            if (estate.RentRequests.Any(x => x.UserId == userId && x.IsAccepted && now <= x.DepartureDate))
                throw new BussinessRuleException("RequestAlreadySent");

            var request = new RentRequest
            {
                RentEstateId = estateId,
                Status = RentStatus.New,
                UserId = userId,
                YearCount = monthCount / 12,
                MonthCount = monthCount % 12,
                TotalPrice = estate.MonthRentPrice * monthCount,
                ArrivalDate = startDate,
                DepartureDate = startDate.AddMonths(monthCount)
            };

            await _rentEstateRepository.AddAsync(request);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"لديك طلب ايجار جديد للعقار رقم {estate.EstateNumber}",
                TextEn = $"You have a new rent request for estate number {estate.EstateNumber}",
                UserId = estate.UserId,
                Type = NotifyTypes.NewRentRequest,
                CategoryType = CategoryType.Rent,
                RouteId = request.Id
            });

            return true;
        }

        public Task<PageDTO<BaseRatingItemDto>> AllUserRatingsOnEstate(long estateId, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReportComment(string userId, long requestId, string reason)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> ReservedDays(long estateId)
        {
            throw new NotImplementedException();
        }
    }
}
