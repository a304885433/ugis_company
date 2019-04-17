using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 试剂耗材
    /// </summary>
    public class Reagent : AuditedEntity
    {
        /// <summary>
        /// 试剂名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string Batch { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int Qualitity { get; set; }
        
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
    }
}
