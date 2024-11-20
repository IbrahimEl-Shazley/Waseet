using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Security.Claims;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.ExtensionsMethods;
using Wasit.Service.Interfaces.General;
using Wasit.Services;

namespace Wasit.Service.Implementation.General
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //public string UserId => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value.Decrypt();

        public string UserId
        {
            get
            {
                if (ProjectTypeService.IsApi == false)
                {
                    return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                else
                {
                    return _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value.Decrypt();
                }
            }
        }

        //public string UserId => _httpContextAccessor.HttpContext?.User.Identity.Name;

        public Language Language
        {
            get
            {
                if (ProjectTypeService.IsApi == false)
                {
                    var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                    return string.Equals(lang, "AR", StringComparison.OrdinalIgnoreCase) ? Language.Ar : Language.En;
                }
                else
                {
                    var lang = _httpContextAccessor.HttpContext?.Request.Headers.AcceptLanguage.FirstOrDefault("ar");
                    return string.Equals(lang, "AR", StringComparison.OrdinalIgnoreCase) ? Language.Ar : Language.En;
                }
            }
        }

        //public string UserId
        //{
        //    get
        //    {
        //        if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        //            return null;
        //        return (JwtManager.GetClaimValue(_httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity, "userId".Decrypt() ?? "");
        //    }
        //}

        public bool IsArabic => Language == Language.Ar;



    }
}
