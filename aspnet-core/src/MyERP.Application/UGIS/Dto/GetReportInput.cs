using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    /// <summary>
    /// 报表查询输入参数
    /// </summary>
    public class GetReportInput
    {
        /// <summary>
        /// 点位Id
        /// </summary>
        public int? ChkPointId { get; set; }

        /// <summary>
        /// 企业Id
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 因子Id
        /// </summary>
        public int? PoluTypeId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

    }
}
