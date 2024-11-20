using Wasit.Core.Entities.UserTables;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.LOOKUP.Regions;

namespace Wasit.Services.Interfaces.Generic.Shared
{
    public interface IRegionService : IBaseService<Region, RegionDto, CreateRegionDto, UpdateRegionDto>
    {
        Task<List<RegionDto>> GetListWithoutPagination(RegionFilterDto filter);
        Task<PageDTO<RegionDto>> GetListWithPagination(RegionFilterDto filter, int pageNumber, int pageSize);

    }
}
