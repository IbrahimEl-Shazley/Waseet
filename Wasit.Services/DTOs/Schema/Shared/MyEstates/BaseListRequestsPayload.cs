using System.ComponentModel.DataAnnotations;

namespace Wasit.Services.DTOs.Schema.Shared.MyEstates
{
    public class BaseListRequestsPayload
    {
        [Required]
        public long EstateId { get; set; }

        [Required]
        public int PageNumber { get; set; } = 1;
    }
}
