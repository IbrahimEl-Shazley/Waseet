using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.PropertiesManagement;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.IO;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.PropertiesManagement.RentManagement;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.PropertiesManagement;

namespace Wasit.Services.Implementations.Generic.PropertiesManagement
{
    public class RentManagementService : BaseService, IRentManagementService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public RentManagementService(IServiceProvider serviceProvider, IUnitOfWork uow) : base(serviceProvider)
        {
            _uow = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _baseRepository = uow.Repository<IBaseRepository>();
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
        }

        public async Task<bool> AddRentalManagementOrder(AddRentalMangementOrderPayload payload, string userId)
        {
            if (await _baseRepository.AnyAsync<RentalManagementOrder>(x => x.UserId == userId && x.EstateNumber == payload.UniqueNumber && !x.IsCanceled && !x.IsTerminated))
                throw new BussinessRuleException("RequestAlreadySentWithSameDeedNumber");

            if (await _baseRepository.AnyAsync<RentalManagementOrder>(x => x.UserId != userId && x.EstateNumber == payload.UniqueNumber && !x.IsCanceled && !x.IsTerminated))
                throw new BussinessRuleException("EstateAlreadyExists");

            var rentalManagementOrder = _mapper.Map<RentalManagementOrder>(payload);

            foreach (var apartment in rentalManagementOrder.EstateApartments)
            {
                while (await _baseRepository.AnyAsync<EstateApartment>(x => x.Number == apartment.Number))
                    apartment.Number = new Random().Next(100000, 999999);
            }

            await _baseRepository.AddAsync(rentalManagementOrder);
            await _uow.SaveChangeAsync();

            await _notificationService.SendNotifyToDashboardAsync(new NotificationModel
            {
                TextAr = "لديك طلب اضافة عقار جديد في قسم ادارة الايجارات، يرجي مراجعته",
                TextEn = "You have a new request to add a new estate in the rental management, please review it",
                Type = NotifyTypes.NewRentalManagementRequest,
                RouteId = rentalManagementOrder.Id
            });

            return true;
        }


        public async Task<ManagedRentEstateInfoDto> EditRentalManagementOrder(long orderId, EditRentalMangementOrderPayload payload)
        {
            var rentalManagementOrder = await _baseRepository.FirstOrDefaultAsync<RentalManagementOrder>(x => x.Id == orderId) ??
                throw new BussinessRuleException(message: "RequestNotFound");
            try
            {
                _baseRepository.BeginTrnsaction();

                if (payload.ApartmentsToBeRemoved.Count != 0)
                {
                    var apartments = rentalManagementOrder.EstateApartments.Where(x => payload.ApartmentsToBeRemoved.Contains(x.Id)).ToList();
                    _baseRepository.RemoveRange(apartments);
                    await _uow.SaveChangeAsync();
                }

                if (payload.Apartments.Count != 0)
                {
                    var apartments = _mapper.Map<List<EstateApartment>>(payload.Apartments);

                    foreach (var apartment in apartments)
                    {
                        while (await _baseRepository.AnyAsync<EstateApartment>(x => x.Number == apartment.Number))
                            apartment.Number = new Random().Next(100000, 999999);
                        rentalManagementOrder.EstateApartments.Add(apartment);
                    }
                    await _uow.SaveChangeAsync();
                }

                if (payload.Image != null)
                    rentalManagementOrder.EstateImage = IOHelper.Upload(payload.Image, (int)FileName.Estates);

                rentalManagementOrder = _mapper.Map(payload, rentalManagementOrder);
                _baseRepository.Update(rentalManagementOrder);

                await _uow.SaveChangeAsync();
                _baseRepository.Commit();
                return await ManagedRentEstateInfo(rentalManagementOrder.UserId, orderId);
            }
            catch
            {
                _baseRepository.RollBack();
                throw new BussinessRuleException("UnexpectedFailurePLeaseTryAgainLater");
            }
        }


        public async Task<PageDTO<RentalManagementOrderItemDto>> ListMyManagedRentEstates(string userId, int pageNumber, RentalManagementOrderStatus status)
        {
            var skip = (pageNumber - 1) * MyConstants.PageSize;
            var query = _baseRepository.GetQuery<RentalManagementOrder>(x => x.UserId == userId && x.OrderStatus == status && !x.IsTerminated, false)
                .OrderByDescending(x => x.Id);

            var result = await query.Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            return new PageDTO<RentalManagementOrderItemDto>
            {
                CurrentPage = pageNumber,
                TotalCount = await query.CountAsync(),
                Count = result.Count,
                Data = _mapper.Map<List<RentalManagementOrderItemDto>>(result)
            };
        }


        public async Task<ManagedRentEstateInfoDto> ManagedRentEstateInfo(string userId, long orderId)
        {
            var rentalManagementOrder = await _baseRepository
                .FirstOrDefaultAsync<RentalManagementOrder>(x => x.Id == orderId && x.UserId == userId, false, [x => x.EstateApartments]) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<ManagedRentEstateInfoDto>(rentalManagementOrder);
        }

        public async Task<bool> CancelRentManagementOrder(string userId, long orderId)
        {
            var rentalManagementOrder = await _baseRepository.FirstOrDefaultAsync<RentalManagementOrder>(x => x.Id == orderId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            rentalManagementOrder.IsCanceled = true;
            rentalManagementOrder.OrderStatus = RentalManagementOrderStatus.Finished;
            _baseRepository.Update(rentalManagementOrder);
            await _uow.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {rentalManagementOrder.User.User_Name} بالغاء طلب ادارة العقار",
                TextEn = $"User {rentalManagementOrder.User.User_Name} has canceled the rental management order",
                Type = NotifyTypes.CancelRentalManagementRequest,
                RouteId = rentalManagementOrder.Id
            });

            return true;
        }

        public async Task<bool> CancelContractForRentManagement(string userId, long orderId)
        {
            var rentalManagementOrder = await _baseRepository.FirstOrDefaultAsync<RentalManagementOrder>(x => x.Id == orderId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            rentalManagementOrder.IsTerminated = true;
            rentalManagementOrder.OrderStatus = RentalManagementOrderStatus.Finished;
            _baseRepository.Update(rentalManagementOrder);
            await _uow.SaveChangeAsync();

            await _notificationService.SendNotifyToDashboardAsync(new NotificationModel
            {
                TextAr = $"قام {rentalManagementOrder.User.User_Name} بانهاء التعاقد لادارة الايجارات الخاصة به",
                TextEn = $"User {rentalManagementOrder.User.User_Name} has terminated the rental management contract",
                Type = NotifyTypes.TerminateRentalManagementRequest,
                RouteId = rentalManagementOrder.Id
            });

            return true;
        }

    }
}
