using AAITHelper;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.UserTables
{
    public class AdminNotification : Entity
    {
        public string TextAr { get; set; }
        public string TextEn { get; set; }
        public DateTime Date { get; set; } = HelperDate.GetCurrentDate();
        public NotifyTypes Type { get; set; }
        public bool IsSeen { get; set; }
        public long? RouteId { get; set; }
        public CategoryType? CategoryType { get; set; }
    }
}
