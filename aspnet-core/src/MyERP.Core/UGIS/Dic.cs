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

        /// <summary>
        /// 数据
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 排序Id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 字典类型编码
        /// </summary>
        public string TypeCode { get; set; }

    }
}
