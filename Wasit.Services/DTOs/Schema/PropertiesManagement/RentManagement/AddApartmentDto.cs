using Wasit.Core.Enums;

namespace Wasit.Services.DTOs.Schema.PropertiesManagement.RentManagement
{
    public class AddApartmentDto
    {
        public string Name { get; set; }
        public double RentPrice { get; set; }
        public PaymentDeadline PaymentDeadline { get; set; }
    }
}
