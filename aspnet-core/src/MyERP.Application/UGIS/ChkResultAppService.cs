using Abp.Application.Services;
using Abp.Domain.Repositories;
using MyERP.UGIS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;

namespace MyERP.UGIS
{
    public class ChkResultAppService : MyAsyncCrudAppService<ChkResult, ChkResultDto, int, GetAllChkResultInput>
    {

        IRepository<CompanyInfo, int> companyInfoRepository;

        public ChkResultAppService(IRepository<ChkResult, int> repository, IRepository<CompanyInfo, int> companyInfoRepository) : base(repository)
        {
            this.companyInfoRepository = companyInfoRepository;
        }

        protected override IQueryable<ChkResult> CreateFilteredQuery(GetAllChkResultInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.CompanyId.HasValue, t => t.CompanyId == input.CompanyId)
                .WhereIf(input.ChkPointId.HasValue, t => t.ChkPointId == input.ChkPointId)
                .WhereIf(input.PoluTypeId.HasValue, t => t.PoluTypeId == input.PoluTypeId);
        }

    }
}
