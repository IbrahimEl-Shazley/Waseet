using Microsoft.AspNetCore.Http;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.ViewModels.Estates.MyEstates
{
    public class UpdateDailyRentEstateViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDevelopable { get; set; }
        public double Price { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Location { get; set; }
        public double Area { get; set; }
        public string Features { get; set; }
        public long EstateTypeId { get; set; }
        public long CityId { get; set; }
        public long RegionId { get; set; }
        public HashSet<BaseImageItemDto> Images { get; set; } = new HashSet<BaseImageItemDto>();
        public HashSet<IFormFile> ImagesFiles { get; set; } = new HashSet<IFormFile>();
    }
}
