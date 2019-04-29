using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    /// <summary>
    /// 获取所有字典类型
    /// </summary>
    public class GetAllDicTypeInput : ISortedResultRequest
    {
        public string Sorting { get; set; } = "id ASC";
    }
}
