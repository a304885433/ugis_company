using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 排放因子
    /// </summary>
    public class PoluType : AuditedEntity
    {
        /// <summary>
        /// 排放名称
        /// </summary>
        public string Name { get; set; }


    }
}
