namespace Wasit.Services.DTOs.Schema.Rent.RentRatingRequest
{
    public class ConsumerRatingRequestInfoForRentEstateDto
    {
        public double EvaluationPrice { get; set; }
        public string DelegateName { get; set; }
        public string? DelegatePhone { get; set; }
        public string? DelegateReport { get; set; }
        public bool IsRated { get; set; }
    }
}
