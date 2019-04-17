using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 排查结果
    /// </summary>
    public class ChkResult : AuditedEntity
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 点位Id
        /// </summary>
        public int ChkPointId { get; set; }

        /// <summary>
        /// 排放因子
        /// </summary>
        public int PoluTypeId { get; set; }

        ///// <summary>
        ///// 排查结果
        ///// </summary>
        //public string Result { get; set; }

        /// <summary>
        /// 排查日期
        /// </summary>
        public DateTime ChkDate { get; set; }

        /// <summary>
        /// 排查批次
        /// </summary>
        public string ChkBatch { get; set; }

        /// <summary>
        /// 浓度
        /// </summary>
        public decimal Concentration { get; set; }

    }
}
