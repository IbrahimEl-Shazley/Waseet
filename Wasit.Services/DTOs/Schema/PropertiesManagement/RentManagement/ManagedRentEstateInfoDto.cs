using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Shared;

namespace Wasit.Services.DTOs.Schema.PropertiesManagement.RentManagement
{
    public class ManagedRentEstateInfoDto : DTO<long>
    {
        public string UniqueNumber { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public SharedDto City { get; set; }
        public SharedDto Region { get; set; }
        public List<ApartmentItemDto> Apartments { get; set; } = [];
    }
}
