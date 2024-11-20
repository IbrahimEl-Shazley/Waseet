using System.ComponentModel.DataAnnotations;
using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.Sale.SaleReservationRequest
{
    public class ListReservationRequestsPayload : BaseListRequestsPayload
    {
        [Required]
        public ReservationStatus Status { get; set; }
    }
}
