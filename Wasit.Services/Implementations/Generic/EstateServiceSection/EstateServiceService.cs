using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.EstateServiceSection;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Service.Interfaces.General;
using Wasit.Services.DTOs.Schema.EstateService;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;
using Wasit.Services.Implementation;
using Wasit.Services.Interfaces.Generic.EstateServiceSection;
using Wasit.Services.ViewModels.Profits.AllPackages;

namespace Wasit.Services.Implementations.Generic.EstateServiceSection
{
    public class EstateServiceService : BaseService, IEstateServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IBaseRepository _baseRepository;


        public EstateServiceService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            _currentUserService = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
            _baseRepository = uow.Repository<IBaseRepository>();

        }

        public async Task<IEnumerable<EstateServicePackagesDto>> ServicesPackages()
        {
            var result = await _baseRepository
           .GetListAsync<P4ShowEstateService>(x => x.IsActive && ! x.IsDeleted , false);

            return _mapper.Map<List<EstateServicePackagesDto>>(result);
           
        }
    }
}
