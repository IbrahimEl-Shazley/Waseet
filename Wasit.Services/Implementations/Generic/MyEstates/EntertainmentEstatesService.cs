using AAITHelper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Enums;
using Wasit.Core.ExtensionsMethods;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.General;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.Interfaces.Entertainment;
using Wasit.Repositories.UnitOfWork;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentEstate;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentRentRequest;
using Wasit.Services.DTOs.Schema.EstateCategories.EstateType;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.MyEstates;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.Services.Implementations.Generic.MyEstates
{
    public class EntertainmentEstatesService : BaseService<EntertainmentEstate, EntertainmentEstateDto, CreateEntertainmentEstateDto, UpdateEntertainmentEstateDto>, IMySharedEstatesService, IEntertainmentEstatesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntertainmentEstateRepository _entertainmentEstateRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IGeneralService _generalService;

        public EntertainmentEstatesService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
            _entertainmentEstateRepository = uow.Repository<IEntertainmentEstateRepository>();
            _userRepository = uow.Repository<IUserRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _currentUserService = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
            _generalService = (IGeneralService)serviceProvider.GetService(typeof(IGeneralService));
        }


        public async Task<bool> ConfirmEstateDeedNumber(string estateDeedNumber)
        {
            var estateFound = await _entertainmentEstateRepository
            .AnyAsync<EntertainmentEstate>(s => s.EstateNumber == estateDeedNumber);

            if (estateFound)
                throw new BussinessRuleException("EstateAlreadyExists");

            return true;
        }


        public async Task<dynamic> GetEstateTypes()
        {
            var result = await _entertainmentEstateRepository
               .GetQuery<CategoryEstateType>(x => x.Category.Type == CategoryType.Entertainment && x.EstateType.IsActive, false, [x => x.EstateType, x => x.Category])
               .ToListAsync();

            return _mapper.Map<List<EstateTypeDto>>(result);
        }


        public async Task<dynamic> ListMyEstates(string userId, int pageNumber)
        {
            return await GetListWithPagingAsync(pageNumber, MyConstants.PageSize, estate => estate.UserId == userId, false,
                [estate => estate.Region.City, estate => estate.User, estate => estate.EstateType]);
        }


        public async Task<bool> AddNewEntertainmentEstate(string userId, CreateEntertainmentEstateDto model)
        {
            if (await _entertainmentEstateRepository.EntertainmentEstateExists(model.UniqueNumber, userId))
                throw new BussinessRuleException("EstateAlreadyExists");

            try
            {
                var entertainmentEstate = _mapper.Map<EntertainmentEstate>(model);
                var entertainmentEstateImages = _mapper.Map<List<EntertainmentEstateImage>>(model.Images);
                var entertainmentEstateSpecs = _mapper.Map<List<EntertainmentEstateSpecificationValue>>(model.AdditionalSpecs);

                await _unitOfWork.BeginTransactionAsync();

                entertainmentEstate.Images = entertainmentEstateImages;
                entertainmentEstate.SpecificationValues = entertainmentEstateSpecs;

                await _entertainmentEstateRepository.AddAsync(entertainmentEstate);
                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitAsync();

                await _notificationService.SendNotifyToDashboardAsync(new NotificationModel
                {
                    TextAr = "لديك طلب اضافة عقار جديد، يرجي مراجعته",
                    TextEn = "You have a new request to add a new property, please review it",
                    Type = NotifyTypes.AddNewEstate,
                    CategoryType = CategoryType.Entertainment,
                    RouteId = entertainmentEstate.Id
                });

                return true;
            }
            catch
            {
                await _unitOfWork.RollBackAsync();
                throw new BussinessRuleException("InternalServerErrorOccured");
            }
        }


        public async Task<EntertainmentEstateInfoDto> MyEntertainmentEstateInfo(string userId, long estateId)
        {
            try
            {
                var entertainmentEstate = await _entertainmentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId == userId
                , true, [x => x.Images, x => x.SpecificationValues, x => x.Requests]) ??
                    throw new BussinessRuleException("EstateNotFound");

                var culture = DateTimeHelper.GetCulture(_currentUserService.Language);

                return _mapper.Map<EntertainmentEstateInfoDto>(entertainmentEstate, o => o.Items["culture"] = culture);
            }
            catch
            {
                throw new InternalServerException("InternalServerErrorOccured");
            }
        }


        public async Task<bool> EditEntertainmentEstate(string userId, UpdateEntertainmentEstateDto model)
        {
            var entertainmentEstate = await _entertainmentEstateRepository
                .FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId, true, [x => x.Images, x => x.SpecificationValues]) ??
                throw new BussinessRuleException("EstateNotFound");

            try
            {
                var updatedEntertainmentEstate = _mapper.Map(model, entertainmentEstate);
                var entertainmentEstateImages = _mapper.Map<List<EntertainmentEstateImage>>(model.Images);
                var entertainmentEstateSpecs = _mapper.Map<List<EntertainmentEstateSpecificationValue>>(model.AdditionalSpecs);

                await _unitOfWork.BeginTransactionAsync();

                if (model.Images.Any())
                {
                    entertainmentEstateImages.ForEach(x => x.EntertainmentEstateId = entertainmentEstate.Id);
                    await _entertainmentEstateRepository.AddRangeAsync(entertainmentEstateImages);
                }

                if (model.ImagesToBeRemoved.Any())
                    _entertainmentEstateRepository.RemoveRange(entertainmentEstate.Images.Where(x => model.ImagesToBeRemoved.Contains(x.Id)).ToList());

                _entertainmentEstateRepository.RemoveRange(entertainmentEstate.SpecificationValues.ToList());
                updatedEntertainmentEstate.SpecificationValues = entertainmentEstateSpecs;

                _entertainmentEstateRepository.Update(updatedEntertainmentEstate);
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
            var specificationValues = await _entertainmentEstateRepository.GetListAsync<EntertainmentEstateSpecificationValue>(x => x.EntertainmentEstateId == estateId, false);
            return _mapper.Map<List<SpecificationValueDto>>(specificationValues);
        }

        public async Task<PageDTO<EntertainmentRentRequestDto>> ListEntertainmentRentRequests(ListEntertainmentRentRequestPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;
            var result = await _entertainmentEstateRepository
                .GetListWithPagingAsync<EntertainmentRequest>(skip, MyConstants.PageSize, x => x.EntertainmentEstateId == model.EstateId && x.Status == model.Status, false, x => x.User);
            return _mapper.Map<PageDTO<EntertainmentRentRequestDto>>(result);
        }


        public async Task<EntertainmentRentRequestInfoDto> EntertainmentRentRequestInfo(long requestId)
        {
            var entertainmentRequest = await _entertainmentEstateRepository
                .FirstOrDefaultAsync<EntertainmentRequest>(x => x.Id == requestId, false) ??
                throw new BussinessRuleException("RequestNotFound");

            var culture = DateTimeHelper.GetCulture(_currentUserService.Language);

            return _mapper.Map<EntertainmentRentRequestInfoDto>(entertainmentRequest, o => o.Items["culture"] = culture);
        }


        public async Task<bool> ChangeEstateVisibility(long estateId)
        {
            var entertainmentEstate = await _entertainmentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId) ??
                throw new BussinessRuleException("EstateNotFound");

            entertainmentEstate.IsShow = !entertainmentEstate.IsShow;

            _entertainmentEstateRepository.Update(entertainmentEstate);
            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> RemoveEstate(long estateId)
        {
            var entertainmentEstate = await _entertainmentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId) ??
                throw new BussinessRuleException("EstateNotFound");

            if (entertainmentEstate.Requests.Any(x => x.Status != DailyRentStatus.Finished && x.Status != DailyRentStatus.Canceled))
                throw new BussinessRuleException("EstateCanNotBeRemoved");

            entertainmentEstate.IsDeleted = true;

            _entertainmentEstateRepository.Update(entertainmentEstate);
            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> ReportComment(string userId, long requestId, string reason)
        {
            var request = await _entertainmentEstateRepository.FirstOrDefaultAsync<EntertainmentRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

            await _entertainmentEstateRepository.AddAsync(new ReportEntertainmentComment
            {
                EntertainmentRequestId = requestId,
                ReasonForReport = reason,
                ReporterId = userId
            });

            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyToDashboardAsync(new NotificationModel
            {
                TextAr = $"قام المستخدم {request.EntertainmentEstate.User.User_Name} بالابلاغ عن تعليق مسيء",
                TextEn = $"User {request.EntertainmentEstate.User.User_Name} has reported a violated comment",
                Type = NotifyTypes.NewReport,
                CategoryType = CategoryType.Entertainment,
                RouteId = requestId
            });

            return true;
        }


        public async Task<PageDTO<BaseRatingItemDto>> ListEstateRatings(string userId, long estateId, int pageNumber)
        {
            var ratings = _entertainmentEstateRepository.GetQuery<EntertainmentRequest>(x => x.EntertainmentEstateId == estateId && x.UserRatingDateTime != null, false, x => x.User)
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
            var entertainmentEstate = await _entertainmentEstateRepository.FirstOrDefaultAsync<EntertainmentEstate>(x => x.Id == estateId && x.UserId == userId, true, x => x.Requests) ??
                throw new BussinessRuleException("EstateNotFound");

            if (entertainmentEstate.Requests.IsDatesInRange(startDate, endDate))
                throw new BussinessRuleException("EstateIsAlreadyReserved");

            entertainmentEstate.BookingFrom = startDate;
            entertainmentEstate.BookingTo = endDate;

            _entertainmentEstateRepository.Update(entertainmentEstate);
            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<string> DownloadFinancialAccounts(string userId, long estateId, DateTime startDate, DateTime endDate)
        {
            return string.Empty;
        }

        public async Task<bool> RateRentee(string userId, long requestId, double rating)
        {
            var request = await _entertainmentEstateRepository.FirstOrDefaultAsync<EntertainmentRequest>(x => x.Id == requestId && x.EntertainmentEstate.UserId == userId) ??
               throw new BussinessRuleException("RequestNotFound");

            if (request.Status != DailyRentStatus.Finished)
                throw new BussinessRuleException("RequestNotFinished");

            request.OwnerRating = rating;
            request.OwnerRatingDateTime = HelperDate.GetCurrentDate();
            _entertainmentEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            request.User.Rating = await _generalService.CalcAverageUserRating(request.UserId);
            _userRepository.UpdateUser(request.User);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"قام {request.EntertainmentEstate.User.User_Name} بتقييمك",
                TextEn = $"User {request.EntertainmentEstate.User.User_Name} has rated you",
                UserId = request.UserId,
                Type = NotifyTypes.NewRatingForConsumer,
                CategoryType = CategoryType.Entertainment,
                RouteId = requestId
            });

            return true;
        }
    }
}
