namespace Wasit.Services.DTOs.Schema.PropertiesManagement.PayApartmentRent
{
    public class RentPayerApartmentInfoDto
    {
        public string UniqueNumber { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string PublisherName { get; set; }
        public RentPayerApartmentItemInfoDto Apartment { get; set; }
    }
}
