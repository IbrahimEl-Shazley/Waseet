using Wasit.Core.Entities.Shared;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.SettingTables
{
    public class Service : Entity
    {
        public string ServiceName { get; set; }
        public double ServiceCost { get; set; }
        public ServiceType Type { get; set; }
        public ProfitType DisplayType { get; set; }
        public bool IsActive { get; set; }
    }
}
