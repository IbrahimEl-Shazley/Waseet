using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Wasit.Core.ExtensionsMethods;

namespace Wasit.Core.Helpers.Sql
{
    public static class SeedHelper
    {
        public static void GenericSeed<T>(DbContext context, string rootPath) where T : class
        {
            var path = Path.Combine(rootPath, typeof(T).Name + ".json").ToUniformedPath();

            if (string.IsNullOrEmpty(path))
                return;

            if (!File.Exists(path))
                return;

            if (context.Set<T>().Any())
                return;

            List<T>? list = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));
            foreach (var item in list)
            {
                context.Add(item);
            }
        }

        /// <summary>
        /// object must have properties exact as (Id, Code, NameAr, NameEn)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="context"></param>
        public static void GenericEnumSeed<T, TEnum>(DbContext context) where T : class where TEnum : Enum
        {
            if (context.Set<T>().Any())
                return;

            var list = Enum.GetValues(typeof(TEnum));
            foreach (TEnum item in list)
            {
                var obj = (T)Activator.CreateInstance(typeof(T));

                if (typeof(T).GetProperty("Id") != null) typeof(T).GetProperty("Id").SetValue(obj, (int)(object)item);
                if (typeof(T).GetProperty("Code") != null) typeof(T).GetProperty("Code").SetValue(obj, item.ToString());
                if (typeof(T).GetProperty("NameAr") != null) typeof(T).GetProperty("NameAr").SetValue(obj, item.GetDescription());
                if (typeof(T).GetProperty("NameEn") != null) typeof(T).GetProperty("NameEn").SetValue(obj, item.ToString().SplitPascal());

                context.Add(obj);
            }
        }

    }
}
