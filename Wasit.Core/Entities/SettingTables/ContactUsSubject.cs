using Wasit.Core.Entities.Shared;

namespace Wasit.Core.Entities.SettingTables
{
    public class ContactUsSubject : LookupEntity
    {
        public ContactUsSubject()
        {
            ContactUs = new HashSet<ContactUs>();
        }
        public virtual ICollection<ContactUs> ContactUs { get; set; }
    }
}
