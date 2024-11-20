using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Wasit.Core.Models.DTO;

namespace Wasit.Core.Helpers
{
    public static class ReflectionHelper
    {
        public static List<LookupControllerDTO> GetControllers(Type baseType, string nameSpace)
        {
            var res = baseType.Assembly.GetTypes()
             .Where(x => String.Equals(x.Namespace, nameSpace, StringComparison.Ordinal))
             .Where(x => x.Name.Contains("Controller"))
             .Select(x => new LookupControllerDTO { Id = x.Name.Replace("Controller", ""), Name = x.Name })
             .ToList();
            return res;
        }

        public static List<LookupControllerDTO> GetActions(Type baseType, string nameSpace, string controllerName)
        {
            controllerName = controllerName.Contains("Controller") ? controllerName : $"{controllerName}Controller";
            Type? controllerType = baseType.Assembly.GetType($"{nameSpace}.{controllerName}");
            Assembly assembly = controllerType.Assembly;

            var res = assembly.GetTypes()
                .Where(type => controllerType.IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic && !method.IsDefined(typeof(NonActionAttribute), true))
                .Where(method => method.Module.Name == "Wasit.API.dll")
                .Select(x => new LookupControllerDTO { Id = x.Name, Name = x.Name })
                .ToList();
            return res;
        }

        public static async Task<object?> InvokeMethodAsync<TSource>(object instance, string methodName, Type type)
        {
            var task = (Task?)typeof(TSource).GetMethod(methodName).MakeGenericMethod(type).Invoke(instance, new object[] { });
            await task;
            var resultProperty = task.GetType().GetProperty("Result");
            return resultProperty.GetValue(task);
        }
    }
}
