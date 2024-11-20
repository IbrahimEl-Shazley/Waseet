using Wasit.Core.Enums;

namespace Wasit.Services.DTOs.Schema.SEC.Broker
{
    public class IndividualBrokerInfoDto : UserProfileDto
    {
        public AccountType AccountType { get; set; }
        public string BrokerageDocumentNo { get; set; }
    }
}
