using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace MyERP.UGIS.Dto
{
    /// <summary>
    /// 公司
    /// </summary>
    [AutoMapTo(typeof(CompanyFile))]
    public class CompanyFileUpdateInput : EntityDto<int>
    {
        /// <summary>
        /// 备注字段
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 文件JSON
        /// </summary>
        public string Files { get; set; }
    }
}
