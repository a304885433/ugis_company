using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    /// <summary>
    /// 企业药品信息
    /// </summary>
    [AutoMapTo(typeof(CompanyMedcineType))]
    public class CompanyMedcineTypeDto : CompanyMedcineType, IEntityDto
    {
    }
}
