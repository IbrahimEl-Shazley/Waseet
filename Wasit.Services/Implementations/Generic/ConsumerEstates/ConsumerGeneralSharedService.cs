using AutoMapper;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.DTOs.Schema.Shared.ConsumerEstates;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;

namespace Wasit.Services.Implementations.Generic.ConsumerEstates
{
    public class ConsumerGeneralSharedService : BaseService, IConsumerGeneralSharedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;

        public ConsumerGeneralSharedService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _baseRepository = unitOfWork.Repository<IBaseRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
        }

        public Task<object> GePublisherInfo(string publisher)
        {
            throw new BussinessRuleException("NotImplementedYaFlutter");
        }

        public async Task<PageDTO<BaseInvestmentItemDto>> PublisherInvestmentOpportunities(string publisher, int pageNumber)
        {
            throw new BussinessRuleException("NotImplementedYaFlutter");
        }
    }
}
