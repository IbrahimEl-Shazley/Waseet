using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Wasit.Core.Helpers
{
    public static class AttributeHelper
    {
        public static string? GetAttributeArgumentValue(ActionExecutingContext actionContext, string AttributeName, string ArgumentName)
        {
            string? namedArgumentResult = string.Empty;
            var controllerActionDescriptor = actionContext.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var actionAttributess = controllerActionDescriptor.MethodInfo.GetCustomAttributesData();
                var controllerAttributes = ((Microsoft.AspNetCore.Mvc.ControllerBase)actionContext.Controller).ControllerContext.ActionDescriptor.ControllerTypeInfo.CustomAttributes;

                if (actionAttributess != null)
                {
                    var authorizeAttribute = actionAttributess.FirstOrDefault(x => x.AttributeType.Name == AttributeName);
                    if (authorizeAttribute == null)
                        authorizeAttribute = controllerAttributes.FirstOrDefault(x => x.AttributeType.Name == AttributeName);

                    if (authorizeAttribute != null)
                    {
                        if (authorizeAttribute.NamedArguments.Count > 0)
                        {
                            var namedargument = authorizeAttribute.NamedArguments.FirstOrDefault(x => x.MemberName == ArgumentName);
                            namedArgumentResult = namedargument.TypedValue.Value != null ? namedargument.TypedValue.Value.ToString() : string.Empty;
                        }
                    }
                }
            }
            return namedArgumentResult;
        }
    }
}
