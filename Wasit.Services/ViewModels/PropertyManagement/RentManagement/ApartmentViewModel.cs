namespace Wasit.Services.ViewModels.PropertyManagement.RentManagement
{
    public class ApartmentViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public double Value { get; set; }
        public string PaymentDate { get; set; }
        public string IsPaid { get; set; }
    }
}
