using Microsoft.EntityFrameworkCore.Storage;
using Wasit.Context;
using Wasit.Repositories.Interfaces;

namespace Wasit.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        private Dictionary<Type, object> repositories;

        public UnitOfWork(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _context = dbContext;
            //_userRepository = userRepository;
        }

        ~UnitOfWork()
        {
            _context.Dispose();
        }


        public TRepository Repository<TRepository>() where TRepository : IBaseRepository
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TRepository);

            if (!repositories.ContainsKey(type))
            {
                repositories[type] = _serviceProvider.GetService(typeof(TRepository));
            }

            return (TRepository)repositories[type];
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangeAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task RollBackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

    }

}
