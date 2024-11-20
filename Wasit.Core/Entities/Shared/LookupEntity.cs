using System.ComponentModel.DataAnnotations;

namespace Wasit.Core.Entities.Shared
{
    public abstract class LookupEntity : Entity
    {
        [MaxLength(100)]
        public virtual string NameAr { get; set; }

        [MaxLength(100)]
        public virtual string NameEn { get; set; }

        public bool IsActive { get; set; }
    }
}
