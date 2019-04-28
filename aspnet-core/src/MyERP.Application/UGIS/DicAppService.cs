using Abp.Application.Services;
using Abp.Domain.Repositories;
using MyERP.UGIS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;

namespace MyERP.UGIS
{
    /// <summary>
    /// 字典类型应用服务
    /// </summary>
    public class DicAppService : MyAsyncCrudAppService<Dic, DicDto, int, GetAllDicInput>
    {
        public DicAppService(IRepository<Dic, int> repository) : base(repository)
        {
            LocalizationSourceName = MyERPConsts.LocalizationSourceName;
        }

        protected override IQueryable<Dic> CreateFilteredQuery(GetAllDicInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(!string.IsNullOrEmpty(input.TypeCode), t => t.TypeCode == input.TypeCode)
                .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name));

        }
    }
}
