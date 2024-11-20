using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Enums;
using Wasit.Core.ExtensionsMethods;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.IO;
using Wasit.Core.Helpers.Localization;
using Wasit.Core.Models;
using Wasit.Filters;
using Wasit.Services;
using Wasit.Services.Interfaces;

namespace Wasit.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ValidationError]
    [Route("api/[controller]")]
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            ProjectTypeService.IsApi = true;

        }
        protected Language Language
        {
            get
            {
                //var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                //return lang.ToUpper() == "AR" ? Language.Ar : Language.En;

                if (ProjectTypeService.IsApi == false)
                {
                    var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                    return string.Equals(lang, "AR", StringComparison.OrdinalIgnoreCase) ? Language.Ar : Language.En;
                }
                else
                {
                    var lang = Request.Headers.AcceptLanguage.FirstOrDefault("ar").ToUpper();
                    return lang == "AR" ? Language.Ar : Language.En;
                }
            }
        }

        protected string UserId
        {
            get
            {
                //return (JwtManager.GetClaimValue(HttpContext.User.Identity as ClaimsIdentity, "userId").Decrypt() ?? "");
                return HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value.Decrypt()??"";

            }
        }

        protected string UserIdentity
        {
            get
            {
                //return JwtManager.GetClaimValue(HttpContext.User.Identity as ClaimsIdentity, ClaimTypes.NameIdentifier).Decrypt();
                return HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.Decrypt()??"";
            }
        }


        [NonAction]
        protected OkObjectResult _OK(string successMessage = null)
        {
            return new OkObjectResult(GlobalResponse.Init().Success(null, Localize(successMessage)));
        }

        [NonAction]
        protected IActionResult _Forbidden(object data = null, string errorMessage = null)
        {
            return StatusCode(StatusCodes.Status403Forbidden, GlobalResponse.Init().Forbidden(data, Localize(errorMessage)));
        }

        [NonAction]
        protected OkObjectResult _OK(object data = null, string successMessage = null)
        {
            return new OkObjectResult(GlobalResponse.Init().Success(data, Localize(successMessage)));
            
        }

        

        [NonAction]
        protected BadRequestObjectResult _BadRequest(string errorMessage = "BadRequest")
        {
            return new BadRequestObjectResult(GlobalResponse.Init().BadRequest(Localize(errorMessage)));
        }
        [NonAction]
        protected string Localize(string key)
        {
            return LocalizerHelper.Localize(key, Language, MyConstants.GeneralLocalizationPath);
        }
    }


    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TEntity, TListDto, TCreateDto, TUpdateDto> : BaseController where TEntity : Entity
    {
        private readonly IBaseService<TEntity, TListDto, TCreateDto, TUpdateDto> _baseService;

        public BaseController(IBaseService<TEntity, TListDto, TCreateDto, TUpdateDto> baseService)
        {
            _baseService = baseService;
        }


        [HttpGet("GetList")]
        public virtual async Task<IActionResult> GetList()
        {
            return _OK(await _baseService.GetListAsync());
        }

        [HttpGet("GetListWithPaging")]
        public virtual async Task<IActionResult> GetListWithPaging(string filter = "{'search':null}", [FromQuery] int pageNumber = 1, [FromQuery] int PageSize = 10)
        {
            return _OK(await _baseService.GetListWithPagingAsync(pageNumber, PageSize));
        }

        [HttpGet("GetById/{id}")]
        public virtual async Task<IActionResult> GetById(long id)
        {
            return _OK(await _baseService.FindAsync(id));
        }

        [HttpPost("Create")]
        public virtual async Task<IActionResult> Create([FromBody] TCreateDto dto)
        {
            return _OK(await _baseService.AddAndCommitAsync(dto), Localize("ItemCreatedSuccessfully"));
        }

        [HttpPut("Update")]
        public virtual async Task<IActionResult> Update([FromBody] TUpdateDto dto)
        {
            return _OK(await _baseService.UpdateAndCommitAsync(dto), Localize("ItemUpdatedSuccessfully"));
        }

        [HttpDelete("Delete/{id}")]
        public virtual async Task<IActionResult> Delete(long id)
        {
            return _OK(await _baseService.SoftRemoveAndCommitAsync(id), Localize("ItemDeletedSuccessfully"));
        }

        [HttpDelete("HardDelete/{id}")]
        public virtual async Task<IActionResult> HardDelete(long id)
        {
            var allowed = JsonConvert.DeserializeObject<List<string>>(IOHelper.ReadFile(MyConstants.HardDeletePath));
            if (!allowed.Any(x => x == UserIdentity))
                throw new BussinessRuleException("NOT ALLOWED!!");

            return _OK(await _baseService.RemoveAndCommitAsync(id), Localize("ItemDeletedSuccessfully"));
        }

    }
}
