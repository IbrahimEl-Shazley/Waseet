using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentEstate;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;

namespace Wasit.Services.Implementations.Generic.ConsumerEstates
{
    public class ConsumerFavouriteEstatesService : BaseService, IConsumerFavouriteEstatesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;

        public ConsumerFavouriteEstatesService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _baseRepository = unitOfWork.Repository<IBaseRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
        }


        public async Task<dynamic> ListFavouriteEstates(string userId, CategoryType category, long? estateTypeId, int pageNumber)
        {
            return category switch
            {
                CategoryType.Sale => await FavouriteSaleEstates(userId, estateTypeId, pageNumber),
                CategoryType.Rent => await FavouriteRentEstates(userId, estateTypeId, pageNumber),
                CategoryType.Entertainment => await FavouriteEntertainmentEstates(userId, estateTypeId, pageNumber),
                CategoryType.DailyRent => await FavouriteDailyRentEstates(userId, estateTypeId, pageNumber),
                _ => throw new BussinessRuleException("CategoryNotFound")
            };
        }


        #region Private Methods
        private async Task<dynamic> FavouriteSaleEstates(string userId, long? estateTypeId, int pageNumber)
        {
            var skip = MyConstants.PageSize * (pageNumber - 1);

            var query = _baseRepository.GetQuery<SaleEstateFavorite>(x => x.UserId == userId, false, x => x.SaleEstate.Region.City);

            if (estateTypeId.HasValue && estateTypeId != 0)
                query = query.Where(x => x.SaleEstate.EstateTypeId == estateTypeId);

            var data = await query.OrderByDescending(x => x.Id).Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            var result = new PageDTO<SaleEstateDto>
            {
                CurrentPage = pageNumber,
                Count = data.Count,
                TotalCount = await query.CountAsync(),
                Data = _mapper.Map<List<SaleEstateDto>>(data)
            };

            return result;
        }

        private async Task<dynamic> FavouriteRentEstates(string userId, long? estateTypeId, int pageNumber)
        {
            var skip = MyConstants.PageSize * (pageNumber - 1);

            var query = _baseRepository.GetQuery<RentEstateFavorite>(x => x.UserId == userId, false, x => x.RentEstate.Region.City);

            if (estateTypeId.HasValue && estateTypeId != 0)
                query = query.Where(x => x.RentEstate.EstateTypeId == estateTypeId);

            var data = await query.OrderByDescending(x => x.Id).Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            var result = new PageDTO<RentEstateDto>
            {
                CurrentPage = pageNumber,
                Count = data.Count,
                TotalCount = await query.CountAsync(),
                Data = _mapper.Map<List<RentEstateDto>>(data)
            };

            return result;
        }

        private async Task<dynamic> FavouriteEntertainmentEstates(string userId, long? estateTypeId, int pageNumber)
        {
            var skip = MyConstants.PageSize * (pageNumber - 1);

            var query = _baseRepository.GetQuery<EntertainmentEstateFavorite>(x => x.UserId == userId, false, x => x.EntertainmentEstate.Region.City);

            if (estateTypeId.HasValue && estateTypeId != 0)
                query = query.Where(x => x.EntertainmentEstate.EstateTypeId == estateTypeId);

            var data = await query.OrderByDescending(x => x.Id).Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            var result = new PageDTO<EntertainmentEstateDto>
            {
                CurrentPage = pageNumber,
                Count = data.Count,
                TotalCount = await query.CountAsync(),
                Data = _mapper.Map<List<EntertainmentEstateDto>>(data)
            };

            return result;
        }

        private async Task<dynamic> FavouriteDailyRentEstates(string userId, long? estateTypeId, int pageNumber)
        {
            var skip = MyConstants.PageSize * (pageNumber - 1);

            var query = _baseRepository.GetQuery<DailyRentEstateFavorite>(x => x.UserId == userId, false, x => x.DailyRentEstate.Region.City);

            if (estateTypeId.HasValue && estateTypeId != 0)
                query = query.Where(x => x.DailyRentEstate.EstateTypeId == estateTypeId);

            var data = await query.OrderByDescending(x => x.Id).Skip(skip).Take(MyConstants.PageSize).ToListAsync();

            var result = new PageDTO<DailyRentEstateDto>
            {
                CurrentPage = pageNumber,
                Count = data.Count,
                TotalCount = await query.CountAsync(),
                Data = _mapper.Map<List<DailyRentEstateDto>>(data)
            };

            return result;
        }
        #endregion
    }
}
