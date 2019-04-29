using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    /// <summary>
    /// 获取排查结果输入参数
    /// </summary>
    public class GetAllChkResultInput : PagedResultRequestDto
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 排查点位Id
        /// </summary>
        public int? ChkPointId { get; set; }

        /// <summary>
        /// 因子Id
        /// </summary>
        public int? PoluTypeId { get; set; }

        /// <summary>
        /// 排查日期
        /// </summary>
        public DateTime? StartChkDate { get; set; }

        /// <summary>
        /// 排查日期
        /// </summary>
        public DateTime? EndChkDate { get; set; }

        /// <summary>
        /// 排查批次
        /// </summary>
        public string ChkBatch { get; set; }

        /// <summary>
        /// 浓度
        /// </summary>
        public decimal? StartConcentration { get; set; }

        /// <summary>
        /// 浓度
        /// </summary>
        public decimal? EndConcentration { get; set; }
    }
}
