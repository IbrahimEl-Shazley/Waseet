namespace Wasit.Services.ViewModels.EstateSettings
{
    public class CreateEstateTypeSpecificationViewModel
    {
        public long Id { get; set; }
        public bool IsRequired { get; set; }
    }

    public class UpdateEstateTypeSpecificationViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
    }
}
