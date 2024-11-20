using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.Shared.ConsumerEstates
{
    public class BaseEstateInfoDto : LookupDTO
    {
        public HashSet<BaseImageItemDto> Images { get; set; } = new HashSet<BaseImageItemDto>();
        public string Number { get; set; }
        public string Addresss { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Location { get; set; }
        public string EstateType { get; set; }
        public HashSet<SpecificationItemDto> Specs { get; set; } = new HashSet<SpecificationItemDto>();
        public string Description { get; set; }
        public virtual double Price { get; set; }
        public bool IsFavourite { get; set; }
    }
}
