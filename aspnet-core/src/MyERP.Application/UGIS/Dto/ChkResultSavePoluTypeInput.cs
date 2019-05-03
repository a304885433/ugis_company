using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    /// <summary>
    /// 排查结果
    /// </summary>
    [AutoMapTo(typeof(ChkResult))]
    public class ChkResultSavePoluTypeInput
    {
        /// <summary>
        /// 排查因子Id
        /// </summary>
        public int PoluTypeId { get; set; }

        /// <summary>
        /// 浓度
        /// </summary>
        public decimal Concentration { get; set; }
    }
}
