
using Wasit.Core.Enums;

namespace Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications
{
    public class NotificationModel
    {
        public string? Title { get; set; }
        public string TextAr { get; set; }
        public string TextEn { get; set; }
        public NotifyTypes Type { get; set; }
        public string? UserId { get; set; }
        public long? RouteId { get; set; }
        public CategoryType? CategoryType { get; set; }
    }
}
