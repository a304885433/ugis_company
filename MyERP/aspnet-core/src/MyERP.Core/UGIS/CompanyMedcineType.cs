using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 企业药品信息
    /// </summary>
    public class CompanyMedcineType :  AuditedEntity
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 药品Id
        /// </summary>
        public int MedTypeId { get; set; }

       /// <summary>
       /// 月购置量
       /// </summary>
        public int MonthAmmount { get; set; }
    }
}
