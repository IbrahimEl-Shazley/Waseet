using Wasit.Core.Enums;

namespace Wasit.Services.ViewModels.Estates
{
    public class EstateViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UniqueNumber { get; set; }
        public double Price { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public bool IsVisible { get; set; }
    }
}
