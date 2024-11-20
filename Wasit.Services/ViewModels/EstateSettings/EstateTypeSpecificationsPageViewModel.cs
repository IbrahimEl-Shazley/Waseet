namespace Wasit.Services.ViewModels.EstateSettings
{
    public class EstateTypeSpecificationsPageViewModel
    {
        public long Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Categories { get; set; }

        public List<EstateTypeSpecificationViewModel> Specifications { get; set; } = [];
    }
}
