namespace Wasit.Services.ViewModels.Profits.AllPackages
{
    public class PackageViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }
        public int MaxUsageCount { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
    }
}
