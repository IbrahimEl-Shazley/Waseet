using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.DTOs.Schema.Shared;

namespace Wasit.Services.ViewModels.Estates.MyEstates
{
    public class MyEstateDetailsViewModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public HashSet<BaseImageItemDto> Images { get; set; } = new HashSet<BaseImageItemDto>();
        public string Number { get; set; }
        public bool IsVisible { get; set; }
        public string Addresss { get; set; }
        //public string Lat { get; set; }
        //public string Lng { get; set; }
        //public string Location { get; set; }
        //public SharedDto City { get; set; }
        //public SharedDto Region { get; set; }
        //public string Features { get; set; }
        //public long EstateTypeId { get; set; }
        //public string EstateType { get; set; }
        public HashSet<SpecificationItemDto> Specs { get; set; } = new HashSet<SpecificationItemDto>();
        public string Description { get; set; }
        public bool IsDevelopable { get; set; }
        public virtual double Price { get; set; }
        public double Area { get; set; }
        public double PricingServiceCost { get; set; }
        public double Deposit { get; set; }
        public double AveragePrice { get; set; }
        public double FinalSalePrice { get; set; }
        public bool IsReserved { get; set; }
        public bool IsSold { get; set; }
        public bool IsRented { get; set; }
        public int ReservationRequestsCount { get; set; }
        public int EvaluationRequestsCount { get; set; }
        public int PurchaseRequestsCount { get; set; }
        public int RentRequestsCount { get; set; }
        public int DailyRentRequestsCount { get; set; }
        public int EntertainmentRentRequestsCount { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsRejected { get; set; }
    }
}
