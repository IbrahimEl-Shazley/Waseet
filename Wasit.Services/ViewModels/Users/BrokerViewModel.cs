using Wasit.Core.Enums;

namespace Wasit.Services.ViewModels.Users
{
    public class BrokerViewModel : UserViewModel
    {
        public string AccountType { get; set; }
    }
    
    public class IndividualBrokerViewModel : AdditionalInfoViewModel
    {
        public string BrokerageDocumentNo { get; set; }
    }
    
    public class FaciltyBrokerViewModel : AdditionalInfoViewModel
    {
        public string FacilityType { get; set; }
        public string CommercialNo { get; set; }
        public string WorkingDocumentNo { get; set; }
    }
}
