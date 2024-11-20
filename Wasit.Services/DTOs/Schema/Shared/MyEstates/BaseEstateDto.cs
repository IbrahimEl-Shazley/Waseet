using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.Shared.MyEstates
{
    public class BaseEstateDto : LookupDTO
    {
        public string PublisherName { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public virtual string Type { get; set; }
    }
}
