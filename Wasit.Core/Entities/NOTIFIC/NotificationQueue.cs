using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Enum;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.NOTIFIC
{
    [Table("NotificationQueue")]
    public class NotificationQueue : Entity
    {
        [Required]
        [ForeignKey(nameof(NotificationTypeId))]
        public Nullable<int> NotificationTypeId { get; set; }
        public virtual NotificationType NotificationType { get; set; }

        [Required]
        [ForeignKey(nameof(NotificationCategoryId))]
        public Nullable<int> NotificationCategoryId { get; set; }
        public virtual NotificationCategory NotificationCategory { get; set; }

        [Required]
        [MaxLength(50)]
        public string From { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string To { get; set; }

        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }
        
        [Required]
        public string Body { get; set; }

        public bool IsSent { get; set; }
    }
}
