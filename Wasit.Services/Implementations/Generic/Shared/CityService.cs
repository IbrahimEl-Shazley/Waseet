using Microsoft.AspNetCore.Mvc;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.LOOKUP.Cities;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.Services.Implementations.Generic.Shared
{
    public class CityService : BaseService<City, CityDto, CreateCityDto, UpdateCityDto>, ICityService
    {
        public CityService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(uow, serviceProvider)
        {

        }


        public async Task<List<CityDto>> GetListWithoutPagination(FilterDTO filter)
        {
            return await GetListAsync(x =>
            x.IsActive &&
            (filter.Search == null ||
            x.NameAr.Contains(filter.Search) ||
            x.NameEn.Contains(filter.Search)));
        }

        public async Task<PageDTO<CityDto>> GetListWithPagination(FilterDTO filter, [FromQuery] int pageNumber = 1, [FromQuery] int PageSize = 10)
        {
            return await GetListWithPagingAsync(pageNumber, PageSize, x =>
                x.IsActive &&
                (filter.Search == null ||
                x.NameAr.Contains(filter.Search) ||
                x.NameEn.Contains(filter.Search)));
        }
    }
}
