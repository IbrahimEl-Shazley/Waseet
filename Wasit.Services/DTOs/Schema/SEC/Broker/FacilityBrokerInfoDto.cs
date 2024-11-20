using Wasit.Core.Enums;

namespace Wasit.Services.DTOs.Schema.SEC.Broker
{
    public class FacilityBrokerInfoDto : IndividualBrokerInfoDto
    {
        public string BrokerageDocumentNo { get; set; }
        public string CommercialNo { get; set; }
        public string LicenseNo { get; set; }
        public AccountType AccountType { get; set; }
        public FacilityType FacilityType { get; set; }
    }
}
