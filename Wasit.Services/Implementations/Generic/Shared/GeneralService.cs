using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Entities.SettingTables;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Enums;
using Wasit.Core.Models.IO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.Services.Implementations.Generic.Shared
{
    public class GeneralService : BaseService, IGeneralService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IBaseRepository _baseRepository;

        public GeneralService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _uow = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _baseRepository = uow.Repository<IBaseRepository>();
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
        }


        public async Task<string> TermsAndConditions(Language lang, string userType)
        {
            return await _baseRepository.GetQuery<Setting>(x => true, false)
                .Select(x => x.TermsAndConditions(lang, userType))
                .FirstOrDefaultAsync();
        }

        public async Task<string> AboutUs(Language lang)
        {
            return await _baseRepository.GetQuery<Setting>(x => true, false)
                .Select(x => x.AboutUs(lang))
                .FirstOrDefaultAsync();
        }


        public async Task<double> CalcAverageUserRating(string userId)
        {
            var ratingTasks = new[]
            {
                GetAverageRating<RentRequest>(x => x.RentEstate.UserId == userId, x => x.OwnerRating),
                GetAverageRating<RentRatingRequest>(x => x.ProviderId == userId, x => x.ReviewerRating),
                GetAverageRating<SaleRatingRequest>(x => x.ProviderId == userId, x => x.UserRating),
                GetAverageRating<DailyRentRequest>(x => x.UserId == userId, x => x.OwnerRating),
                GetAverageRating<DailyRentRequest>(x => x.DailyRentEstate.UserId == userId, x => x.UserRating),
                GetAverageRating<EntertainmentRequest>(x => x.UserId == userId, x => x.OwnerRating),
                GetAverageRating<EntertainmentRequest>(x => x.EntertainmentEstate.UserId == userId, x => x.UserRating)
            };

            var ratings = await Task.WhenAll(ratingTasks);
            var averageRating = ratings.Average();
            return Math.Round(averageRating, 1);
        }



        public async Task<double> CalcAverageEstateRating(long estateId)
        {
            var ratingTasks = new[]
            {
                GetAverageRating<DailyRentRequest>(x => x.DailyRentEstateId == estateId, x => x.EstateRating),
                GetAverageRating<EntertainmentRequest>(x => x.EntertainmentEstateId == estateId, x => x.EstateRating)
            };

            var ratings = await Task.WhenAll(ratingTasks);
            var averageRating = ratings.Average();
            return Math.Round(averageRating, 1);
        }



        private Task<double> GetAverageRating<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, double>> selector) where T : Entity
        {            
            var ratingsQuery = _baseRepository.GetQuery(predicate, false).Select(selector);
            return ratingsQuery.Any() ? Task.FromResult(ratingsQuery.Average()) :Task.FromResult( 0.0);
        }
    }
}
