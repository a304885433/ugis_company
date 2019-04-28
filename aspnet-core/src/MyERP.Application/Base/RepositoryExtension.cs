using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Base
{
    public static class RepositoryExtension
    {
        public static async Task<List<TEntity>> InsertOrUpdateAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, List<TEntity> entityList)
        where TEntity : class, IEntity<TPrimaryKey>
        {
            var ret = new List<TEntity>();
            foreach (var entity in entityList)
            {
                var entityAfter = await repository.InsertOrUpdateAsync(entity);
                ret.Add(entityAfter);
            }
            return ret;
        }
    }
}
