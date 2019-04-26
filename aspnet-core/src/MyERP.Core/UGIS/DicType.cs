using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 字典类型
    /// </summary>
    public class DicType : AuditedEntity
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 字典类型编码
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 父级字典类型编码
        /// </summary>
        public string ParentTypeCode { get; set; }

        /// <summary>
        /// 排序Id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 配置Json信息
        /// </summary>
        public string ConfigJson { get; set; }
    }
}
