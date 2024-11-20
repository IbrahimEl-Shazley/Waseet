using Wasit.Core.Enums;

namespace Wasit.Services.ViewModels.Profits
{
    public class ProfitViewModel
    {
        public string Name { get; set; }
        public double  Cost { get; set; }
        public ServiceType ServiceType { get; set; }
        public ProfitType DisplayType { get; set; }
    }
}
