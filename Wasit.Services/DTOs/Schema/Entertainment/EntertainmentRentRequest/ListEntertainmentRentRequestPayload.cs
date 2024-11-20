using System.ComponentModel.DataAnnotations;
using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.DTOs.Schema.Entertainment.EntertainmentRentRequest
{
    public class ListEntertainmentRentRequestPayload : BaseListRequestsPayload
    {
        [Required]
        public DailyRentStatus Status { get; set; }
    }
}
