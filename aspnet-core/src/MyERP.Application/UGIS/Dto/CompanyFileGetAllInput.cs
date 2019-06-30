using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace MyERP.UGIS.Dto
{

    /// <summary>
    /// 公司文件查询输入参数
    /// </summary>
    [AutoMapTo(typeof(CompanyFile))]
    public class CompanyFileGetAllInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
