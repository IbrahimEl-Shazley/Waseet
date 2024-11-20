using Wasit.Core.Enums;
using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.Shared.SharedDefinition.Notifications
{
    public class NotificationItemDto : DTO<long>
    {
        public string Text { get; set; }
        public string TimeSpan { get; set; }
        public NotifyTypes Type { get; set; }
        public string UserId { get; set; }
        public long? ItemId { get; set; }
        public CategoryType? CategoryType { get; set; }
    }
}
