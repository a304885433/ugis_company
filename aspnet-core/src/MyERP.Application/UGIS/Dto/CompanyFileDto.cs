using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace MyERP.UGIS.Dto
{
   [AutoMapTo(typeof(CompanyFile))]
    public class CompanyFileDto : CompanyFile, IEntityDto
    {
    }
}
