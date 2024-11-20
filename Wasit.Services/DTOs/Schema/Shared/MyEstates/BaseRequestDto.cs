using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.Shared.MyEstates
{
    public class BaseRequestDto : DTO<long>
    {
        public string UserName { get; set; }
        public virtual string Status { get; set; }
    }
}
