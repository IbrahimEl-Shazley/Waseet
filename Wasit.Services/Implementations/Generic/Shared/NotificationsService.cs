using AAITHelper;
using AutoMapper;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Helpers;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.Services.Implementations.Generic.Shared
{
    public class NotificationsService : BaseService, INotificationsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public NotificationsService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _baseRepository = unitOfWork.Repository<IBaseRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _currentUser = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
        }


        public async Task<PageDTO<NotificationItemDto>> Notifications(string userId, int pageNumber)
        {
            var skip = (pageNumber - 1) * MyConstants.PageSize;
            var query = _baseRepository
                           .GetQuery<Notification>(x => x.UserId == userId).OrderByDescending(x => x.Id).AsQueryable();

            var totalItems = await query.CountAsync();

            var unSeenNotifications = query.Where(x => x.IsSeen == false).ToList();
            foreach (var notification in unSeenNotifications)
                notification.IsSeen = true;
            _baseRepository.UpdateRange(unSeenNotifications);
            await _unitOfWork.SaveChangeAsync();

            query = query.Skip(skip).Take(MyConstants.PageSize);
            var now = HelperDate.GetCurrentDate();
            var culture = _currentUser.IsArabic ? new CultureInfo("ar-EG") : new CultureInfo("en-US");
            var result = query.Select(x => new NotificationItemDto
            {
                Id = x.Id,
                Text = _currentUser.IsArabic ? x.TextAr : x.TextEn,
                Type = x.Type,
                TimeSpan = x.Date.Humanize(true, now, culture),
                ItemId = x.RouteId,
                CategoryType = x.CategoryType,
                UserId = x.UserId
            });

            return new PageDTO<NotificationItemDto>
            {
                CurrentPage = pageNumber,
                TotalCount = totalItems,
                Count = result.Count(),
                Data = await result.ToListAsync()
            };
        }


        public async Task<bool> RemoveAllNotifications(string userId)
        {
            var notifications = await _baseRepository.GetListAsync<Notification>(x => x.UserId.Equals(userId));

            _baseRepository.RemoveRange(notifications);
            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> RemoveNotification(string userId, int id)
        {
            var notification = await _baseRepository.FirstOrDefaultAsync<Notification>(x => x.UserId.Equals(userId) && x.Id == id) ??
                throw new BussinessRuleException("NotificationNotFound");

            _baseRepository.Remove(notification);
            return await _unitOfWork.SaveChangeAsync();
        }
    }
}
