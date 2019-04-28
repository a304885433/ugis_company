using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Linq;

namespace Abp.Application.Services
{
    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto>
        : MyAsyncCrudAppService<TEntity, TEntityDto, int>
        where TEntity : class, IEntity<int>
        where TEntityDto : IEntityDto<int>
    {
        protected MyAsyncCrudAppService(IRepository<TEntity, int> repository)
            : base(repository)
        {

        }
    }

    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey>
        : MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, PagedAndSortedResultRequestDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected MyAsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput>
        : MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TEntityDto, TEntityDto, TEntityDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected MyAsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TSaveInput>
        : MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TCreateInput, TSaveInput>
        where TGetAllInput : IPagedAndSortedResultRequest
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
       where TCreateInput : IEntityDto<TPrimaryKey>
        where TSaveInput : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected MyAsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TSaveInput>
        : MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, EntityDto<TPrimaryKey>, TSaveInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TSaveInput : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected MyAsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TSaveInput>
    : MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, EntityDto<TPrimaryKey>, TSaveInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetInput : IEntityDto<TPrimaryKey>
        where TSaveInput : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected MyAsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput, TSaveInput>
       : CrudAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>,
        IAsyncCrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
           where TEntity : class, IEntity<TPrimaryKey>
           where TEntityDto : IEntityDto<TPrimaryKey>
           where TUpdateInput : IEntityDto<TPrimaryKey>
           where TGetInput : IEntityDto<TPrimaryKey>
           where TDeleteInput : IEntityDto<TPrimaryKey>
           where TSaveInput : IEntityDto<TPrimaryKey>
            where TPrimaryKey : struct
    {
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }

        protected MyAsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {
            AsyncQueryableExecuter = NullAsyncQueryableExecuter.Instance;
        }

        public virtual async Task<TEntityDto> Get(TGetInput input)
        {
            CheckGetPermission();

            var entity = await GetEntityByIdAsync(input.Id);
            return MapToEntityDto(entity);
        }

        public virtual async Task<PagedResultDto<TEntityDto>> GetAll(TGetAllInput input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            try
            {

                var entities = await AsyncQueryableExecuter.ToListAsync(query);
                return new PagedResultDto<TEntityDto>(
              totalCount,
              entities.Select(MapToEntityDto).ToList()
          );
            }
            catch (System.Exception ex)
            {

                throw;
            }

        }

        public virtual async Task<List<TEntityDto>> GetAllItem(TGetAllInput input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input);


            query = ApplySorting(query, input);

            try
            {

                var entities = await AsyncQueryableExecuter.ToListAsync(query);
                var list = entities.Select(MapToEntityDto).ToList();
                return list;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        public virtual async Task<int> GetTotal(TGetAllInput input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            return totalCount;
        }

        public virtual async Task<TEntityDto> Create(TCreateInput input)
        {
            CheckCreatePermission();

            var entity = MapToEntity(input);

            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public virtual async Task<TEntityDto> Update(TUpdateInput input)
        {
            CheckUpdatePermission();

            var entity = await GetEntityByIdAsync(input.Id);

            MapToEntity(input, entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public virtual async Task<TEntityDto> Save(TSaveInput input)
        {
            if (input.Id.Equals(default(TPrimaryKey)))
            {
                CheckCreatePermission();

                var entity = input.MapTo<TEntity>();

                await Repository.InsertAsync(entity);
                await CurrentUnitOfWork.SaveChangesAsync();

                return MapToEntityDto(entity);
            }
            else
            {
                CheckUpdatePermission();

                var entity = await GetEntityByIdAsync(input.Id);

                ObjectMapper.Map(input, entity);

                await CurrentUnitOfWork.SaveChangesAsync();

                return MapToEntityDto(entity);
            }

        }

        public virtual Task Delete(TDeleteInput input)
        {
            CheckDeletePermission();

            return Repository.DeleteAsync(input.Id);
        }

        protected virtual Task<TEntity> GetEntityByIdAsync(TPrimaryKey id)
        {
            return Repository.GetAsync(id);
        }


    }
}
