using Wasit.Services.DTOs.Schema.PropertiesManagement.RentManagement;

namespace Wasit.Services.DTOs.Schema.PropertiesManagement.PayApartmentRent
{
    public class RentPayerApartmentItemInfoDto : ApartmentItemDto
    {
        public string Status { get; set; }
        public bool IsPaid { get; set; }
    }
}
