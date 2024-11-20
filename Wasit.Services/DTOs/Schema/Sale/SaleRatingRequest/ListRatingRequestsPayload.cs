using System.ComponentModel.DataAnnotations;
using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.Sale.SaleRatingRequest
{
    public class ListRatingRequestsPayload : BaseListRequestsPayload
    {
        [Required]
        public RatingStatus Status { get; set; }
    }
}
