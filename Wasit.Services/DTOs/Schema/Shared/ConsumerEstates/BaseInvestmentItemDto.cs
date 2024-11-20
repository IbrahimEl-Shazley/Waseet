using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.Shared.ConsumerEstates
{
    public class BaseInvestmentItemDto : DTO<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
