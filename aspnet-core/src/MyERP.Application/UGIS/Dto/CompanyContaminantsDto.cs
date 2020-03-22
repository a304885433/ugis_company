using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace MyERP.UGIS.Dto
{
   /// <summary>
   /// 企业因子排放信息
   /// </summary>
   [AutoMapTo(typeof(CompanyContaminants))]
   public class CompanyContaminantsDto : CompanyContaminants, IEntityDto
   {

   }
}
