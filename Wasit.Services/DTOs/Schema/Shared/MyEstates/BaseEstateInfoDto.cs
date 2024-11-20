using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.Shared.MyEstates
{
    public class BaseEstateInfoDto : LookupDTO
    {
        public HashSet<BaseImageItemDto> Images { get; set; } = new HashSet<BaseImageItemDto>();
        public string Number { get; set; }
        public bool IsVisible { get; set; }
        public string Addresss { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Location { get; set; }
        public SharedDto City { get; set; }
        public SharedDto Region { get; set; }
        public string Features { get; set; }
        public long EstateTypeId { get; set; }
        public string EstateType { get; set; }
        public HashSet<SpecificationItemDto> Specs { get; set; } = new HashSet<SpecificationItemDto>();
        public string Description { get; set; }
        public bool IsDevelopable { get; set; }
        public virtual double Price { get; set; }
        public double Area { get; set; }
    }
}
