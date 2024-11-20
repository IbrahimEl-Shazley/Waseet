using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wasit.Controllers;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.LOOKUP.Regions;
using Wasit.Services.Interfaces.Generic.Shared;

namespace Wasit.API.Controllers.Shared
{
    [ApiExplorerSettings(GroupName = "Shared")]
    public class RegionsController : BaseController<Region, RegionDto, CreateRegionDto, UpdateRegionDto>
    {
        private readonly IRegionService _regionService;

        public RegionsController(IRegionService regionService) : base(regionService)
        {
            _regionService = regionService;
        }

        [AllowAnonymous]
        [HttpGet("ListWithoutPagination")]
        [ProducesResponseType(typeof(List<RegionDto>), 200)]
        public async Task<IActionResult> ListWithoutPagination(string filter = "{'search':null, 'cityId': 0}")
        {
            var rfilter = JsonConvert.DeserializeObject<RegionFilterDto>(filter);
            return _OK(await _regionService.GetListWithoutPagination(rfilter));
        }

        [AllowAnonymous]
        [HttpGet("ListWithPagination")]
        [ProducesResponseType(typeof(PageDTO<RegionDto>), 200)]
        public override async Task<IActionResult> GetListWithPaging(string filter = "{'search':null, 'cityId': 0}", [FromQuery] int pageNumber = 1, [FromQuery] int PageSize = 10)
        {
            var rfilter = JsonConvert.DeserializeObject<RegionFilterDto>(filter);
            return _OK(await _regionService.GetListWithPagination(rfilter, pageNumber, PageSize));
        }

    }
}
