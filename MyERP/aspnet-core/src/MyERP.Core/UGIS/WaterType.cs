using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 药品信息
    /// </summary>
    public class WaterType : FullAuditedEntity
    {
        public string MedName { get; set; }
    }
}
