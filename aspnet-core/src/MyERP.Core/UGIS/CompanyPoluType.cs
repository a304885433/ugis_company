using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 企业因子排放信息
    /// </summary>
   public class CompanyPoluType : AuditedEntity
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 排放因子Id
        /// </summary>
       public int PoluTypeId { get; set; }

        /// <summary>
        /// 排放单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 排放标准名称
        /// </summary>
        public string EmissionStdType { get; set; }
    }
}
