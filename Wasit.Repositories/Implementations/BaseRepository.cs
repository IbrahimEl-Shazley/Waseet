using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Wasit.Context;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Helpers;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;

namespace Wasit.Repositories.Implementations
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected IQueryable<T> InsializeQuery<T>(params Expression<Func<T, object>>[] includes) where T : Entity
        {
            var query = _context.Set<T>().Where(x => !x.IsDeleted).AsQueryable();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    var splitted = include.ToString().Replace("(", "").Replace(")", "").Split("Select");
                    if (splitted.Length > 1)
                    {
                        string pathInclude = string.Empty;
                        var i = 0;
                        foreach (var prop in splitted)
                        {
                            var subSplitted = prop.Split('.');
                            for (int j = 1; j < subSplitted.Length; j++)
                            {
                                pathInclude += $"{subSplitted[j]}";
                                if (j < subSplitted.Length - 1)
                                    pathInclude += ".";
                            }
                            if (i < splitted.Length - 1)
                                pathInclude += ".";
                            i++;
                        }
                        pathInclude = pathInclude.Replace("..", ".");
                        query = query.Include(pathInclude);
                    }
                    else
                    {
                        query = query.Include(include);
                    }
                }
            }
            return query;
        }


        protected Expression<Func<T, bool>> PreparePredicate<T>(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? predicate : (x => true);
        }

        // query
        public IQueryable<T> GetQuery<T>(Expression<Func<T, bool>> predicate = null, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : Entity
        {
            if (withTracking)
                return InsializeQuery<T>(includes).Where(PreparePredicate(predicate));
            else
                return InsializeQuery<T>(includes).Where(PreparePredicate(predicate)).AsNoTracking();
        }

        // filter
        public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate = null, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : Entity
        {
            if (withTracking)
                return await InsializeQuery<T>(includes).Where(PreparePredicate(predicate)).ToListAsync();
            else
                return await InsializeQuery<T>(includes).Where(PreparePredicate(predicate)).AsNoTracking().ToListAsync();
        }

        // paging
        public async Task<PageDTO<T>> GetListWithPagingAsync<T>(int skip, int take, Expression<Func<T, bool>> predicate = null, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : Entity
        {
            var query = InsializeQuery<T>(includes).Where(PreparePredicate(predicate)).OrderByDescending(x => x.Id);
            List<T> list;

            if (withTracking)
                list = await query.Skip(skip).Take(take).ToListAsync();
            else
                list = await query.Skip(skip).Take(take).AsNoTracking().ToListAsync();

            return new PageDTO<T>
            {
                CurrentPage = (skip + MyConstants.PageSize) / MyConstants.PageSize,
                Count = list.Count,
                TotalCount = query.Count(),
                Data = list,
            };
        }

        // from SQL
        public async Task<List<T>> FromSQLAsync<T>(string sp) where T : Entity
        {
            return await _context.Set<T>().FromSqlRaw(sp).ToListAsync();
        }

        // first or default
        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : Entity
        {
            if (withTracking)
                return await InsializeQuery<T>(includes).FirstOrDefaultAsync(PreparePredicate(predicate));
            else
                return await InsializeQuery<T>(includes).AsNoTracking().FirstOrDefaultAsync(PreparePredicate(predicate));
        }
        public async Task<T> LastOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, bool withTracking = true, params Expression<Func<T, object>>[] includes) where T : Entity
        {
            if (withTracking)
                return await InsializeQuery<T>(includes).OrderBy(x => x.Id).LastOrDefaultAsync(PreparePredicate(predicate));
            else
                return await InsializeQuery<T>(includes).AsNoTracking().OrderBy(x => x.Id).LastOrDefaultAsync(PreparePredicate(predicate));
        }

        // any
        public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate = null) where T : Entity
        {
            return await InsializeQuery<T>().AnyAsync(PreparePredicate(predicate));
        }

        // count
        public async Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate = null) where T : Entity
        {
            return await InsializeQuery<T>().CountAsync(PreparePredicate(predicate));
        }

        // add
        public async Task AddAsync<T>(T entity) where T : Entity
        {
            await _context.AddAsync(entity);
        }
        public async Task AddRangeAsync<T>(List<T> entities) where T : Entity
        {
            await _context.AddRangeAsync(entities);
        }

        // update
        public void Update<T>(T entity) where T : Entity
        {
            _context.Update<T>(entity);
        }
        public void UpdateRange<T>(List<T> entities) where T : Entity
        {
            _context.UpdateRange(entities);
        }

        // remove
        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task RemoveAsync<T>(long id) where T : Entity
        {
            var entity = await FirstOrDefaultAsync<T>(x => x.Id == id, false);
            _context.Remove(entity);
        }
        public async Task RemoveAsync<T>(Expression<Func<T, bool>> predicate) where T : Entity
        {
            var entity = await FirstOrDefaultAsync(predicate, false);
            _context.Remove(entity);
        }
        public void RemoveRange<T>(List<T> entities) where T : Entity
        {
            _context.RemoveRange(entities);
        }

        // soft remove
        public async Task SoftRemoveAsync<T>(long id) where T : Entity
        {
            var entity = await FirstOrDefaultAsync<T>(x => x.Id == id, false);
            if (entity != null)
            {
                _context.Entry<T>(entity).State = EntityState.Modified;
            }
        }
        public void SoftRemove<T>(T entity) where T : Entity
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
        }
        public void SoftRemoveRange<T>(List<T> entities) where T : Entity
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        // reflection
        public async Task<List<T>> GetLookupForReflectionAsync<T>() where T : LookupEntity
        {
            return await _context.Set<T>().AsQueryable().ToListAsync();
        }
        public async Task<List<T>> GetEnumForReflectionAsync<T>() where T : EnumEntity
        {
            return await _context.Set<T>().AsQueryable().ToListAsync();
        }
        public void BeginTrnsactionAsync()
        {
            _context.Database.BeginTransactionAsync();
        }
        public void BeginTrnsaction()
        {
            _context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _context.Database.CommitTransaction();
        }
        public void CommitAsync()
        {
            _context.Database.CommitTransactionAsync();
        }
        public void RollBack()
        {
            _context.Database.RollbackTransaction();
        }
        public void RollBackAsync()
        {
            _context.Database.RollbackTransactionAsync();
        }
    }

    public class BaseRepository<T> : BaseRepository, IBaseRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // query
        public IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate = null, bool withTracking = true, params Expression<Func<T, object>>[] includes)
        {
            if (withTracking)
                return InsializeQuery<T>().Where(PreparePredicate(predicate));
            else
                return InsializeQuery<T>().Where(PreparePredicate(predicate)).AsNoTracking();
        }

        // filter
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, bool withTracking = true, params Expression<Func<T, object>>[] includes)
        {
            if (withTracking)
                return await InsializeQuery(includes).Where(PreparePredicate(predicate)).ToListAsync();
            else
                return await InsializeQuery(includes).Where(PreparePredicate(predicate)).AsNoTracking().ToListAsync();
        }

        // paging
        public async Task<PageDTO<T>> GetListWithPagingAsync(int skip, int take, Expression<Func<T, bool>> predicate = null, bool withTracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = InsializeQuery(includes).Where(PreparePredicate(predicate));
            List<T> list;

            if (withTracking)
                list = await query.Skip(skip).Take(take).ToListAsync();
            else
                list = await query.Skip(skip).Take(take).AsNoTracking().ToListAsync();

            return new PageDTO<T>
            {
                CurrentPage = (skip + MyConstants.PageSize) / MyConstants.PageSize,
                Count = list.Count,
                TotalCount = query.Count(),
                Data = list,
            };
        }

        // from SQL
        public async Task<List<T>> FromSQLAsync(string sp)
        {
            _context.Advantages.AddRangeAsync();
            return await _context.Set<T>().FromSqlRaw(sp).ToListAsync();
        }

        // first or default
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool withTracking = true, params Expression<Func<T, object>>[] includes)
        {
            if (withTracking)
                return await InsializeQuery(includes).FirstOrDefaultAsync(PreparePredicate(predicate));
            else
                return await InsializeQuery(includes).AsNoTracking().FirstOrDefaultAsync(PreparePredicate(predicate));
        }

        // any
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await InsializeQuery<T>().AnyAsync(PreparePredicate(predicate));
        }

        // count
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await InsializeQuery<T>().CountAsync(PreparePredicate(predicate));
        }

        // add
        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }
        public async Task AddRangeAsync(List<T> entities)
        {
            await _context.AddRangeAsync(entities);
        }

        // update
        public void Update(T entity)
        {
            _context.Update(entity);
        }
        public void UpdateRange(List<T> entities)
        {
            _context.UpdateRange(entities);
        }

        // remove
        public void Remove(T entity)
        {
            _context.Remove(entity);
        }
        public async Task RemoveAsync(long id)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id, false);
            _context.Remove(entity);
        }
        public async Task RemoveAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await FirstOrDefaultAsync(predicate, false);
            _context.Remove(entity);
        }
        public void RemoveRange(List<T> entities)
        {
            _context.RemoveRange(entities);
        }

        // soft remove
        public async Task SoftRemoveAsync(long id)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id, false);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
        public void SoftRemove(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void SoftRemoveRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }


        

    }
}
