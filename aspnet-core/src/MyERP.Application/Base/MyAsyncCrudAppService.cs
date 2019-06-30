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
    /// <summary>
    /// Curd服务（简单模式） 指定实体和Dto，
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TEntityDto">实体Dto类型</typeparam>
    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto>
        : MyAsyncCrudAppService<TEntity, TEntityDto, int, IPagedAndSortedResultRequest>
        where TEntity : class, IEntity<int>
        where TEntityDto : IEntityDto<int>
    {
        protected MyAsyncCrudAppService(IRepository<TEntity, int> repository)
            : base(repository)
        {

        }
    }

    /// <summary>
    /// Curd服务（查询模式） 指定查询类型，实体新增修改保存默认使用实体Dto类型
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TEntityDto">实体Dto类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键参数类型</typeparam>
    /// <typeparam name="TGetAllInput">实体GetAll 输入参数类型</typeparam>
    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput>
        : MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TEntityDto, TEntityDto, TEntityDto>
        where TGetAllInput : IPagedAndSortedResultRequest
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected MyAsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    /// <summary>
    /// Curd服务（实体修改简单模式）  分别指定查询、修改类型(用于新增、修改、保存)
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TEntityDto">实体Dto类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键参数类型</typeparam>
    /// <typeparam name="TGetAllInput">实体GetAll 输入参数类型</typeparam>
    /// <typeparam name="TSaveInput">实体Save 输入参数类型</typeparam>
    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TSaveInput>
        : MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TSaveInput, TSaveInput, TSaveInput>
        where TGetAllInput : IPagedAndSortedResultRequest
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TSaveInput : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected MyAsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    /// <summary>
    /// Curd服务（实体修改高级模式） 分别指定查询、新增、修改类型(保存类型同修改)
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TEntityDto">实体Dto类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键参数类型</typeparam>
    /// <typeparam name="TGetAllInput">实体GetAll 输入参数类型</typeparam>
    /// <typeparam name="TCreateInput">实体Create 输入参数类型</typeparam>
    /// <typeparam name="TUpdateInput">实体Update 输入参数类型</typeparam>
    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>
        : MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TUpdateInput>
        where TGetAllInput : IPagedAndSortedResultRequest
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
       where TCreateInput : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected MyAsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    /// <summary>
    /// Curd服务实现（实体修改复杂模式） 分别指定查询、新增、修改、保存类型
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TEntityDto">实体Dto类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键参数类型</typeparam>
    /// <typeparam name="TGetAllInput">实体GetAll 输入参数类型</typeparam>
    /// <typeparam name="TCreateInput">实体Create 输入参数类型</typeparam>
    /// <typeparam name="TUpdateInput">实体Update 输入参数类型</typeparam>
    /// <typeparam name="TSaveInput">实体Save 输入参数类型</typeparam>
    public abstract class MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TSaveInput>
    : MyAsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, EntityDto<TPrimaryKey>, EntityDto<TPrimaryKey>, TSaveInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetAllInput : IPagedAndSortedResultRequest
        where TSaveInput : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected MyAsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {

        }
    }

    /// <summary>
    /// Curd服务实现 全类型自定义
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TEntityDto">实体Dto类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键参数类型</typeparam>
    /// <typeparam name="TGetAllInput">实体GetAll 输入参数类型</typeparam>
    /// <typeparam name="TCreateInput">实体Create 输入参数类型</typeparam>
    /// <typeparam name="TUpdateInput">实体Update 输入参数类型</typeparam>
    /// <typeparam name="TGetInput">实体Get 输入参数类型</typeparam>
    /// <typeparam name="TDeleteInput">实体Delete 输入参数类型</typeparam>
    /// <typeparam name="TSaveInput">实体Save 输入参数类型</typeparam>
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

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> Get(TGetInput input)
        {
            CheckGetPermission();

            var entity = await GetEntityByIdAsync(input.Id);
            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 分页获取实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取查询出来的全部实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 统计实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<int> Count(TGetAllInput input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            return totalCount;
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> Create(TCreateInput input)
        {
            CheckCreatePermission();

            var entity = MapToEntity(input);

            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> Update(TUpdateInput input)
        {
            CheckUpdatePermission();

            var entity = await GetEntityByIdAsync(input.Id);

            MapToEntity(input, entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 保存实体，新增或修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 保存一组实体，新增或修改
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntityDto>> SaveChanges(List<TSaveInput> inputs)
        {
            var list = new List<TEntityDto>();
            foreach (var input in inputs)
            {
                var entity = await Save(input);
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual Task Delete(TDeleteInput input)
        {
            CheckDeletePermission();

            return Repository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual Task<TEntity> GetEntityByIdAsync(TPrimaryKey id)
        {
            return Repository.GetAsync(id);
        }


    }
}
