using Wasit.Core.Enums;

namespace Wasit.Services.ViewModels.Estates.MyEstates
{
    public class MyRequestDetailsViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public MyEstateInfoCardViewModel EstateInfo { get; set; }
        public MyReservationRequestDetailsViewModel? ReservationRequestDetails { get; set; }
        public MyRatingRequestDetailsViewModel? RatingRequestDetails { get; set; }
        public MyPurchaseRequestDetailsViewModel? PurchaseRequestDetails { get; set; }
        public MyRentRequestDetailsViewModel? RentRequestDetails { get; set; }
        public MyDailyRentRequestDetailsViewModel? DailyRentRequestDetails { get; set; }
        public MyEntertainmentRentRequestDetailsViewModel? EntertainmentRentRequestDetails { get; set; }
    }


    public class MyReservationRequestDetailsViewModel
    {
        public string Status { get; set; }
        public string TimeSpan { get; set; }
    }
    
    
    public class MyRatingRequestDetailsViewModel
    {
        public long Id { get; set; }
        public double Price { get; set; }
        public string? DelegateName { get; set; }
        public string? Report { get; set; }
    }

    public class MyPurchaseRequestDetailsViewModel
    {
        public long Id { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsRejected { get; set; }
        public bool HasRefundRequest { get; set; }
        public string Status { get; set; }
        public PurchaseStatus StatusEnum { get; set; }
        public double Deposit { get; set; }
        public double FinalPrice { get; set; }
        public bool IsPaid { get; set; }
    }
    
    
    public class MyRentRequestDetailsViewModel
    {
        public long Id { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsRented { get; set; }
        public string Status { get; set; }
        public RentStatus StatusEnum { get; set; }
        public int MonthsCount { get; set; }
        public int YearsCount { get; set; }
        public bool IsPaid { get; set; }
    }

    public class MyDailyRentRequestDetailsViewModel
    {
        public long Id { get; set; }
        public DailyRentStatus StatusEnum { get; set; }
        public int DaysCount { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
        public string TimeSpan { get; set; }
        public string CancelDate { get; set; }
    }


    public class MyEntertainmentRentRequestDetailsViewModel
    {
        public long Id { get; set; }
        public DailyRentStatus StatusEnum { get; set; }
        public int DaysCount { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
        public string TimeSpan { get; set; }
        public string CancelDate { get; set; }
    }

    public class MyEstateInfoCardViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public double Price { get; set; }
    }


}
