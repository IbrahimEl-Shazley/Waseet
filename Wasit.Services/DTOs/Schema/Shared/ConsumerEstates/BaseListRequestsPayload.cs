using System.ComponentModel;

namespace Wasit.Services.DTOs.Schema.Shared.ConsumerEstates
{
    public class BaseListRequestsPayload
    {
        [DisplayName("رقم الصفحة")]
        public virtual int PageNumber { get; set; } = 1;

        [DisplayName("الحالة")]
        public virtual int Status { get; set; }
    }
}
