using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Wasit.Core.Models;

namespace Wasit.Filters
{
    public class ValidationErrorAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is RedirectToRouteResult)
                return;

            if (((ObjectResult)context.Result).Value is GlobalResponse)
                return;

            var validationErrors = ((ValidationProblemDetails)((ObjectResult)context.Result).Value).Errors;
            if (validationErrors != null && validationErrors.Any())
            {
                StringBuilder message = new StringBuilder();
                foreach (var error in validationErrors)
                {
                    message.AppendLine();
                    message.Append($"{error.Value[0]}");
                }

                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                            { "controller", "error" },
                            { "action", "badrequest" },
                            { "message", message }
                        });
            }
            base.OnResultExecuting(context);
        }
    }
}
