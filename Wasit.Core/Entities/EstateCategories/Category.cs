using Wasit.Core.Entities.Shared;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.EstateCategories
{
    public class Category : LookupEntity
    {
        public Category()
        {
            CategoryEstateTypes = new HashSet<CategoryEstateType>();
        }


        public CategoryType Type { get; set; }
        public string Image { get; set; }
        public virtual ICollection<CategoryEstateType> CategoryEstateTypes { get; set; }
    }
}
