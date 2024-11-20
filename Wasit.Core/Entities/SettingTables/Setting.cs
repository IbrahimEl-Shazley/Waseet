using System.ComponentModel.DataAnnotations.Schema;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.SettingTables
{
    public class Setting : Entity
    {
        [Column(TypeName ="int")]
        public long Id { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Address { get; set; }

        #region Terms And Conditions
        public string ConditionsOwnerAr { get; set; }
        public string CondtionsOwnerEn { get; set; }

        public string ConditionsDelegateAr { get; set; }
        public string ConditionsDelegateEn { get; set; }
        
        public string ConditionsBrokerAr { get; set; }
        public string ConditionsBrokerEn { get; set; }
        
        public string ConditionsDeveloperAr { get; set; }
        public string ConditionsDeveloperEn { get; set; }
        #endregion

        #region About Us
        public string AboutUsAr { get; set; }
        public string AboutUsEn { get; set; }
        #endregion

        public string SenderName_SMS { get; set; } = "test";
        public string UserName_SMS { get; set; } = "test";
        public string Password_SMS { get; set; } = "test";

        public string ServerKey_FCM { get; set; }
        public string SenderId_FCM { get; set; }


        public string TermsAndConditions(Language lang, string userType) => userType switch
        {
            nameof(UserType.Owner) => lang == Language.Ar ? ConditionsOwnerAr : CondtionsOwnerEn,
            nameof(UserType.Delegate) => lang == Language.Ar ? ConditionsDelegateAr : ConditionsDelegateEn,
            nameof(UserType.Developer) => lang == Language.Ar ? ConditionsDeveloperAr : ConditionsDeveloperEn,
            nameof(UserType.Broker) => lang == Language.Ar ? ConditionsBrokerAr : ConditionsBrokerEn,
            _ => string.Empty
        };


        public string AboutUs(Language lang) => lang switch
        {
            Language.Ar => AboutUsAr,
            Language.En => AboutUsEn,
            _ => string.Empty
        };
    }
}
