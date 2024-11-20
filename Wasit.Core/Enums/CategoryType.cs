using System.ComponentModel.DataAnnotations;

namespace Wasit.Core.Enums
{
    public enum CategoryType : byte
    {
        [Display(Name = "شراء")]
        Sale = 1,
        [Display(Name = "ايجار")]
        Rent = 2,
        [Display(Name = "ايجار يومي")]
        DailyRent = 3,
        [Display(Name = "ايجار ترفيهي")]
        Entertainment = 4
    }
}
