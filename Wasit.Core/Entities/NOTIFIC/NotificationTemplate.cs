using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Enum;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.NOTIFIC
{
    [Table("NotificationTemplate")]
    public class NotificationTemplate : Entity
    {
        [ForeignKey("NotificationTypeId")]
        public virtual NotificationType NotificationType { get; set; }
        public int NotificationTypeId { get; set; }

        [ForeignKey("NotificationCategoryId")]
        public virtual NotificationCategory NotificationCategory { get; set; }
        public int NotificationCategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string NameAr { get; set; }

        [MaxLength(50)]
        public string NameEn { get; set; }

        [Required]
        public string TemplateAr { get; set; }

        [Required]
        public string TemplateEn { get; set; }
    }
}
