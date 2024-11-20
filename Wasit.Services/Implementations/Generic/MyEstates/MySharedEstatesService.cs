using AutoMapper;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Services.Implementation;

namespace Wasit.Services.Implementations.Generic.MyEstates
{
    public class MySharedEstatesService : BaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;

        public MySharedEstatesService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _baseRepository = uow.Repository<IBaseRepository>();
            _unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
        }

    }
}
