namespace Wasit.Core.Models.DTO
{
    public class LookupDTO : DTO<long?>
    {
        public virtual string Name { get; set; }
 
        //public virtual bool IsActive { get; set; }
    }
}
