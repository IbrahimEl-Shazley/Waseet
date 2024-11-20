using AAITHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.RentEstateSection;
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
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentRequest;
using Wasit.Services.DTOs.Schema.EstateCategories.EstateType;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.MyEstates;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.Services.Implementations.Generic.MyEstates
{
    public class DailyRentEstateService : BaseService<DailyRentEstate, DailyRentEstateDto, CreateDailyRentEstateDto, UpdateDailyRentEstateDto>, IMySharedEstatesService, IDailyRentEstatesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDailyRentEstateRepository _dailyRentEstateRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly IGeneralService _generalService;

        public DailyRentEstateService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
            _dailyRentEstateRepository = uow.Repository<IDailyRentEstateRepository>();
            _userRepository = uow.Repository<IUserRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _currentUserService = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
            _generalService = (IGeneralService)serviceProvider.GetService(typeof(IGeneralService));
        }

        public async Task<bool> ConfirmEstateDeedNumber(string estateDeedNumber)
        {
            try
            {
                var result = await _dailyRentEstateRepository
                .AnyAsync<DailyRentEstate>(s => s.EstateNumber == estateDeedNumber);

                if (result is false)
                    throw new BussinessRuleException("EstateAlreadyExists");

                //return result;
                return true;
            }
            catch
            {
                throw new BussinessRuleException("InternalServerErrorOccured");
            }
        }


        public async Task<dynamic> GetEstateTypes()
        {
            var result = await _dailyRentEstateRepository
               .GetQuery<CategoryEstateType>(x => x.Category.Type == CategoryType.DailyRent && x.EstateType.IsActive, false, [x => x.EstateType, x => x.Category])
               .ToListAsync();

            return _mapper.Map<List<EstateTypeDto>>(result);
        }


        public async Task<dynamic> ListMyEstates(string userId, int pageNumber)
        {
            return await GetListWithPagingAsync(pageNumber, MyConstants.PageSize, estate => estate.UserId == userId, false,
                [estate => estate.Region.City, estate => estate.User, estate => estate.EstateType]);
        }


        public async Task<bool> AddNewDailyRentEstate(string userId, CreateDailyRentEstateDto model)
        {
            if (await _dailyRentEstateRepository.DailyRentEstateExists(model.UniqueNumber, userId))
                throw new BussinessRuleException("EstateAlreadyExists");

            try
            {
                var dailyRentEstate = _mapper.Map<DailyRentEstate>(model);
                var dailyRentEstateImages = _mapper.Map<List<DailyRentEstateImage>>(model.Images);
                var dailyRentEstateSpecs = _mapper.Map<List<DailyRentEstateSpecificationValue>>(model.AdditionalSpecs);

                await _unitOfWork.BeginTransactionAsync();

                dailyRentEstate.Images = dailyRentEstateImages;
                dailyRentEstate.SpecificationValues = dailyRentEstateSpecs;

                await _dailyRentEstateRepository.AddAsync(dailyRentEstate);
                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitAsync();

                await _notificationService.SendNotifyToDashboardAsync(new NotificationModel
                {
                    TextAr = "لديك طلب اضافة عقار جديد، يرجي مراجعته",
                    TextEn = "You have a new request to add a new property, please review it",
                    Type = NotifyTypes.AddNewEstate,
                    CategoryType = CategoryType.DailyRent,
                    RouteId = dailyRentEstate.Id
                });

                return true;
            }
            catch
            {
                await _unitOfWork.RollBackAsync();
                throw new BussinessRuleException("InternalServerErrorOccured");
            }
        }


        public async Task<DailyRentEstateInfoDto> MyDailyRentEstateInfo(string userId, long estateId)
        {
            try
            {
                var dailyRentEstate = await _dailyRentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId == userId
                , true, [x => x.Images, x => x.SpecificationValues, x => x.Requests]) ??
                    throw new BussinessRuleException("EstateNotFound");

                var culture = DateTimeHelper.GetCulture(_currentUserService.Language);

                return _mapper.Map<DailyRentEstateInfoDto>(dailyRentEstate, o => o.Items["culture"] = culture);
            }
            catch
            {
                throw new InternalServerException("InternalServerErrorOccured");
            }
        }


        public async Task<bool> EditDailyRentEstate(string userId, UpdateDailyRentEstateDto model)
        {
            var dailyRentEstate = await _dailyRentEstateRepository
                .FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId, true, [x => x.Images, x => x.SpecificationValues]) ??
                throw new BussinessRuleException("EstateNotFound");

            try
            {
                var updatedDailyRentEstate = _mapper.Map(model, dailyRentEstate);
                var dailyRentEstateImages = _mapper.Map<List<DailyRentEstateImage>>(model.Images);
                var dailyRentEstateSpecs = _mapper.Map<List<DailyRentEstateSpecificationValue>>(model.AdditionalSpecs);

                await _unitOfWork.BeginTransactionAsync();

                if (model.Images.Count != 0)
                {
                    dailyRentEstateImages.ForEach(x => x.DailyRentEstateId = dailyRentEstate.Id);
                    await _dailyRentEstateRepository.AddRangeAsync(dailyRentEstateImages);
                }

                if (model.ImagesToBeRemoved.Count != 0)
                    _dailyRentEstateRepository.RemoveRange(dailyRentEstate.Images.Where(x => model.ImagesToBeRemoved.Contains(x.Id)).ToList());

                _dailyRentEstateRepository.RemoveRange(dailyRentEstate.SpecificationValues.ToList());
                updatedDailyRentEstate.SpecificationValues = dailyRentEstateSpecs;

                _dailyRentEstateRepository.Update(updatedDailyRentEstate);
                await _unitOfWork.SaveChangeAsync();

                await _unitOfWork.CommitAsync();

                return true;
            }
            catch
            {
                await _unitOfWork.RollBackAsync();
                throw new BussinessRuleException("InternalServerErrorOccured");
            }
        }

        public async Task<IEnumerable<SpecificationValueDto>> GetSpecificationValues(string userId, long estateId)
        {
            var specificationValues = await _dailyRentEstateRepository.GetListAsync<DailyRentEstateSpecificationValue>(x => x.DailyRentEstateId == estateId, false);
            return _mapper.Map<List<SpecificationValueDto>>(specificationValues);
        }

        public async Task<PageDTO<DailyRentRequestDto>> ListDailyRentRequests(ListDailyRentRequestsPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;
            var result = await _dailyRentEstateRepository
                .GetListWithPagingAsync<DailyRentRequest>(skip, MyConstants.PageSize, x => x.DailyRentEstateId == model.EstateId && x.Status == model.Status, false, x => x.User);
            return _mapper.Map<PageDTO<DailyRentRequestDto>>(result);
        }


        public async Task<DailyRentRequestInfoDto> DailyRentRequestInfo(long requestId)
        {
            var dailyRentRequest = await _dailyRentEstateRepository.FirstOrDefaultAsync<DailyRentRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

            var culture = DateTimeHelper.GetCulture(_currentUserService.Language);

            return _mapper.Map<DailyRentRequestInfoDto>(dailyRentRequest, o => o.Items["culture"] = culture);
        }


        public async Task<bool> ChangeEstateVisibility(long estateId)
        {
            var dailyRentEstate = await _dailyRentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId) ??
                throw new BussinessRuleException("EstateNotFound");

            dailyRentEstate.IsShow = !dailyRentEstate.IsShow;

            _dailyRentEstateRepository.Update(dailyRentEstate);
            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> RemoveEstate(long estateId)
        {
            var dailyRentEstate = await _dailyRentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId) ??
                throw new BussinessRuleException("EstateNotFound");

            if (dailyRentEstate.Requests.Any(x => x.Status != DailyRentStatus.Finished && x.Status != DailyRentStatus.Canceled))
                throw new BussinessRuleException("EstateCanNotBeRemoved");

            dailyRentEstate.IsDeleted = true;

            _dailyRentEstateRepository.Update(dailyRentEstate);
            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> ReportComment(string userId, long requestId, string reason)
        {
            var dailyRentRequest = await _dailyRentEstateRepository.FirstOrDefaultAsync<DailyRentRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

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
                TextAr = $"قام المستخدم {dailyRentRequest.User.User_Name} بالابلاغ عن تعليق مسيء",
                TextEn = $"User {dailyRentRequest.User.User_Name} has reported a violated comment",
                Type = NotifyTypes.NewReport,
                CategoryType = CategoryType.DailyRent,
                RouteId = report.Id
            });

            return true;
        }

        public async Task<PageDTO<BaseRatingItemDto>> ListEstateRatings(string userId, long estateId, int pageNumber)
        {
            var ratings = _dailyRentEstateRepository.GetQuery<DailyRentRequest>(x => x.DailyRentEstateId == estateId && x.UserRatingDateTime != null, false, x => x.User)
                .OrderByDescending(x => x.Id);
            var skip = (pageNumber - 1) * MyConstants.PageSize;
            var data = await ratings.Skip(skip).Take(MyConstants.PageSize).OrderByDescending(x => x.Id).ToListAsync();
            var culture = DateTimeHelper.GetCulture(_currentUserService.Language);

            return new PageDTO<BaseRatingItemDto>
            {
                CurrentPage = pageNumber,
                TotalCount = await ratings.CountAsync(),
                Count = data.Count,
                Data = _mapper.Map<List<BaseRatingItemDto>>(data, o => o.Items["culture"] = culture)
            };
        }


        public async Task<bool> SelectReservationPeriod(string userId, long estateId, DateTime startDate, DateTime endDate)
        {
            var dailyRentEstate = await _dailyRentEstateRepository.FirstOrDefaultAsync<DailyRentEstate>(x => x.Id == estateId && x.UserId == userId, true, x => x.Requests) ??
                            throw new BussinessRuleException("EstateNotFound");

            if (dailyRentEstate.Requests.IsDatesInRange(startDate, endDate))
                throw new BussinessRuleException("EstateIsAlreadyReserved");

            dailyRentEstate.BookingFrom = startDate;
            dailyRentEstate.BookingTo = endDate;

            _dailyRentEstateRepository.Update(dailyRentEstate);
            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<string> DownloadFinancialAccounts(string userId, long estateId, DateTime startDate, DateTime endDate)
        {
            return string.Empty;
        }

        public async Task<bool> RateRentee(string userId, long requestId, double rating)
        {
            var request = await _dailyRentEstateRepository.FirstOrDefaultAsync<DailyRentRequest>(x => x.Id == requestId && x.DailyRentEstate.UserId == userId) ??
               throw new BussinessRuleException("RequestNotFound");

            if (request.Status != DailyRentStatus.Finished)
                throw new BussinessRuleException("RequestNotFinished");

            request.OwnerRating = rating;
            request.OwnerRatingDateTime = HelperDate.GetCurrentDate();
            _dailyRentEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            request.User.Rating = await _generalService.CalcAverageUserRating(request.UserId);
            _userRepository.UpdateUser(request.User);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {request.DailyRentEstate.User.User_Name} بتقييمك",
                TextEn = $"User {request.DailyRentEstate.User.User_Name} has rated you",
                UserId = request.UserId,
                Type = NotifyTypes.NewRatingForConsumer,
                CategoryType = CategoryType.DailyRent,
                RouteId = requestId
            });

            return true;
        }
    }
}
