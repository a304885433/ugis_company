using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    [AutoMapTo(typeof(CompanyInfo))]
    public class CompanyInfoGetAllDto : PagedAndSortedResultRequestDto
    {
        public string Tel { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}
