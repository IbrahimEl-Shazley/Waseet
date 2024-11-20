using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.PropertiesManagement;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.PropertiesManagement.MaintainanceMangment;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.PropertiesManagement;

namespace Wasit.Services.Implementations.Generic.PropertiesManagement
{
    public class MaintainanceMangementService : BaseService, IMaintainanceMangementService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;


        public MaintainanceMangementService(IServiceProvider serviceProvider, IUnitOfWork uow) : base(serviceProvider)
        {
            _uow = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _baseRepository = uow.Repository<IBaseRepository>();
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
        }


        public async Task<bool> AddMaintainanceOrder(string userId, AddMaintainanceOrderPayload payload)
        {
            if (await _baseRepository.AnyAsync<MaintenanceOrder>(x => x.EstateNumber == payload.UniqueNumber && x.OrderStatus != RentalManagementOrderStatus.Finished))
                throw new BussinessRuleException("EstateAlreadyExists");

            var serviceCost = await _baseRepository
                .GetQuery<Core.Entities.SettingTables.Service>(x => x.Type == ServiceType.MaintenanceOrder, false)
                .Select(x => x.ServiceCost)
                .FirstOrDefaultAsync();

            serviceCost *= payload.Area;

            var maintainanceOrder = _mapper.Map<MaintenanceOrder>(payload, o => o.Items[nameof(MaintenanceOrder.ServiceCost)] = serviceCost);
            await _baseRepository.AddAsync(maintainanceOrder);
            await _uow.SaveChangeAsync();

            await _notificationService.SendNotifyToDashboardAsync(new NotificationModel
            {
                TextAr = "لديك طلب اضافة عقار جديد في قسم ادارة المرافق والصيانة، يرجي مراجعته",
                TextEn = "You have a new request to add a new estate in the maintenance management, please review it",
                Type = NotifyTypes.NewMaintainanceManagementRequest,
                RouteId = maintainanceOrder.Id
            });

            return true;
        }


        public async Task<PageDTO<MaintainanceOrderItemDto>> ListMyManagedMaintainanceEstates(string userId, int pageNumber, RentalManagementOrderStatus status)
        {
            var skip = (pageNumber - 1) * MyConstants.PageSize;
            var query = _baseRepository.GetQuery<MaintenanceOrder>(x => x.UserId == userId && x.OrderStatus == status, false)
                .OrderByDescending(x => x.Id);

            var result = await query.Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            return new PageDTO<MaintainanceOrderItemDto>
            {
                CurrentPage = pageNumber,
                TotalCount = await query.CountAsync(),
                Count = result.Count,
                Data = _mapper.Map<List<MaintainanceOrderItemDto>>(result)
            };
        }


        public async Task<bool> RateMaintainanceOrder(string userId, long orderId, double rating)
        {
            var maintainanceOrder = await _baseRepository.FirstOrDefaultAsync<MaintenanceOrder>(x => x.Id == orderId && x.UserId == userId) ??
                throw new BussinessRuleException("RequestNotFound");

            if (maintainanceOrder.UserRate > 0)
                throw new BussinessRuleException("AlreadyRated");

            if (maintainanceOrder.OrderStatus != RentalManagementOrderStatus.Finished)
                throw new BussinessRuleException("MaintainanceOrderNotFinishedToBeRated");

            maintainanceOrder.UserRate = rating;
            _baseRepository.Update(maintainanceOrder);
            await _uow.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {maintainanceOrder.User.User_Name} بتقييمك",
                TextEn = $"User {maintainanceOrder.User.User_Name} has rated you",
                Type = NotifyTypes.NewMaintainanceRatingForDelegate,
                UserId = maintainanceOrder.ProviderId,
                RouteId = maintainanceOrder.Id
            });

            return true;
        }

    }
}
