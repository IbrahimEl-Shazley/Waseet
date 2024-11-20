using System.Linq.Expressions;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Models.DTO;

namespace Wasit.Services.Interfaces
{


    public interface IBaseService
    {

    }
    public interface IBaseService<TEntity, TListDto, TCreateDto, TUpdateDto> : IBaseService where TEntity : Entity
    {
        // query
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate, bool withTracking = true, params Expression<Func<TEntity, object>>[] includes);

        // filter
        Task<List<TListDto>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, bool withTracking = true, params Expression<Func<TEntity, object>>[] includes);

        // paging
        Task<PageDTO<TListDto>> GetListWithPagingAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null, bool withTracking = true, params Expression<Func<TEntity, object>>[] includes);

        // from SQL
        Task<List<TListDto>> FromSQLAsync(string sp);

        // first or default
        Task<TListDto> FindAsync(long id, bool withTracking = true);
        Task<TListDto> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, bool withTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<TListDto> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, bool withTracking = true, params Expression<Func<TEntity, object>>[] includes);

        // any
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);

        // count
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        // add
        Task<TEntity> AddAsync(TCreateDto dto);
        Task<List<TEntity>> AddRangeAsync(List<TCreateDto> dtos);

        // update
        TEntity Update(TUpdateDto dto);
        List<TEntity> UpdateRange(List<TUpdateDto> dtos);

        // remove
        void Remove(TCreateDto dto);
        Task RemoveAsync(long id);
        Task RemoveAsync(Expression<Func<TEntity, bool>> predicate);
        void RemoveRange(List<TCreateDto> dtos);

        // soft remove
        Task<TEntity> SoftRemoveAsync(long id);
        TEntity SoftRemove(TCreateDto dto);
        List<TEntity> SoftRemoveRange(List<TCreateDto> dtos);

        // GENERIC FAST ACTION
        Task<TListDto> AddAndCommitAsync(TCreateDto dto);
        Task<TListDto> UpdateAndCommitAsync(TUpdateDto dto);
        Task<bool> RemoveAndCommitAsync(long id);
        Task<bool> SoftRemoveAndCommitAsync(long id);
    }

}
