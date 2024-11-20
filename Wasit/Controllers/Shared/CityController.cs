using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wasit.Controllers;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.LOOKUP.Cities;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.API.Controllers.Shared
{
    [ApiExplorerSettings(GroupName = "Shared")]
    public class CityController : BaseController<City, CityDto, CreateCityDto, UpdateCityDto>
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService) : base(cityService)
        {
            _cityService = cityService;
        }

        [AllowAnonymous]
        [HttpGet("ListWithoutPagination")]
        [ProducesResponseType(typeof(List<CityDto>), 200)]
        public async Task<IActionResult> ListWithoutPagination(string filter = "{'search':null}")
        {
            var cfilter = JsonConvert.DeserializeObject<FilterDTO>(filter);
            return _OK(await _cityService.GetListWithoutPagination(cfilter));
        }

        [AllowAnonymous]
        [HttpGet("ListWithPagination")]
        [ProducesResponseType(typeof(PageDTO<CityDto>), 200)]
        public override async Task<IActionResult> GetListWithPaging(string filter = "{'search':null}", [FromQuery] int pageNumber = 1, [FromQuery] int PageSize = 10)
        {
            var sfilter = JsonConvert.DeserializeObject<FilterDTO>(filter);
            return _OK(await _cityService.GetListWithPagination(sfilter, pageNumber, PageSize));
        }


    }
}
