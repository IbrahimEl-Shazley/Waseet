namespace Wasit.Services.ViewModels.PropertyManagement.RentManagement
{
    public class RentManagementRequestDetailsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string UniqueNumber { get; set; }
        public int ApartmentsCount { get; set; }
        public string UserName { get; set; }
        public List<ApartmentViewModel> Apartments { get; set; } = [];
    }
}
