using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.Shared.MyEstates
{
    public class BaseUpdateEstateDto : LookupUpdateDTO
    {
        [DisplayName("نوع العقار")]
        public int EstateTypeId { get; set; }

        [DisplayName("الحي")]
        public int RegionId { get; set; }

        [DisplayName("خط العرض")]
        public string Lat { get; set; }

        [DisplayName("خط الطول")]
        public string Lng { get; set; }

        [DisplayName("الموقع")]
        public string Location { get; set; }

        [DisplayName("المساحة")]
        public double Area { get; set; }

        [DisplayName("الوصف")]
        public string Description { get; set; }

        [DisplayName("المميزات")]
        public string Features { get; set; }

        [DisplayName("قابل للتطوير")]
        public bool IsDevelopable { get; set; }

        [DisplayName("السعر")]
        public double Price { get; set; }

        [DisplayName("الصور")]
        public List<IFormFile>? Images { get; set; } = new List<IFormFile>();

        public List<long>? ImagesToBeRemoved { get; set; } = new List<long>();

        [DisplayName("الخصائص الاضافية")]
        public List<SpecificationKeyValueDto>? AdditionalSpecs { get; set; } = new List<SpecificationKeyValueDto>();
    }
}
