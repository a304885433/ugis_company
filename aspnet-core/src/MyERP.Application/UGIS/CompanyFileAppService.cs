using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using MyERP.UGIS.Dto;
using Abp.Linq.Extensions;
using Castle.Core.Internal;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using Abp.AutoMapper;

namespace MyERP.UGIS
{
    /// <summary>
    /// 企业图片服务
    /// </summary>
    public class CompanyFileAppService : MyAsyncCrudAppService<CompanyFile, CompanyFileDto, int, 
        CompanyFileGetAllInput, CompanyFileUpdateInput>
    {
        public CompanyFileAppService(IRepository<CompanyFile, int> repository) : base(repository)
        {
        }

        protected override IQueryable<CompanyFile> CreateFilteredQuery(CompanyFileGetAllInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.StartTime.HasValue, t => t.CreationTime >= input.StartTime)
                .WhereIf(input.EndTime.HasValue, t => t.CreationTime <= input.EndTime)
                .WhereIf(!input.Remark.IsNullOrEmpty(), t => t.Remark != null && t.Remark.Contains(input.Remark));
        }

        public override async Task<CompanyFileDto> Create(CompanyFileUpdateInput input)
        {
            var files = JsonConvert.DeserializeObject<List<CompanyFile>>(input.Files);
            CompanyFile fileEntity = null;
            foreach (var file in files)
            {
                file.Remark = input.Remark;
                fileEntity = await Repository.InsertAsync(file);
            }

            return fileEntity.MapTo<CompanyFileDto>();
        }

    }
}
