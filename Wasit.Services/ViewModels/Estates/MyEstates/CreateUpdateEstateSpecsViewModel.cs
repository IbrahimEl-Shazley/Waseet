using Wasit.Core.Enums;

namespace Wasit.Services.ViewModels.Estates.MyEstates
{
    public class CreateUpdateEstateSpecsViewModel
    {
        public CategoryType Category { get; set; }
        public long EstateId { get; set; }
        public List<SpecificationFormIValueViewModel> Specs { get; set; } = [];
    }


    public class SpecificationFormIValueViewModel
    {
        public long Id { get; set; }
        public string Value { get; set; }
    }
}
