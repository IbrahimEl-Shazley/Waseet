using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.PropertiesManagement.RentManagement
{
    public class ApartmentItemDto : DTO<long>
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string PaymentDeadline { get; set; }
        public double RentPrice { get; set; }
        public string Status { get; set; }
    }
}
