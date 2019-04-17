using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 药品信息111
    /// </summary>
    public class MedType : AuditedEntity
    {
        /// <summary>
        /// 药品名称11
        /// </summary>
        public string MedName { get; set; }
    }
}
