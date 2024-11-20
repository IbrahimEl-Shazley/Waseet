using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.Shared.PackagesSection
{
    public class BasePackageItemDto : DTO<long>
    {
        public string Name { get; set; }
        public int Period { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
