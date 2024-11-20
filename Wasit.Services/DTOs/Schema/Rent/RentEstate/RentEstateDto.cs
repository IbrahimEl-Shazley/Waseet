using System.Text.Json.Serialization;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.Rent.RentEstate
{
    public class RentEstateDto : BaseEstateDto
    {
        [JsonIgnore]
        public override string Type { get; set; }
    }
}
