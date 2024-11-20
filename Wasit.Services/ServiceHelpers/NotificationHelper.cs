using Wasit.Core.Entities.NOTIFIC;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Localization;
using Wasit.Services.ServiceHelpers.EmailTemplates;

namespace Wasit.Services.ServiceHelpers
{
    public static class NotificationHelper
    {
        public static string FromEmail()
        {
            return Appsettings.GetSettingValue("Email:Email");
        }

        public static string LoadNotificationSubject(NotificationCategoryEnum emailType, Language lang)
        {
            var values = Enum.GetValues(typeof(NotificationCategoryEnum));
            foreach (NotificationCategoryEnum item in values)
            {
                if (item == emailType) return LocalizerHelper.Localize(item.ToString(), lang, MyConstants.GeneralLocalizationPath);
            }
            return string.Empty;
        }

        public static string LoadNotificationBody(INotificationTemplate templateLocator, NotificationTemplate template, dynamic input, Language lang)
        {
            string html = input != null ? input.ToString() : "NO CONTENT";
            if (template == null)
                return html;

            html = (lang == Language.Ar ? template?.TemplateAr : template?.TemplateEn);

            html = templateLocator.Prepare(html, input);

            return html;
        }
    }
}
