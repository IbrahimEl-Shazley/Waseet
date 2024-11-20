using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.EstateCategories
{
    public class CategoryEstateType : LookupEntity
    {
        public CategoryEstateType()
        {
            NameAr = string.Empty;
            NameEn = string.Empty;
            IsActive = true;
        }

        [Column(TypeName = "int")]
        public override long Id { get; set; }
        public long CategoryId { get; set; }
        public long EstateTypeId { get; set; }

        public virtual Category Category { get; set; }
        public virtual EstateType EstateType { get; set; }
    }
}
