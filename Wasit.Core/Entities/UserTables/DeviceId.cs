using AAITHelper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wasit.Core.Entities.UserTables
{
    public class DeviceId
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DeviceId_ { get; set; }
        public string DeviceType { get; set; }
        public DateTime Date { get; set; } = HelperDate.GetCurrentDate();

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }
    }
}
