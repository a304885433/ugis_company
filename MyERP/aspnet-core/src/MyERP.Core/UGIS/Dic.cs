using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 字典表
    /// </summary>
    public class Dic : AuditedEntity
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字典类别
        /// </summary>
        public string Category { get; set; }

    }
}
