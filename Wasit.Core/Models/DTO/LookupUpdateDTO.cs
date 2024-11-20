using System.ComponentModel;

namespace Wasit.Core.Models.DTO
{
    public class LookupUpdateDTO : DTO<long?>
    {
        [DisplayName("الاسم بالعربية")]
        public string NameAr { get; set; }

        [DisplayName("الاسم بالانجليزية")]
        public string NameEn { get; set; }
        //public bool IsActive { get; set; }

    }
}
