using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;

namespace Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest
{
    public class SaleRatingRequestInfoDto
    {
        public long Id { get; set; }
        public SaleEstateDto EstateInfo { get; set; }
        public string UserName { get; set; }
        public double EvaluatuionPrice { get; set; }
        public string DelegateName { get; set; }
        public string? Report { get; set; }
        public RatingStatus StatusEnum { get; set; }
    }
}
