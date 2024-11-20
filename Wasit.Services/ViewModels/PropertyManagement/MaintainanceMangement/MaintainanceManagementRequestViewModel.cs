using Wasit.Core.Enums;

namespace Wasit.Services.ViewModels.PropertyManagement.MaintainanceMangement
{
    public class MaintainanceManagementRequestViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string EstateName { get; set; }
        public string? DelegateName { get; set; }
        public string? DelegatePhone { get; set; }
        public double Rating { get; set; }
        public RentalManagementOrderStatus Status { get; set; }
    }
}
