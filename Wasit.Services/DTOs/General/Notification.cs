using Wasit.Core.Enums;

namespace Wasit.Services.DTOs.General
{
    public class Notification
    {
        public virtual NotificationCategoryEnum NotificationCategory { get; set; }
        public virtual string To { get; set; }
        public dynamic Input { get; set; }
    }

    public class SMS : Notification
    {

    }

    public class Email : Notification
    {

    }
}
