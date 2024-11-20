using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;

namespace Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest
{
    public class SaleReservationRequestInfoDto
    {
        public long Id { get; set; }
        public SaleEstateDto EstateInfo { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public ReservationStatus StatusEnum { get; set; }
        public string RequestDateTime { get; set; }
    }
}
