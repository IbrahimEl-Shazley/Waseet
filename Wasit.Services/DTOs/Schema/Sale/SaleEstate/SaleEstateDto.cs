using System.Text.Json.Serialization;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.Sale.SaleEstate
{
    public class SaleEstateDto : BaseEstateDto
    {
        [JsonIgnore]
        public override string Type { get; set; }
    }
}
