using Wasit.Core.Enums;
using Wasit.Services.ServiceHelpers.EmailTemplates;

namespace Wasit.Services.ServiceHelpers
{
    public static class NotificationTemplateLocator
    {
        public static readonly Dictionary<NotificationCategoryEnum, Type> Templates = new Dictionary<NotificationCategoryEnum, Type>
        {
            { NotificationCategoryEnum.ActivateAccount, typeof(ActivateAccountTemplate) },
            { NotificationCategoryEnum.ResetPassword, typeof(ResetPasswordTemplate) }
            //{ NotificationCategoryEnum.CreateBrandRequest, typeof(CreateBrandRequestTemplate) }
        };
    }
}
