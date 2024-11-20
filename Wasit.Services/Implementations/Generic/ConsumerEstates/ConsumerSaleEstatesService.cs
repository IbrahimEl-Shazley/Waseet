using AAITHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.AddPriceToEstate;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.Interfaces.Sale;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;
using Wasit.Services.DTOs.Schema.Sale.UserPriceToEstate;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;

namespace Wasit.Services.Implementations.Generic.ConsumerEstates
{
    public class ConsumerSaleEstatesService : BaseService<SaleEstate, SaleEstateDto, CreateSaleEstateDto, UpdateSaleEstateDto>, IConsumerSaleEstatesService, IConsumerSharedEstatesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleEstateRepository _saleEstateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public ConsumerSaleEstatesService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
            _saleEstateRepository = uow.Repository<ISaleEstateRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _userRepository = (IUserRepository)serviceProvider.GetService(typeof(IUserRepository));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
        }



        public async Task<dynamic> ListEstates(string userId, ListEstatesPayload payload)
        {
            var user = await _userRepository.UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id.Equals(userId)) ??
                throw new BussinessRuleException("UserNotExist");

            IEnumerable<SaleEstate> data;
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
                query = query.Where(x => x.EstatePrice >= payload.StartPrice);

            if (payload.EndPrice.HasValue)
                query = query.Where(x => x.EstatePrice <= payload.EndPrice);

            data = await query.OrderByDescending(x => x.Id).Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            if (payload.OrderByDistanceDesc.HasValue)
                data = data.OrderByDescending(x => GeneralHelper.GetDistance(x.Lat, x.Lng, user.Lat, user.Lng));

            if (payload.OrderByDistanceAsc.HasValue)
                data = data.OrderBy(x => GeneralHelper.GetDistance(x.Lat, x.Lng, user.Lat, user.Lng));

            var result = new PageDTO<SaleEstateDto>
            {
                CurrentPage = payload.PageNumber,
                Count = data.Count(),
                TotalCount = await query.CountAsync(),
                Data = _mapper.Map<List<SaleEstateDto>>(data)
            };

            return result;
        }


        public async Task<ConsumerSaleEstateInfoDto> SaleEstateInfo(string userId, long estateId)
        {
            try
            {
                var saleEstate = await _saleEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId, false, [x => x.UserPriceToEstates, x => x.PricingRequests, x => x.Images, x => x.SpecificationValues]) ??
                    throw new BussinessRuleException("EstateNotFound");

                var assignDelegateCost = await _saleEstateRepository
                            .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.AddRatingRequest && x.IsActive, false)
                            .Select(x => x.ServiceCost)
                            .FirstOrDefaultAsync();

                var addReservationRequestCost = await _saleEstateRepository
                            .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.AddReservationRequest && x.IsActive, false)
                            .Select(x => x.ServiceCost)
                            .FirstOrDefaultAsync();

                //var managementPhoneNumber = await _saleEstateRepository
                //            .GetQuery<Setting>(x => true, false)
                //            .Select(x => x.ManagementPhoneNumber)
                //            .FirstOrDefaultAsync();

                var result = _mapper.Map<ConsumerSaleEstateInfoDto>(saleEstate);

                result.AssignDelegateCost = assignDelegateCost * saleEstate.EstateArea;
                result.AddReservationRequestCost = addReservationRequestCost;
                result.ManagementPhoneNumber = "0000000000000000";
                result.IsSubscribedToAddPrice = await _saleEstateRepository.AnyAsync<S4AddPriceToEstate>(x => x.UserId == userId && x.IsActive);
                return result;
            }
            catch
            {
                throw new BussinessRuleException("InternalServerErrorOccured");
            }
        }


        public async Task<bool> AddOrRemoveFavouriteEstate(string userId, long estateId)
        {
            var estate = await _saleEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId, false, x => x.Favorites) ??
                throw new BussinessRuleException("EstateNotFound");


            if (estate.Favorites.Any(x => x.SaleEstateId == estateId && x.UserId == userId))
                await _saleEstateRepository.RemoveAsync<SaleEstateFavorite>(x => x.SaleEstateId == estateId && x.UserId == userId);
            else
                await _saleEstateRepository.AddAsync(new SaleEstateFavorite
                {
                    SaleEstateId = estateId,
                    UserId = userId
                });

            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<List<AddPricePackageItemDto>> ListP4AddPriceToEstate()
        {
            var packages = await _saleEstateRepository.GetListAsync<P4AddPriceToEstate>(x => x.IsActive, false);

            return _mapper.Map<List<AddPricePackageItemDto>>(packages);
        }


        public async Task<bool> SubscribeToAddPriceToEstatePackage(long packageId, TypePay paymentMethod)
        {
            var package = await _saleEstateRepository.FirstOrDefaultAsync<P4AddPriceToEstate>(x => x.Id == packageId && x.IsActive, false) ??
                throw new BussinessRuleException("PackageNotFound");

            var subscription = _mapper.Map<S4AddPriceToEstate>(package);
            subscription.TypePay = paymentMethod;
            subscription.IsPay = true;

            await _saleEstateRepository.AddAsync(subscription);
            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> AddPriceToEstate(long estateId, double price, string userId)
        {
            var estate = await _saleEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId != userId, false) ??
                throw new BussinessRuleException("EstateNotFound");

            var supscription = await _saleEstateRepository.FirstOrDefaultAsync<S4AddPriceToEstate>(x => x.UserId == userId && x.IsActive) ??
                throw new BussinessRuleException("SubscriptionNotFound");

            var now = HelperDate.GetCurrentDate();
            if (supscription.ExpireDate < now || supscription.RemainingAddPriceCount == 0)
                throw new BussinessRuleException("SubscriptionExpired");

            if (price <= estate.EstatePrice * 0.45)
                throw new BussinessRuleException("PriceTooLowThan45Percent");

            await _saleEstateRepository.AddAsync(new UserPriceToEstate
            {
                UserId = userId,
                SaleEstateId = estateId,
                Price = price
            });

            supscription.RemainingAddPriceCount -= 1;
            _saleEstateRepository.Update(supscription);

            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم اضافة سومة جديدة للعقار رقم {estate.EstateNumber}",
                TextEn = $"New price added to estate number {estate.EstateNumber}",
                UserId = estate.UserId,
                Type = NotifyTypes.NewPriceToEstate,
                CategoryType = CategoryType.Sale,
                RouteId = estateId
            });

            return true;
        }


        public async Task<long> AddPurchaseRequest(long estateId, string userId)
        {
            var estate = await _saleEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId != userId, false) ??
                throw new BussinessRuleException("EstateNotFound");

            if (estate.IsReserved && estate.ReservationRequests.Any(x => x.UserId != userId && x.ReservationStatus == ReservationStatus.Current))
                throw new BussinessRuleException("EstateAlreadyReserved");

            if (estate.PurchaseRequests.Any(x => x.UserId != userId && x.Status == PurchaseStatus.Current))
                throw new BussinessRuleException("SomeoneElseHasCurrentPurchaseRequest");

            if (estate.PurchaseRequests.Any(x => x.UserId == userId && (x.Status != PurchaseStatus.Finished && x.Status != PurchaseStatus.Canceled)))
                throw new BussinessRuleException("RequestAlreadySent");

            if (estate.Deposit == 0)
                throw new BussinessRuleException("EstateHasNoDeposit");

            var request = new PurchaseRequest
            {
                SaleEstateId = estateId,
                Status = PurchaseStatus.New,
                UserId = userId
            };
            await _saleEstateRepository.AddAsync(request);

            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"لديك طلب شراء جديد للعقار رقم {estate.EstateNumber}",
                TextEn = $"You have a new purchase request for estate number {estate.EstateNumber}",
                UserId = estate.UserId,
                Type = NotifyTypes.NewPurchaseRequest,
                CategoryType = CategoryType.Sale,
                RouteId = request.Id
            });

            return request.Id;
        }


        public async Task<bool> AssignDelegate(long estateId, TypePay paymentMethod, string userId)
        {
            var estate = await _saleEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId != userId, false) ??
                throw new BussinessRuleException("EstateNotFound");

            if (estate.IsReserved && estate.ReservationRequests.Any(x => x.UserId != userId && x.ReservationStatus == ReservationStatus.Current))
                throw new BussinessRuleException("EstateAlreadyReserved");

            if (estate.RatingRequests.Any(x => x.UserId == userId && x.RatingStatus != RatingStatus.Finished))
                throw new BussinessRuleException("RequestAlreadySent");

            var cost = await _saleEstateRepository
                            .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.AddRatingRequest, false)
                            .Select(x => x.ServiceCost)
                            .FirstOrDefaultAsync();

            var request = new SaleRatingRequest
            {
                IsPay = true,
                SaleEstateId = estateId,
                TypePay = paymentMethod,
                ServiceCost = cost * estate.EstateArea,
                UserId = userId,
                RatingStatus = RatingStatus.New
            };

            await _saleEstateRepository.AddAsync(request);
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
                    CategoryType = CategoryType.Sale,
                    RouteId = request.Id
                });
            }

            return true;
        }


        public async Task<bool> AddReservationRequest(Language lang, long estateId, TypePay paymentMethod, string userId)
        {
            var estate = await _saleEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId != userId, false) ??
                throw new BussinessRuleException("EstateNotFound");

            if (estate.IsReserved)
                throw new BussinessRuleException("EstateAlreadyReserved");

            var cost = await _saleEstateRepository
                            .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.AddReservationRequest, false)
                            .Select(x => x.ServiceCost)
                            .FirstOrDefaultAsync();

            var request = new SaleReservationRequest
            {
                IsPay = true,
                SaleEstateId = estateId,
                TypePay = paymentMethod,
                ServiceCost = cost,
                UserId = userId,
                ReservationStatus = ReservationStatus.Current,
                ReservationDate = HelperDate.GetCurrentDate()
            };

            await _saleEstateRepository.AddAsync(request);

            estate.IsReserved = true;
            _saleEstateRepository.Update(estate);

            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم حجز عقارك رقم {estate.EstateNumber} لمدة 24 ساعة",
                TextEn = $"Your estate with number {estate.EstateNumber} has been reserved for 24 hours",
                UserId = estate.UserId,
                Type = NotifyTypes.NewReservationRequest,
                CategoryType = CategoryType.Sale,
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
