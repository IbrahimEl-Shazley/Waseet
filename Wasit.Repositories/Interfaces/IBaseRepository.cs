using System.Linq.Expressions;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Models.DTO;

namespace Wasit.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        // query
        IQueryable<T> GetQuery<T>(Expression<Func<T, bool>> predicate, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : Entity;

        // filter
        Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate = null, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : Entity;

        // paging
        Task<PageDTO<T>> GetListWithPagingAsync<T>(int skip, int take, Expression<Func<T, bool>> predicate = null, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : Entity;

        // from SQL
        Task<List<T>> FromSQLAsync<T>(string sp) where T : Entity;

        // first or default
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : Entity;
        Task<T> LastOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : Entity;

        // any
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate = null) where T : Entity;

        // count
        Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate = null) where T : Entity;

        // add
        Task AddAsync<T>(T entity) where T : Entity;
        Task AddRangeAsync<T>(List<T> entities) where T : Entity;

        // update
        void Update<T>(T entity) where T : Entity;
        void UpdateRange<T>(List<T> entities) where T : Entity;

        // remove
        void Remove<T>(T entity) where T : class;
        Task RemoveAsync<T>(long id) where T : Entity;
        Task RemoveAsync<T>(Expression<Func<T, bool>> predicate) where T : Entity;
        void RemoveRange<T>(List<T> entity) where T : Entity;

        // soft remove
        Task SoftRemoveAsync<T>(long id) where T : Entity;
        void SoftRemove<T>(T entity) where T : Entity;
        void SoftRemoveRange<T>(List<T> entities) where T : Entity;

        // reflection
        Task<List<T>> GetLookupForReflectionAsync<T>() where T : LookupEntity;
        Task<List<T>> GetEnumForReflectionAsync<T>() where T : EnumEntity;
        
        #region Transactions
        public void RollBack();
        public void RollBackAsync();
        public void Commit();
        public void CommitAsync();
        public void BeginTrnsaction();
        public void BeginTrnsactionAsync();
        #endregion


    }

    public interface IBaseRepository<T> : IBaseRepository where T : Entity
    {
        // query
        IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate, bool withTracking = true, params Expression<Func<T, object>>[] includes);

        // filter
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, bool withTracking = true, params Expression<Func<T, object>>[] includes);

        // paging
        Task<PageDTO<T>> GetListWithPagingAsync(int skip, int take, Expression<Func<T, bool>> predicate = null, bool withTracking = true, params Expression<Func<T, object>>[] includes);

        // from SQL
        Task<List<T>> FromSQLAsync(string sp);

        // first or default
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool withTracking = true, params Expression<Func<T, object>>[] includes);

        // any
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);

        // count
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);

        // add
        Task AddAsync(T entity);

        Task AddRangeAsync(List<T> entities);

        // update
        void Update(T entity);
        void UpdateRange(List<T> entities);

        // remove
        void Remove(T entity);
        Task RemoveAsync(long id);
        Task RemoveAsync(Expression<Func<T, bool>> predicate);
        void RemoveRange(List<T> entity);

        // soft remove
        Task SoftRemoveAsync(long id);
        void SoftRemove(T entity);
        void SoftRemoveRange(List<T> entities);

    }
}
