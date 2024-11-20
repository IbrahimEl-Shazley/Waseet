using Wasit.Core.Entities.Shared;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.EstateCategories
{
    public class Specification : LookupEntity
    {
        public Specification()
        {
            EstateSpecifications = new HashSet<EstateTypeSpecification>();
        }

        public string Icon { get; set; }
        public SpecificationType Type { get; set; }

        public virtual ICollection<EstateTypeSpecification> EstateSpecifications { get; set; }



        public string Name(Language lang) => lang == Language.Ar ? NameAr : NameEn;
    }
}
