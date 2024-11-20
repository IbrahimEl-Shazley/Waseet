using Wasit.Core.Enums;

namespace Wasit.Services.ViewModels.Estates.MyEstates
{
    public class SpecificationFormItemViewModel
    {
        public long Id { get; set; }
        public long EstateTypeSpecificationId { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public bool IsRequired { get; set; }
        public SpecificationType Type { get; set; }
        public string Value { get; set; }
    }
}
