using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.PropertiesManagement.MaintainanceMangment
{
    public class MaintainanceOrderItemDto : DTO<long>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string UniqueNumber { get; set; }
        public string Image { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string ProviderName { get; set; }
        public string ProviderPhone { get; set; }
        public bool IsRated { get; set; }
    }
}
