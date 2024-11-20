using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;

namespace Wasit.Services.DTOs.Schema.Rent.RentRequest
{
    public class RentRequestInfoDto
    {
        public long Id { get; set; }
        public RentEstateDto EstateInfo { get; set; }
        public string UserName { get; set; }
        public bool IsAccepted { get; set; }
        public int Months { get; set; }
        public int Years { get; set; }
        public double RequiredPrice { get; set; }
        public RentStatus StatusEnum { get; set; }
        public string Status { get; set; }
        public bool IsRented { get; set; }
        public bool IsPaid { get; set; }
    }
}
