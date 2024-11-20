using Wasit.Core.Enums;

namespace Wasit.Services.ViewModels.PropertyManagement.RentManagement
{
    public class RentManagementRequestViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PropertyName { get; set; }
        public string UniqueNumber { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsTerminated { get; set; }
        public bool IsApproved { get; set; }
        public string Status { get; set; }

        public bool IsPending => !IsApproved && !IsCanceled && !IsTerminated;
    }
}
