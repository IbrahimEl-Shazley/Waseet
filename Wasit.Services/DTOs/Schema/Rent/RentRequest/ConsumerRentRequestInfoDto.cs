namespace Wasit.Services.DTOs.Schema.Rent.RentRequest
{
    public class ConsumerRentRequestInfoDto
    {
        public int Months { get; set; }
        public int Years { get; set; }
        public double RentPrice { get; set; }
        public string? OwnerName { get; set; }
        public double? PaidAmount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsRated { get; set; }
        public string Status { get; set; }
    }
}
