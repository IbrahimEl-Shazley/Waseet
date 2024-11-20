using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces.Rent;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.EstateCategories.EstateType;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;
using Wasit.Services.DTOs.Schema.Rent.RentRatingRequest;
using Wasit.Services.DTOs.Schema.Rent.RentRequest;
using Wasit.Services.DTOs.Schema.Rent.RentReservationRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest;
using Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.General;
using Wasit.Services.Interfaces.Generic.MyEstates;

namespace Wasit.Services.Implementations.Generic.MyEstates
{
    public class RentEstateService : BaseService<RentEstate, RentEstateDto, CreateRentEstateDto, UpdateRentEstateDto>, IMySharedEstatesService, IRentEstatesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentEstateRepository _rentEstateRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public RentEstateService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
            _rentEstateRepository = uow.Repository<IRentEstateRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _notificationService = (INotificationService)serviceProvider.GetService(typeof(INotificationService));
        }


        public async Task<bool> ConfirmEstateDeedNumber(string estateDeedNumber)
        {
            var estateFound = await _rentEstateRepository
            .AnyAsync<RentEstate>(s => s.EstateNumber == estateDeedNumber);

            if (estateFound)
                throw new BussinessRuleException("EstateAlreadyExists");

            return true;
        }


        public async Task<dynamic> GetEstateTypes()
        {
            var result = await _rentEstateRepository
               .GetQuery<CategoryEstateType>(x => x.Category.Type == CategoryType.Rent && x.EstateType.IsActive, false, [x => x.EstateType, x => x.Category])
               .ToListAsync();

            return _mapper.Map<List<EstateTypeDto>>(result);
        }


        public async Task<dynamic> ListMyEstates(string userId, int pageNumber)
        {
            return await GetListWithPagingAsync(pageNumber, MyConstants.PageSize, estate => estate.UserId == userId, false, [estate => estate.Region.City, estate => estate.User]);
        }


        public async Task<bool> AddNewRentEstate(string userId, CreateRentEstateDto model)
        {
            if (await _rentEstateRepository.RentEstateExists(model.UniqueNumber, userId))
                throw new BussinessRuleException("EstateAlreadyExists");

            try
            {
                var rentEstate = _mapper.Map<RentEstate>(model);
                var rentEstateImages = _mapper.Map<List<RentEstateImage>>(model.Images);
                var rentEstateSpecs = _mapper.Map<List<RentEstateSpecificationValue>>(model.AdditionalSpecs);

                await _unitOfWork.BeginTransactionAsync();

                rentEstate.Images = rentEstateImages;
                rentEstate.SpecificationValues = rentEstateSpecs;

                await _rentEstateRepository.AddAsync(rentEstate);
                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitAsync();

                await _notificationService.SendNotifyToDashboardAsync(new NotificationModel
                {
                    TextAr = "لديك طلب اضافة عقار جديد، يرجي مراجعته",
                    TextEn = "You have a new request to add a new property, please review it",
                    Type = NotifyTypes.AddNewEstate,
                    CategoryType = CategoryType.Rent,
                    RouteId = rentEstate.Id
                });

                return true;
            }
            catch
            {
                await _unitOfWork.RollBackAsync();
                throw new BussinessRuleException("InternalServerErrorOccured");
            }
        }


        public async Task<RentEstateInfoDto> MyRentEstateInfo(string userId, long estateId)
        {
            try
            {
                var rentEstate = await _rentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId && x.UserId == userId
                , true, [x => x.Images, x => x.SpecificationValues]) ??
                    throw new BussinessRuleException("EstateNotFound");

                return _mapper.Map<RentEstateInfoDto>(rentEstate);
            }
            catch
            {
                throw new InternalServerException("InternalServerErrorOccured");
            }
        }


        public async Task<bool> EditRentEstate(string userId, UpdateRentEstateDto model)
        {
            var rentEstate = await _rentEstateRepository
                .FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId, true, [x => x.Images, x => x.SpecificationValues]) ??
                throw new BussinessRuleException("EstateNotFound");

            try
            {
                var updatedRentEstate = _mapper.Map(model, rentEstate);
                var rentEstateImages = _mapper.Map<List<RentEstateImage>>(model.Images);
                var rentEstateSpecs = _mapper.Map<List<RentEstateSpecificationValue>>(model.AdditionalSpecs);

                await _unitOfWork.BeginTransactionAsync();

                if (model.ImagesToBeRemoved.Count != 0)
                {
                    _rentEstateRepository.RemoveRange(rentEstate.Images.Where(x => model.ImagesToBeRemoved.Contains(x.Id)).ToList());
                    await _unitOfWork.SaveChangeAsync();
                }

                if (model.Images.Count != 0)
                {
                    rentEstateImages.ForEach(x => x.RentEstateId = rentEstate.Id);
                    await _rentEstateRepository.AddRangeAsync(rentEstateImages);
                    await _unitOfWork.SaveChangeAsync();
                }

                _rentEstateRepository.RemoveRange(rentEstate.SpecificationValues.ToList());
                await _unitOfWork.SaveChangeAsync();


                updatedRentEstate.SpecificationValues = rentEstateSpecs;
                _rentEstateRepository.Update(updatedRentEstate);
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
            var specificationValues = await _rentEstateRepository.GetListAsync<RentEstateSpecificationValue>(x => x.RentEstateId == estateId, false);
            return _mapper.Map<List<SpecificationValueDto>>(specificationValues);
        }


        public async Task<PageDTO<RentReservationRequestDto>> ListRentReservationRequests(ListReservationRequestsPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;
            var result = await _rentEstateRepository
                .GetListWithPagingAsync<RentReservationRequest>(skip, MyConstants.PageSize, x => x.RentEstateId == model.EstateId && x.ReservationStatus == model.Status, false, x => x.User);
            return _mapper.Map<PageDTO<RentReservationRequestDto>>(result);
        }


        public async Task<RentReservationRequestInfoDto> ReservationRentRequestInfo(long requestId)
        {
            var reservationRequest = await _rentEstateRepository.FirstOrDefaultAsync<RentReservationRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<RentReservationRequestInfoDto>(reservationRequest);
        }


        public async Task<PageDTO<RentRatingRequestDto>> ListRentRatingRequests(ListRatingRequestsPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;
            var result = await _rentEstateRepository
                .GetListWithPagingAsync<RentRatingRequest>(skip, MyConstants.PageSize, x => x.RentEstateId == model.EstateId && x.RatingStatus == model.Status, false, x => x.User);
            return _mapper.Map<PageDTO<RentRatingRequestDto>>(result);
        }


        public async Task<RentRatingRequestInfoDto> RentRatingRequestInfo(long requestId)
        {
            var reservationRequest = await _rentEstateRepository.FirstOrDefaultAsync<RentRatingRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

            return _mapper.Map<RentRatingRequestInfoDto>(reservationRequest);
        }


        public async Task<PageDTO<RentRequestDto>> ListRentRequests(ListRentRequestsPayload model)
        {
            var skip = (model.PageNumber - 1) * MyConstants.PageSize;
            var result = await _rentEstateRepository
                .GetListWithPagingAsync<RentRequest>(skip, MyConstants.PageSize, x => x.RentEstateId == model.EstateId && x.Status != RentStatus.Canceled, false, x => x.User);
            return _mapper.Map<PageDTO<RentRequestDto>>(result);
        }


        public async Task<RentRequestInfoDto> RentRequestInfo(long requestId)
        {
            var rentRequest = await _rentEstateRepository.FirstOrDefaultAsync<RentRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

            var totalMonths = rentRequest.YearCount * 12 + rentRequest.MonthCount;
            var result = _mapper.Map<RentRequestInfoDto>(rentRequest);

            if (totalMonths <= 6)
                result.RequiredPrice = totalMonths * rentRequest.RentEstate.MonthRentPrice;
            else
                result.RequiredPrice = 6 * rentRequest.RentEstate.MonthRentPrice;

            return result;
        }


        public async Task<bool> AcceptRentRequest(long requestId)
        {
            var rentRequest = await _rentEstateRepository.FirstOrDefaultAsync<RentRequest>(x => x.Id == requestId, true, x => x.RentEstate.RentRequests) ??
                throw new BussinessRuleException("RequestNotFound");

            if (rentRequest.Status != RentStatus.New)
                throw new BussinessRuleException("RequestCanNotBeAccepted");

            rentRequest.IsAccepted = true;
            rentRequest.Status = RentStatus.Current;

            var otherRequests = rentRequest.RentEstate.RentRequests.Where(x => x.Id != requestId).ToList();
            foreach (var request in otherRequests)
            {
                request.Status = RentStatus.Finished;
            }
            _rentEstateRepository.Update(rentRequest);
            _rentEstateRepository.UpdateRange(otherRequests);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم قبول طلب ايجارك للعقار رقم {rentRequest.RentEstate.EstateNumber}",
                TextEn = $"Your rent request for property no. {rentRequest.RentEstate.EstateNumber} has been accepted",
                UserId = rentRequest.UserId,
                Type = NotifyTypes.RequestAccepted,
                CategoryType = CategoryType.Rent,
                RouteId = requestId
            });

            return true;
        }


        public async Task<bool> RejectRentRequest(long requestId)
        {
            var rentRequest = await _rentEstateRepository.FirstOrDefaultAsync<RentRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

            if (rentRequest.Status != RentStatus.New)
                throw new BussinessRuleException("RequestCanNotBeRejected");

            rentRequest.Status = RentStatus.Finished;

            _rentEstateRepository.Update(rentRequest);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم رفض طلب ايجارك للعقار رقم {rentRequest.RentEstate.EstateNumber}",
                TextEn = $"Your rent request for property no. {rentRequest.RentEstate.EstateNumber} has been rejected",
                UserId = rentRequest.UserId,
                Type = NotifyTypes.RequestRejected,
                CategoryType = CategoryType.Rent,
                RouteId = requestId
            });

            return true;
        }


        public async Task<bool> ConfirmEstateRented(long estateId, long requestId)
        {
            var rentEstate = await _rentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId) ??
                throw new BussinessRuleException("EstateNotFound");

            var request = await _rentEstateRepository.FirstOrDefaultAsync<RentRequest>(x => x.Id == requestId) ??
                throw new BussinessRuleException("RequestNotFound");

            if (!request.IsAccepted && !request.IsPay)
                throw new BussinessRuleException("RequestIsNotAccepted");

            rentEstate.IsRented = true;
            request.IsRented = true;
            _rentEstateRepository.Update(rentEstate);
            _rentEstateRepository.Update(request);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendNotifyAsync(new NotificationModel
            {
                TextAr = $"تم تأكيد استلام العقار رقم {rentEstate.EstateNumber} من قبل المعلن",
                TextEn = $"The rent of property no. {rentEstate.EstateNumber} has been confirmed by the advertiser",
                UserId = rentEstate.RentRequests.FirstOrDefault(x => x.Id == requestId).UserId,
                Type = NotifyTypes.EstateIsSoldOrRentedConfirmation,
                CategoryType = CategoryType.Rent,
                RouteId = requestId
            });

            return true;
        }


        public async Task<bool> ChangeEstateVisibility(long estateId)
        {
            var rentEstate = await _rentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId) ??
                throw new BussinessRuleException("EstateNotFound");

            rentEstate.IsShow = !rentEstate.IsShow;

            _rentEstateRepository.Update(rentEstate);
            return await _unitOfWork.SaveChangeAsync();
        }


        public async Task<bool> RemoveEstate(long estateId)
        {
            var rentEstate = await _rentEstateRepository.FirstOrDefaultAsync(x => x.Id == estateId) ??
                throw new BussinessRuleException("EstateNotFound");

            if (rentEstate.RentRequests.Any(x => x.Status == RentStatus.Current) ||
               rentEstate.ReservationRequests.Any(x => x.ReservationStatus == ReservationStatus.Current) ||
               rentEstate.RatingRequests.Any(x => x.RatingStatus == RatingStatus.New || x.RatingStatus == RatingStatus.Current))
                throw new BussinessRuleException("EstateCanNotBeRemoved");

            rentEstate.IsDeleted = true;

            _rentEstateRepository.Update(rentEstate);
            return await _unitOfWork.SaveChangeAsync();
        }

        #region Not Implemented Methods
        public Task<bool> ReportComment(string userId, long requestId, string reason)
        {
            throw new NotImplementedException();
        }


        public Task<PageDTO<BaseRatingItemDto>> ListEstateRatings(string userId, long estateId, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SelectReservationPeriod(string userId, long estateId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<string> DownloadFinancialAccounts(string userId, long estateId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RateRentee(string userId, long requestId, double rating)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
