using Wasit.Core.Entities.Shared;
using Subject =  Wasit.Core.Enums.ContactUsSubject;

namespace Wasit.Core.Entities.SettingTables
{
    public class ContactUs : Entity
    {
        public string UserName { get; set; }
        public string PhoneCode { get; set; }
        public string Phone { get; set; }
        public Subject Subject { get; set; }
        public string Msg { get; set; }

        //[ForeignKey(nameof(SubjectId))]
        //public virtual ContactUsSubject Subject { get; set; }
    }
}
