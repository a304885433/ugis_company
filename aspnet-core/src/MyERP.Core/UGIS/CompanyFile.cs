using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace MyERP.UGIS
{
    /// <summary>
    /// 通用文件存储
    /// </summary>
   public class CompanyFile : AuditedEntity
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 图片标识
        /// </summary>
        public string UId { get; set; }
    }
}
