using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    /// <summary>
    /// 企业因子排放信息
    /// </summary>
    [AutoMapTo(typeof(CompanyPoluType))]
    public class CompanyPoluTypeDto : CompanyPoluType, IEntityDto
    {

    }
}
