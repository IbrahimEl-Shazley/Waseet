using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;

namespace Wasit.Services.Interfaces.Generic.ConsumerEstates
{
    public interface IConsumerGeneralSharedService : IBaseService
    {
        Task<PageDTO<BaseInvestmentItemDto>> PublisherInvestmentOpportunities(string publisher, int pageNumber);
        Task<object> GePublisherInfo(string publisher);
    }
}
