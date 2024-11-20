using Microsoft.AspNetCore.Mvc;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.LOOKUP.Cities;

namespace Wasit.Services.Interfaces.Generic.Shared
{
    public interface ICityService : IBaseService<City, CityDto, CreateCityDto, UpdateCityDto>
    {
        Task<List<CityDto>> GetListWithoutPagination(FilterDTO filter);
        Task<PageDTO<CityDto>> GetListWithPagination(FilterDTO filter, [FromQuery] int pageNumber = 1, [FromQuery] int PageSize = 10);
    }
}
