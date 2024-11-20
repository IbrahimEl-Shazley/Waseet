using Newtonsoft.Json;
using Wasit.Core.Enums;
using Wasit.Core.ExtensionsMethods;
using Wasit.Core.Models;

namespace Wasit.Core.Helpers.Localization
{
    public static class LocalizerHelper
    {
        private static readonly string generalLocalizationPath = Path.Combine(Hosting.WebRootPath, "Localization", "general-localization.json").ToUniformedPath();
        private static readonly string reportLocalizationPath = Path.Combine(Hosting.WebRootPath, "Localization", "report-localization.json").ToUniformedPath();
        private static readonly string validationLocalizationPath = Path.Combine(Hosting.WebRootPath, "Localization", "fluent-validation-localization.json").ToUniformedPath();

        public static string? Localize(string key, Language lang, string? path = null)
        {
            if (string.IsNullOrEmpty(path))
                path = generalLocalizationPath;

            return Localize(path, key, lang);
        }

        public static string? LocalizeReport(string key, Language lang, string? path = null)
        {
            if (string.IsNullOrEmpty(path))
                path = reportLocalizationPath;

            return Localize(path, key, lang);
        }

        public static string LocalizeValidation(string key, Language lang, string? path = null, params object[] inputs)
        {
            if (string.IsNullOrEmpty(path))
                path = validationLocalizationPath;

            var message = Localize(path, key, lang);
            if (message == null)
                return string.Empty;

            return string.Format(message, inputs);
        }

        public static string? Localize(string path, string key, Language lang)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            var json = File.ReadAllText(path);
            List<LocalizerDTO>? localization = JsonConvert.DeserializeObject<List<LocalizerDTO>>(json);

            if (localization == null || !localization.Any())
                return string.Empty;

            var target = localization.FirstOrDefault(x => x.key == key);
            if (target != null)
            {
                return lang == Language.Ar ? target.ValueAr : target.ValueEn;
            }
            return key;
        }
    }

    internal class LocalizerDTO
    {
        public string? key { get; set; }
        public string? ValueAr { get; set; }
        public string? ValueEn { get; set; }
    }
}
