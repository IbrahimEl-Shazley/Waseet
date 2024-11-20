using Wasit.Core.Entities.UserTables;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.LOOKUP.Regions;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.Services.Implementations.Generic.Shared
{
    public class RegionService : BaseService<Region, RegionDto, CreateRegionDto, UpdateRegionDto>, IRegionService
    {
        public RegionService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(uow, serviceProvider)
        {
        }


        public async Task<List<RegionDto>> GetListWithoutPagination(RegionFilterDto filter)
        {
            return await GetListAsync(x =>
            x.IsActive &&
            (filter.Search == null || x.NameAr.Contains(filter.Search) || x.NameEn.Contains(filter.Search)) &&
            (filter.CityId == 0 || x.CityId == filter.CityId));
        }

        public async Task<PageDTO<RegionDto>> GetListWithPagination(RegionFilterDto filter, int pageNumber, int pageSize)
        {
            return await GetListWithPagingAsync(pageNumber, pageSize, x =>
            x.IsActive &&
            (filter.Search == null || x.NameAr.Contains(filter.Search) || x.NameEn.Contains(filter.Search)) &&
            (filter.CityId == 0 || x.CityId == filter.CityId));
        }
    }
}
