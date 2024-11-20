namespace Wasit.Services.DTOs.Schema.Shared.ConsumerRequests
{
    public class RatingInfoDto
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public string TimeSpan { get; set; }
    }
}
