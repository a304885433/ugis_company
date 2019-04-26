using Abp.Application.Services;
using Abp.Domain.Repositories;
using MyERP.UGIS.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 企业信息应用服务
    /// </summary>
    public class CompanyInfoAppService : AsyncCrudAppService<CompanyInfo,
        CompanyInfoDto, int, CompanyInfoGetAllDto>
    {
        public CompanyInfoAppService(IRepository<CompanyInfo, int> repository) : base(repository)
        {
            LocalizationSourceName = MyERPConsts.LocalizationSourceName;
        }
    }
}
