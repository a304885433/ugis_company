using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyERP.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class MyERPRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<MyERPDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected MyERPRepositoryBase(IDbContextProvider<MyERPDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Add your common methods for all repositories
        //public virtual async Task<TEntity> Save(TEntity entity)
        //{
        //    if (entity.Id.Equals(default(TPrimaryKey)))
        //    {
        //        return await InsertAsync(entity);
        //    }
        //    else
        //    {
        //        return await InsertOrUpdateAsync(entity);
        //    }
        //}

        //public virtual async Task<List<TEntity>> SaveChanges(List<TEntity> entityList)
        //{
        //    var ret = new List<TEntity>();
        //    foreach (var entity in entityList)
        //    {
        //        var entityAfter = await Save(entity);
        //        ret.Add(entityAfter);
        //    }
        //    return ret;
        //}
    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="MyERPRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class MyERPRepositoryBase<TEntity> : MyERPRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        protected MyERPRepositoryBase(IDbContextProvider<MyERPDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Do not add any method here, add to the class above (since this inherits it)!!!
    }
}
