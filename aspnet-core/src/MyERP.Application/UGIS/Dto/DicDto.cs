using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    [AutoMapTo(typeof(Dic))]
    public class DicDto : Dic, IEntityDto
    {
    }
}
