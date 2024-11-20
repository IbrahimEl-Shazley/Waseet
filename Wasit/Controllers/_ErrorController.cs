using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Localization;
using Wasit.Core.Models;
using Wasit.Service.Interfaces.General;

namespace Wasit.API.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;

        public ErrorController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [Route("error")]
        public GlobalResponse Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            string message = Localize(exception.Message);
            //if (Hosting.EnvironmentName == Environments.Production.ToString())
            //    exception = new Exception("): ... :(");

            if (exception is BussinessRuleException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return GlobalResponse.Init().BadRequest(message);
            }
            else if (exception is InternalServerException)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return GlobalResponse.Init().InternalServerError(Localize(message), exception);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return GlobalResponse.Init().InternalServerError(Localize("InternalServerErrorOccured"), exception);
            }
        }

        [Route("badrequest")]
        public GlobalResponse BadRequest(string message = "BadRequest")
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return GlobalResponse.Init().BadRequest(Localize(message));
        }

        [Route("forbidden")]
        public GlobalResponse Forbidden(object data, string message = "Forbidden")
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return GlobalResponse.Init().Forbidden(data, Localize(message));
        }


        [NonAction]
        private string Localize(string key)
        {
            return LocalizerHelper.Localize(key, _currentUserService.Language, MyConstants.GeneralLocalizationPath);
        }
    }
}
