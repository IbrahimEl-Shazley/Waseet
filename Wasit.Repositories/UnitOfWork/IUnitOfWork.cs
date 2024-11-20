using Microsoft.EntityFrameworkCore.Storage;
using Wasit.Repositories.Interfaces;

namespace Wasit.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        TRepo Repository<TRepo>() where TRepo : IBaseRepository;
        bool SaveChange();
        Task<bool> SaveChangeAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task RollBackAsync();
        Task CommitAsync();

    }
}
