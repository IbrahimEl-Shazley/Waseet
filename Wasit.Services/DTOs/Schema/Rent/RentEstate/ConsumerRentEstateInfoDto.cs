using System.Text.Json.Serialization;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;

namespace Wasit.Services.DTOs.Schema.Rent.RentEstate
{
    public class ConsumerRentEstateInfoDto : BaseEstateInfoDto
    {
        [JsonPropertyName("rentPrice")]
        public override double Price { get; set; }
        public PublisherInfoDto PublisherInfo { get; set; }
        public string ManagementPhoneNumber { get; set; }
        public double AddReservationRequestCost { get; set; }
        public double AssignDelegateCost { get; set; }
        public bool IsRented { get; set; }
        public bool IsReserved { get; set; }
    }
}
