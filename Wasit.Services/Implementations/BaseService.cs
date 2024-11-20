using AutoMapper;
using System.Linq.Expressions;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Helpers;
using Wasit.Core.Helpers.Localization;
using Wasit.Core.Models.DTO;
using Wasit.Repositories.Interfaces;
using Wasit.Repositories.UnitOfWork;
using Wasit.Service.Interfaces.General;
using Wasit.Services.Interfaces;

namespace Wasit.Services.Implementation
{
    public class BaseService : IBaseService
    {
        private readonly ICurrentUserService _currentUserService;
        public BaseService(IServiceProvider serviceProvider)
        {
            _currentUserService = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
        }

        protected string Localize(string key)
        {
            return LocalizerHelper.Localize(key, _currentUserService.Language, MyConstants.GeneralLocalizationPath);
        }

    }


    public class BaseService<TEntity, TListDto, TCreateDto, TUpdateDto> : BaseService, IBaseService<TEntity, TListDto, TCreateDto, TUpdateDto> where TEntity : Entity
    {
        private readonly IUnitOfWork _uow;
        private readonly IBaseRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public BaseService(IUnitOfWork uow, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _uow = uow;
            _repo = uow.Repository<IBaseRepository>();
            _mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _currentUserService = (ICurrentUserService)serviceProvider.GetService(typeof(ICurrentUserService));
        }

        public BaseService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        // query
        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate, bool withTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            return _repo.GetQuery(predicate, withTracking, includes);
        }

        // filter
        public async Task<List<TListDto>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, bool withTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            return _mapper.Map<List<TListDto>>(await _repo.GetListAsync(predicate, withTracking, includes));
        }

        // paging
        public async Task<PageDTO<TListDto>> GetListWithPagingAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate = null, bool withTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var skip = (pageNumber - 1) * pageSize;
            var res = await _repo.GetListWithPagingAsync(skip, pageSize, predicate, withTracking, includes);

            return new PageDTO<TListDto>
            {
                Count = res.Count,
                TotalCount = res.TotalCount,
                Data = _mapper.Map<List<TListDto>>(res.Data),
            };
        }

        // from SQL
        public async Task<List<TListDto>> FromSQLAsync(string sp)
        {
            return _mapper.Map<List<TListDto>>(await _repo.FromSQLAsync<TEntity>(sp));
        }

        // first or default
        public async Task<TListDto> FindAsync(long id, bool withTracking = true)
        {
            return _mapper.Map<TListDto>(await _repo.FirstOrDefaultAsync<TEntity>(x => x.Id == id, withTracking));
        }
        public async Task<TListDto> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, bool withTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            return _mapper.Map<TListDto>(await _repo.FirstOrDefaultAsync(predicate, withTracking, includes));
        }
        public async Task<TListDto> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, bool withTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            return _mapper.Map<TListDto>(await _repo.LastOrDefaultAsync(predicate, withTracking, includes));
        }

        // any
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _repo.AnyAsync(predicate);
        }

        // count
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _repo.CountAsync(predicate);
        }

        // add
        public async Task<TEntity> AddAsync(TCreateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            //PredefinedCoulmnsHelper.Add(entity, _statlessSessionService.UserId);
            await _repo.AddAsync(entity);
            return entity;
        }
        public async Task<List<TEntity>> AddRangeAsync(List<TCreateDto> dtos)
        {
            var entities = _mapper.Map<List<TCreateDto>, List<TEntity>>(dtos);
            foreach (var entity in entities)
            {
                //PredefinedCoulmnsHelper.Add(entity, _statlessSessionService.UserId);
            }
            await _repo.AddRangeAsync(entities);
            return entities;
        }

        // update
        public TEntity Update(TUpdateDto dto)
        {
            var entity = _mapper.Map<TUpdateDto, TEntity>(dto);
            //PredefinedCoulmnsHelper.Update(entity, _statlessSessionService.UserId);
            _repo.Update(entity);
            return entity;
        }
        public List<TEntity> UpdateRange(List<TUpdateDto> dtos)
        {
            var entities = _mapper.Map<List<TEntity>>(dtos);
            //foreach (var entity in entities)
            //{
            //    PredefinedCoulmnsHelper.Update(entity, _statlessSessionService.UserId);
            //}
            _repo.UpdateRange(entities);
            return entities;
        }

        // remove
        public void Remove(TCreateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repo.Remove(entity);
        }
        public async Task RemoveAsync(long id)
        {
            await _repo.RemoveAsync<TEntity>(id);
        }
        public async Task RemoveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await _repo.RemoveAsync<TEntity>(predicate);
        }
        public void RemoveRange(List<TCreateDto> dtos)
        {
            var entities = _mapper.Map<List<TCreateDto>, List<TEntity>>(dtos);
            _repo.RemoveRange(entities);
        }

        // soft remove
        public async Task<TEntity> SoftRemoveAsync(long id)
        {
            var entity = await _repo.FirstOrDefaultAsync<TEntity>(x => x.Id == id);
            //PredefinedCoulmnsHelper.SoftRemove(entity, _statlessSessionService.UserId);
            _repo.SoftRemove(entity);
            return entity;
        }
        public TEntity SoftRemove(TCreateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            //PredefinedCoulmnsHelper.SoftRemove(entity, _statlessSessionService.UserId);
            _repo.SoftRemove(entity);
            return entity;
        }
        public List<TEntity> SoftRemoveRange(List<TCreateDto> dtos)
        {
            var entities = _mapper.Map<List<TEntity>>(dtos);
            foreach (var entity in entities)
            {
                //PredefinedCoulmnsHelper.SoftRemove(entity, _statlessSessionService.UserId);
            }
            _repo.SoftRemoveRange(entities);
            return entities;
        }

        // GENERIC FAST ACTION
        public virtual async Task<TListDto> AddAndCommitAsync(TCreateDto dto)
        {
            var entity = _mapper.Map<TCreateDto, TEntity>(dto);
            //PredefinedCoulmnsHelper.Add(entity, _statlessSessionService.UserId);
            await _repo.AddAsync(entity);
            await _uow.SaveChangeAsync();
            return _mapper.Map<TListDto>(entity);
        }
        public virtual async Task<TListDto> UpdateAndCommitAsync(TUpdateDto dto)
        {
            var entity = _mapper.Map<TUpdateDto, TEntity>(dto);
            _repo.Update(entity);
            await _uow.SaveChangeAsync();
            return _mapper.Map<TListDto>(entity);
        }
        public virtual async Task<bool> RemoveAndCommitAsync(long id)
        {
            await _repo.RemoveAsync<TEntity>(id);
            return await _uow.SaveChangeAsync();
        }
        public virtual async Task<bool> SoftRemoveAndCommitAsync(long id)
        {
            var entity = await _repo.FirstOrDefaultAsync<TEntity>(x => x.Id == id);
            //PredefinedCoulmnsHelper.SoftRemove(entity, _statlessSessionService.UserId);
            _repo.SoftRemove(entity);
            return await _uow.SaveChangeAsync();
        }
    }


}
