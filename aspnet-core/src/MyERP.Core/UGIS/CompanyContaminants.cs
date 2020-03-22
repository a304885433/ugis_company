using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 企业污染物
    /// </summary>
   public class CompanyContaminants : AuditedEntity
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 污染物Id
        /// </summary>
        public int ContaminantsId { get; set; }

        /// <summary>
        /// 转移总量
        /// </summary>
        public decimal TransferTotal { get; set; }
    }
}
