using AAITHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.PropertiesManagement;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Enums;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.MyEstates;

namespace Wasit.Services.Implementations.Generic.MyEstates
{
    public class GeneralMySharedEstatesService : BaseService, IGeneralMySharedEstatesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository _baseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;


        public GeneralMySharedEstatesService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _baseRepository = unitOfWork.Repository<IBaseRepository>();
            _userRepository = unitOfWork.Repository<IUserRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
        }


        public async Task<IEnumerable<SpecificationFormItemDto>> GetSpecsForm(long estateTypeId)
        {
            var result = await _baseRepository
                .GetListAsync<EstateTypeSpecification>(x => x.EstateTypeId == estateTypeId, false, x => x.Specification);

            return _mapper.Map<List<SpecificationFormItemDto>>(result);
        }


        public async Task<bool> CheckReservationRequests()
        {
            var now = HelperDate.GetCurrentDate();

            // Process SaleReservationRequest
            var saleReservationRequests = await _baseRepository
                .GetQuery<SaleReservationRequest>(x => x.ReservationStatus == ReservationStatus.Current && x.IsPay && now >= x.ReservationDate.AddHours(24), true, x => x.SaleEstate)
                .ToListAsync();

            foreach (var request in saleReservationRequests)
            {
                request.ReservationStatus = ReservationStatus.Finished;
                request.SaleEstate.IsReserved = false;

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم انتهاء مدة الحجز للعقار رقم {request.SaleEstate.EstateNumber}",
                    TextEn = $"The reservation period for property number {request.SaleEstate.EstateNumber} has ended",
                    UserId = request.UserId,
                    Type = NotifyTypes.FinishedReservationRequestConsumer,
                    CategoryType = CategoryType.Sale,
                    RouteId = request.Id
                });

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم انتهاء مدة الحجز للعقار رقم {request.SaleEstate.EstateNumber}",
                    TextEn = $"The reservation period for property number {request.SaleEstate.EstateNumber} has ended",
                    UserId = request.SaleEstate.UserId,
                    Type = NotifyTypes.FinishedReservationRequestPublisher,
                    CategoryType = CategoryType.Sale,
                    RouteId = request.Id
                });
            }

            // Process RentReservationRequest
            var rentReservationRequests = await _baseRepository
                .GetQuery<RentReservationRequest>(x => x.ReservationStatus == ReservationStatus.Current && x.IsPay && now >= x.ReservationDate.AddHours(24), true, x => x.RentEstate)
                .ToListAsync();

            foreach (var request in rentReservationRequests)
            {
                request.ReservationStatus = ReservationStatus.Finished;
                request.RentEstate.IsReserved = false;

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم انتهاء مدة الحجز للعقار رقم {request.RentEstate.EstateNumber}",
                    TextEn = $"The reservation period for property number {request.RentEstate.EstateNumber} has ended",
                    UserId = request.UserId,
                    Type = NotifyTypes.FinishedReservationRequestConsumer,
                    CategoryType = CategoryType.Rent,
                    RouteId = request.Id
                });

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم انتهاء مدة الحجز للعقار رقم {request.RentEstate.EstateNumber}",
                    TextEn = $"The reservation period for property number {request.RentEstate.EstateNumber} has ended",
                    UserId = request.RentEstate.UserId,
                    Type = NotifyTypes.FinishedReservationRequestPublisher,
                    CategoryType = CategoryType.Rent,
                    RouteId = request.Id
                });
            }

            return await _unitOfWork.SaveChangeAsync();
        }



        public async Task<bool> CheckShortTermRentRequests()
        {
            var now = HelperDate.GetCurrentDate();

            var newDailyRentRequests = await _baseRepository
                 .GetQuery<DailyRentRequest>(x => x.Status == DailyRentStatus.New && x.IsPay && now >= x.ArrivalDate)
                 .ToListAsync();
            foreach (var request in newDailyRentRequests)
            {
                request.Status = DailyRentStatus.Current;

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم بدء ايجارك للعقار رقم {request.DailyRentEstate.EstateNumber}",
                    TextEn = $"The rent for property number {request.DailyRentEstate.EstateNumber} has started",
                    UserId = request.UserId,
                    Type = NotifyTypes.DailyOrEntertainmentRentStartedConsumer,
                    CategoryType = CategoryType.DailyRent,
                    RouteId = request.Id
                });

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم بدء ايجار {request.User.User_Name} للعقار رقم {request.DailyRentEstate.EstateNumber}",
                    TextEn = $"{request.User.User_Name}'s rent for property number {request.DailyRentEstate.EstateNumber} has started",
                    UserId = request.DailyRentEstate.UserId,
                    Type = NotifyTypes.DailyOrEntertainmentRentStartedPublisher,
                    CategoryType = CategoryType.DailyRent,
                    RouteId = request.Id
                });
            }

            var newEntertainmentRequests = await _baseRepository
                 .GetQuery<EntertainmentRequest>(x => x.Status == DailyRentStatus.New && x.IsPay && now >= x.ArrivalDate)
                 .ToListAsync();
            foreach (var request in newEntertainmentRequests)
            {
                request.Status = DailyRentStatus.Current;

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم بدء ايجارك للعقار رقم {request.EntertainmentEstate.EstateNumber}",
                    TextEn = $"The rent for property number {request.EntertainmentEstate.EstateNumber} has started",
                    UserId = request.UserId,
                    Type = NotifyTypes.DailyOrEntertainmentRentStartedConsumer,
                    CategoryType = CategoryType.Entertainment,
                    RouteId = request.Id
                });

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم بدء ايجار {request.User.User_Name} للعقار رقم {request.EntertainmentEstate.EstateNumber}",
                    TextEn = $"{request.User.User_Name}'s rent for property number {request.EntertainmentEstate.EstateNumber} has started",
                    UserId = request.EntertainmentEstate.UserId,
                    Type = NotifyTypes.DailyOrEntertainmentRentStartedPublisher,
                    CategoryType = CategoryType.Entertainment,
                    RouteId = request.Id
                });
            }


            ////////////////////////////////////////////////////////////////////////////////////


            var currentDailyRentRequests = await _baseRepository
                 .GetQuery<DailyRentRequest>(x => x.Status == DailyRentStatus.Current && x.IsPay && now >= x.LeaveDate)
                 .ToListAsync();
            foreach (var request in currentDailyRentRequests)
            {
                request.Status = DailyRentStatus.Finished;

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم انتهاء ايجارك للعقار رقم {request.DailyRentEstate.EstateNumber}",
                    TextEn = $"The rent for property number {request.DailyRentEstate.EstateNumber} has finished",
                    UserId = request.UserId,
                    Type = NotifyTypes.DailyOrEntertainmentRentFinishedConsumer,
                    CategoryType = CategoryType.DailyRent,
                    RouteId = request.Id
                });

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم انتهاء ايجار {request.User.User_Name} للعقار رقم {request.DailyRentEstate.EstateNumber}",
                    TextEn = $"{request.User.User_Name}'s rent for property number {request.DailyRentEstate.EstateNumber} has finished",
                    UserId = request.DailyRentEstate.UserId,
                    Type = NotifyTypes.DailyOrEntertainmentRentFinishedPublisher,
                    CategoryType = CategoryType.DailyRent,
                    RouteId = request.Id
                });
            }


            var currentEntertainmentRequests = await _baseRepository
                 .GetQuery<EntertainmentRequest>(x => x.Status == DailyRentStatus.Current && x.IsPay && now >= x.LeaveDate)
                 .ToListAsync();
            foreach (var request in currentEntertainmentRequests)
            {
                request.Status = DailyRentStatus.Finished;

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم انتهاء ايجارك للعقار رقم {request.EntertainmentEstate.EstateNumber}",
                    TextEn = $"The rent for property number {request.EntertainmentEstate.EstateNumber} has finished",
                    UserId = request.UserId,
                    Type = NotifyTypes.DailyOrEntertainmentRentFinishedConsumer,
                    CategoryType = CategoryType.Entertainment,
                    RouteId = request.Id
                });

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم انتهاء ايجار {request.User.User_Name} للعقار رقم {request.EntertainmentEstate.EstateNumber}",
                    TextEn = $"{request.User.User_Name}'s rent for property number {request.EntertainmentEstate.EstateNumber} has finished",
                    UserId = request.EntertainmentEstate.UserId,
                    Type = NotifyTypes.DailyOrEntertainmentRentFinishedPublisher,
                    CategoryType = CategoryType.Entertainment,
                    RouteId = request.Id
                });
            }

            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> CheckApartmentsRentStatus()
        {
            var currentMonth = HelperDate.GetCurrentDate().Month;

            await _baseRepository
                 .GetQuery<EstateApartment>(x => x.IsRentPaid && currentMonth != x.ApartmentRentPays.OrderBy(x => x.Id).LastOrDefault().PaymentDate.Month)
                 .ForEachAsync(x => x.IsRentPaid = false);

            return await _unitOfWork.SaveChangeAsync();
        }
        
        
        public async Task<bool> CheckUnAcceptedEvaluationRequests()
        {
            var now = HelperDate.GetCurrentDate(3);

            var unAcceptedSaleEvaluationRequests = await _baseRepository
                 .GetQuery<SaleRatingRequest>(x => x.RatingStatus == RatingStatus.New, true, x => x.User)
                 .ToListAsync();
            foreach(var request in unAcceptedSaleEvaluationRequests.Where(x => now - x.CreatedOn.Value >= TimeSpan.FromDays(3)))
            {
                request.IsDeleted = true;
                request.RatingStatus = RatingStatus.Finished;
                request.User.Wallet += request.ServiceCost;
                _userRepository.UpdateUser(request.User);


                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم حذف طلب التقييم العقاري الخاص بك للعقار رقم {request.SaleEstate.EstateNumber} لعدم قبول الطلب خلال 3 ايام من ارساله",
                    TextEn = $"Your evaluation request for property number {request.SaleEstate.EstateNumber} has been deleted because it was not accepted within 3 days",
                    UserId = request.UserId,
                    Type = NotifyTypes.EvaluationRequestIsDeletedConsumer,
                    CategoryType = CategoryType.Sale,
                    RouteId = request.Id
                });
            }
            await _unitOfWork.SaveChangeAsync();


            var unAcceptedRentEvaluationRequests = await _baseRepository
                 .GetQuery<RentRatingRequest>(x => x.RatingStatus == RatingStatus.New, true, x => x.User)
                 .ToListAsync();
            foreach (var request in unAcceptedRentEvaluationRequests.Where(x => now - x.CreatedOn.Value >= TimeSpan.FromDays(3)))
            {
                request.IsDeleted = true;
                request.RatingStatus = RatingStatus.Finished;
                request.User.Wallet += request.ServiceCost;
                _userRepository.UpdateUser(request.User);
                

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم الغاء طلب التقييم العقاري الخاص بك للعقار رقم {request.RentEstate.EstateNumber} لعدم قبول الطلب خلال 3 ايام من ارساله",
                    TextEn = $"Your evaluation request for property number {request.RentEstate.EstateNumber} has been canceled because it was not accepted within 3 days",
                    UserId = request.UserId,
                    Type = NotifyTypes.EvaluationRequestIsDeletedConsumer,
                    CategoryType = CategoryType.Rent,
                    RouteId = request.Id
                });
            }
            await _unitOfWork.SaveChangeAsync();

            return true;
        }


        public async Task<bool> CheckLongTermRentRequests()
        {
            var now = HelperDate.GetCurrentDate().Date;

            var rentRequests = await _baseRepository
                .GetQuery<RentRequest>(x => x.Status == RentStatus.Finished && x.RentEstate.IsRented && now >= x.DepartureDate.Date, true, x => x.RentEstate)
                .ToListAsync();

            foreach (var request in rentRequests)
            {
                request.RentEstate.IsRented = false;

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم انتهاء مدة ايجارك للعقار رقم {request.RentEstate.EstateNumber}",
                    TextEn = $"The rent period for property number {request.RentEstate.EstateNumber} has ended",
                    UserId = request.UserId,
                    Type = NotifyTypes.FinishedLongRentPeriodConsumer,
                    CategoryType = CategoryType.Rent,
                    RouteId = request.Id
                });

                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"تم انتهاء مدة الايجار للعقار رقم {request.RentEstate.EstateNumber}",
                    TextEn = $"The rent period for property number {request.RentEstate.EstateNumber} has ended",
                    UserId = request.RentEstate.UserId,
                    Type = NotifyTypes.FinishedLongRentPeriodPublisher,
                    CategoryType = CategoryType.Rent,
                    RouteId = request.Id
                });
            }

            return await _unitOfWork.SaveChangeAsync();
        }
        
        
        public async Task<bool> CheckAllowedBookingPeriodForShortRentEstates()
        {
            var dailyRentEstates = await _baseRepository
                .GetQuery<DailyRentEstate>(x => !x.BookingFrom.HasValue && !x.BookingTo.HasValue)
                .ToListAsync();
            foreach (var estate in dailyRentEstates)
            {
                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"يرجي تحديدة فترة حجز للعقار رقم {estate.EstateNumber}، حتي يتمكن المستخدمين من ارسال طلبات حجز",
                    TextEn = $"Please set a booking period for property number {estate.EstateNumber} so that users can make booking requests",
                    UserId = estate.UserId,
                    Type = NotifyTypes.RemindPublisherToSetBookingPeriod,
                    CategoryType = CategoryType.DailyRent,
                    RouteId = estate.Id
                });
            }



            var entertainmentRentEstates = await _baseRepository
                .GetQuery<EntertainmentEstate>(x => !x.BookingFrom.HasValue && !x.BookingTo.HasValue)
                .ToListAsync();
            foreach (var estate in entertainmentRentEstates)
            {
                await _notificationService.SendNotifyAsync(new NotificationModel
                {
                    TextAr = $"يرجي تحديدة فترة حجز للعقار رقم {estate.EstateNumber}، حتي يتمكن المستخدمين من ارسال طلبات حجز",
                    TextEn = $"Please set a booking period for property number {estate.EstateNumber} so that users can make booking requests",
                    UserId = estate.UserId,
                    Type = NotifyTypes.RemindPublisherToSetBookingPeriod,
                    CategoryType = CategoryType.Entertainment,
                    RouteId = estate.Id
                });
            }


            return true;
        }
    }
}
