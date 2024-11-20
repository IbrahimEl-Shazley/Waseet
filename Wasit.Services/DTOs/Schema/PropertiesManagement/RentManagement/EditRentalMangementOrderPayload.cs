using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace Wasit.Services.DTOs.Schema.PropertiesManagement.RentManagement
{
    public class EditRentalMangementOrderPayload
    {
        [DisplayName("الاسم")]
        public string Name { get; set; }

        [DisplayName("الحي")]
        public long RegionId { get; set; }

        [DisplayName("الصورة")]
        public IFormFile? Image { get; set; }

        [DisplayName("الشقق المضافة")]
        public List<AddApartmentDto> Apartments { get; set; } = new List<AddApartmentDto>();

        [DisplayName("الشقق المحذوفة")]
        public List<long> ApartmentsToBeRemoved { get; set; } = new List<long>();
    }
}
