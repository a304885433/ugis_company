using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 测量方法
    /// </summary>
    public class MeaureMethod : AuditedEntity
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public string BaseInfo { get; set; }

        /// <summary>
        /// 操作步骤
        /// </summary>
        public string Steps { get; set; }
    }
}
