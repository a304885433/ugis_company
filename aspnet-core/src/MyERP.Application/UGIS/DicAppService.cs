using Abp.Application.Services;
using Abp.Domain.Repositories;
using MyERP.UGIS.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 字典类型应用服务
    /// </summary>
    public class DicAppService : AsyncCrudAppService<Dic, DicDto>
    {
        public DicAppService(IRepository<Dic, int> repository) : base(repository)
        {
            LocalizationSourceName = MyERPConsts.LocalizationSourceName;
        }
    }
}
