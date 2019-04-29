using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    /// <summary>
    /// 查询所有字典的输入参数
    /// </summary>
    public class GetAllDicInput : PagedResultRequestDto, ISortedResultRequest
    {
        /// <summary>
        /// 字典类型Code
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string Sorting { get; set; } = "OrderId ASC";

    }
}
