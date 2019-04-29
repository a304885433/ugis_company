using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    [AutoMapTo(typeof(ChkResult))]
   public class ChkResultDto : ChkResult, IEntityDto
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 因子名称
        /// </summary>
        public string PoluTypeName { get; set; }
        /// <summary>
        /// 排查点位名称
        /// </summary>
        public string ChkPointName { get; set; }

    }
}
