using AAITHelper.ModelHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using Wasit.Core.Entities.NOTIFIC;
using Wasit.Core.Entities.SettingTables;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.Helpers.Notifications;
using Wasit.Integration.DTOs;
using Wasit.Integration.Services.Abstraction;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.General;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Interfaces.General;
using Wasit.Services.ServiceHelpers;
using Wasit.Services.ServiceHelpers.EmailTemplates;
using NotificationDTO = Wasit.Services.DTOs.General.Notification;
using NotificationEntity = Wasit.Core.Entities.UserTables.Notification;

namespace Wasit.Services.Implementation.General
{
    public class NotificationService : BaseService, INotificationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICurrentUserService _statlessSessionService;
        private readonly ISMSService _sMSService;
        private readonly IMailService _mailService;

        private readonly IBaseRepository _baseRepository;
        private readonly IUserRepository _userRepository;

        public NotificationService(IUnitOfWork uow, IConfiguration configuration, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _uow = uow;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _statlessSessionService = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
            _sMSService = (ISMSService)serviceProvider.GetService(typeof(ISMSService));
            _mailService = (IMailService)serviceProvider.GetService(typeof(IMailService));
            _baseRepository = _uow.Repository<IBaseRepository>();
            _userRepository = _uow.Repository<IUserRepository>();
        }


        public async Task<bool> Send(NotificationDTO dto, NotificationTypeEnum notificationType)
        {
            Type type = typeof(ServicesAssembly).Assembly.GetType($"Wasit.Services.DTOs.General.{notificationType}");
            var inst = Activator.CreateInstance(type);
            if (inst.GetType().GetProperty(nameof(dto.To)) != null) inst.GetType().GetProperty(nameof(dto.To)).SetValue(inst, dto.To);
            if (inst.GetType().GetProperty(nameof(dto.Input)) != null) inst.GetType().GetProperty(nameof(dto.Input)).SetValue(inst, dto.Input);
            if (inst.GetType().GetProperty(nameof(dto.NotificationCategory)) != null) inst.GetType().GetProperty(nameof(dto.NotificationCategory)).SetValue(inst, dto.NotificationCategory);
            return await Send((NotificationDTO)inst);
        }

        public async Task<bool> Send(NotificationDTO dto)
        {
            switch (dto)
            {
                case SMS: return await SendSMS((SMS)dto);
                case Email: return await SendEmail((Email)dto);
                default: throw new NotImplementedException();
            }
        }


        public async Task<bool> SendNotifyAsync(NotificationModel model)
        {
            try
            {
                var user = await _userRepository
                    .UserFirstOrDefaultAsync<ApplicationDbUser>(x => x.Id == model.UserId, false, x => x.DeviceIds);

                if (user is null) return false;

                await AddNotification(model);

                if (user.AllowNotify || model.Type == NotifyTypes.RemoveUser || model.Type == NotifyTypes.DeactivateUser)
                {
                    var FcmSettings = await _baseRepository
                        .GetQuery<Setting>(x => true, false)
                        .Select(x => new
                        {
                            x.ServerKey_FCM,
                            x.SenderId_FCM
                        }).FirstOrDefaultAsync();

                    var userDevices = user.DeviceIds
                        .Select(x => new DeviceIdModel
                        {
                            DeviceId = x.DeviceId_,
                            DeviceType = x.DeviceType,
                            FkUser = x.UserId
                        }).ToList();

                    await FCMHelper.SendPushNotification(
                        FcmSettings.ServerKey_FCM,
                        FcmSettings.SenderId_FCM,
                        userDevices,
                        null,
                        user.Lang == Language.Ar ? model.TextAr : model.TextEn,
                        model.Type,
                        model.RouteId,
                        model.CategoryType);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> SendNotifyToDashboardAsync(NotificationModel model)
        {
            try
            {
                await AddAdminNotification(model);

                //var FcmSettings = await _baseRepository
                //    .GetQuery<Setting>(x => true, false)
                //    .Select(x => new
                //    {
                //        x.ApplicationId,
                //        x.SenderId
                //    }).FirstOrDefaultAsync();

                //var userDevices = user.DeviceIds
                //    .Select(x => new DeviceIdModel
                //    {
                //        DeviceId = x.DeviceId_,
                //        DeviceType = x.DeviceType,
                //        FkUser = x.UserId
                //    }).ToList();

                //await FCMHelper.SendPushNotification(
                //    FcmSettings.ApplicationId,
                //    FcmSettings.SenderId,
                //    userDevices,
                //    null,
                //    model.TextAr,
                //    model.Type,
                //    model.RouteId,
                //    model.CategoryType);

                return true;
            }
            catch
            {
                return false;
            }
        }



        #region private
        private async Task<bool> AddNotification(NotificationModel model)
        {
            await _baseRepository.AddAsync(new NotificationEntity
            {
                UserId = model.UserId,
                TextAr = model.TextAr,
                TextEn = model.TextEn,
                Type = model.Type,
                CategoryType = model.CategoryType,
                RouteId = model.RouteId
            });

            return await _uow.SaveChangeAsync();
        }

        private async Task<bool> AddAdminNotification(NotificationModel model)
        {
            await _baseRepository.AddAsync(new AdminNotification
            {
                TextAr = model.TextAr,
                TextEn = model.TextEn,
                Type = model.Type,
                CategoryType = model.CategoryType,
                RouteId = model.RouteId
            });

            return await _uow.SaveChangeAsync();
        }


        private async Task<bool> SendSMS(SMS dto)
        {
            return await _sMSService.Send(new SMSDTO
            {
                Message = dto.Input,
                Number = dto.To,
            });
        }


        private async Task<bool> SendEmail(Email dto)
        {
            INotificationTemplate templateLocator = _serviceProvider.GetService(NotificationTemplateLocator.Templates[dto.NotificationCategory]) as INotificationTemplate;
            NotificationTemplate template = await _baseRepository.FirstOrDefaultAsync<NotificationTemplate>(x => x.NotificationTypeId == (int)NotificationTypeEnum.Email && x.NotificationCategoryId == (int)dto.NotificationCategory);

            var mailMessage = new MailMessage(new MailAddress(NotificationHelper.FromEmail()), new MailAddress(dto.To))
            {
                Subject = NotificationHelper.LoadNotificationSubject(dto.NotificationCategory, _statlessSessionService.Language),
                Body = NotificationHelper.LoadNotificationBody(templateLocator, template, dto.Input, _statlessSessionService.Language),
                IsBodyHtml = true,
            };
            return await _mailService.Send(mailMessage);
        }

        #endregion
    }
}

