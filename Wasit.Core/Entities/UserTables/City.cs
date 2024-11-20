using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.UserTables
{
    public class City : LookupEntity
    {
        public City()
        {
            Regions = new HashSet<Region>();
        }
   
        public virtual ICollection<Region> Regions { get; set; }
    }
}
