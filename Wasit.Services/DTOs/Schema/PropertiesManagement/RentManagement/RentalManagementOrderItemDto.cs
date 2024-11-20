using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.PropertiesManagement.RentManagement
{
    public class RentalManagementOrderItemDto : DTO<long>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string UniqueNumber { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
    }
}
