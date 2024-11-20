using System.Text.Json.Serialization;
using Wasit.Core.Enums;
using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.EstateCategories.Category
{
    public class CategoryDto : LookupDTO
    {
        public CategoryType Type { get; set; }

        //[JsonIgnore]
        //public override bool IsActive { get; set; }

        [JsonIgnore]
        public override long? Id { get; set; }
    }
}
