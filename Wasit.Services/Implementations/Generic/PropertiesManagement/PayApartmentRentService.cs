using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.PropertiesManagement;
using Wasit.Core.Enums;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.PropertiesManagement.PayApartmentRent;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.PropertiesManagement;

namespace Wasit.Services.Implementations.Generic.PropertiesManagement
{
    public class PayApartmentRentService : BaseService, IPayApartmentRentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public PayApartmentRentService(IServiceProvider serviceProvider, IUnitOfWork uow) : base(serviceProvider)
        {
            _uow = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _baseRepository = uow.Repository<IBaseRepository>();
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
        }

        public async Task<RentPayerApartmentInfoDto> GetApartmentInfo(int apartmentNo)
        {
            var apartment = await _baseRepository
                .FirstOrDefaultAsync<EstateApartment>(x => x.Number == apartmentNo && x.RentalOrder.IsApproved, false, x => x.RentalOrder) ??
                throw new BussinessRuleException("ApartmentNotFound");

            return _mapper.Map<RentPayerApartmentInfoDto>(apartment);
        }
        
        
        public async Task<bool> PayRent(string userId, long apartmentId, TypePay paymentMethod)
        {
            var apartment = await _baseRepository.FirstOrDefaultAsync<EstateApartment>(x => x.Id == apartmentId) ??
                throw new BussinessRuleException("ApartmentNotFound");

            var serviceCost = await _baseRepository
                .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.RentManagementAppPercentage, false)
                .Select(x => x.ServiceCost/100)
                .FirstOrDefaultAsync();

            serviceCost *= apartment.RentalPrice;

            var apartmentRentPay = new ApartmentRentPay
            {
                EstateApartmentId = apartment.Id,
                IsPay = true,
                RentalPrice = apartment.RentalPrice,
                TypePay = paymentMethod,
                UserId = userId,
                AppTax = serviceCost,
                PublisherNetTotal = apartment.RentalPrice - serviceCost
            };
            await _baseRepository.AddAsync(apartmentRentPay);

            // if payment is successful
            apartment.IsRentPaid = true;
            await _uow.SaveChangeAsync();

            var userName = await _baseRepository
                .GetQuery<ApartmentRentPay>(x => x.Id == apartmentRentPay.Id, false)
                .Select(x => x.User.User_Name)
                .FirstOrDefaultAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {userName} بدفع قيمة الايجار للشقة رقم {apartment.Number}",
                TextEn = $"User {userName} has paid the rental price for the apartment number {apartment.Number}",
                UserId = apartment.RentalOrder.UserId,
                Type = NotifyTypes.ApartmentRentPaid,
                RouteId = apartment.RentalOrder.Id
            });

            return true;
        }
    }
}
