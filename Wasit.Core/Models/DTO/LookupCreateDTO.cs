using System.ComponentModel;

namespace Wasit.Core.Models.DTO
{
    public class LookupCreateDTO
    {
        [DisplayName("الاسم بالعريبة")]
        public string NameAr { get; set; }

        [DisplayName("الاسم بالانجليزية")]
        public string NameEn { get; set; }
    }
}
