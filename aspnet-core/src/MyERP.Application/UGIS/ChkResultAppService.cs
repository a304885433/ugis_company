using Abp.Application.Services;
using Abp.Domain.Repositories;
using MyERP.UGIS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Abp.AutoMapper;
using MyERP.Base;
using AutoMapper;
using Abp.Application.Services.Dto;
using Castle.Core.Internal;

namespace MyERP.UGIS
{
    public class ChkResultAppService : MyAsyncCrudAppService<ChkResult, ChkResultDto, int, GetAllChkResultInput>
    {

        IRepository<CompanyInfo, int> companyInfoRepository;
        IRepository<Dic, int> dicRepository;

        public ChkResultAppService(IRepository<ChkResult, int> repository,
            IRepository<CompanyInfo, int> companyInfoRepository,
             IRepository<Dic, int> dicRepository) : base(repository)
        {
            this.companyInfoRepository = companyInfoRepository;
            this.dicRepository = dicRepository;
        }

        protected override IQueryable<ChkResult> CreateFilteredQuery(GetAllChkResultInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.CompanyId.HasValue, t => t.CompanyId == input.CompanyId)
                .WhereIf(input.ChkPointId.HasValue, t => t.ChkPointId == input.ChkPointId)
                .WhereIf(input.PoluTypeId.HasValue, t => t.PoluTypeId == input.PoluTypeId);
        }

        public async Task<PagedResultDto<ChkResultDto>> GetCustomAll(GetAllChkResultInput input)
        {
            var chkResultMapper = new MapperConfiguration(cfg =>
            {
                cfg.ValidateInlineMaps = false;
                cfg.CreateMap<ChkResult, ChkResultDto>().ForMember(t => t.ChkPointName, opt => opt.Ignore());
                cfg.CreateMap<ChkResult, ChkResultDto>().ForMember(t => t.CompanyName, opt => opt.Ignore());
                cfg.CreateMap<ChkResult, ChkResultDto>().ForMember(t => t.PoluTypeName, opt => opt.Ignore());
            }).CreateMapper();

            var companyMapper = new MapperConfiguration(cfg =>
            {
                cfg.ValidateInlineMaps = false;
                cfg.CreateMap<CompanyInfo, ChkResultDto>().ForMember(t => t.CompanyName,
                    opt => opt.MapFrom(src => src.Name));
                //cfg.CreateMap<CompanyInfo, ChkResultDto>().ForMember(t => t.Id,
                // opt => opt.Ignore());
            }).CreateMapper();

            var chkPointNameMapper = new MapperConfiguration(cfg =>
            {
                cfg.ValidateInlineMaps = false;
                cfg.CreateMap<Dic, ChkResultDto>().ForMember(t => t.ChkPointName,
                    opt => opt.MapFrom(src => src.Name));
                //cfg.CreateMap<Dic, ChkResultDto>().ForMember(t => t.Id,
                //   opt => opt.Ignore());
            }).CreateMapper();

            var poluTypeNameMapper = new MapperConfiguration(cfg =>
            {
                cfg.ValidateInlineMaps = false;
                cfg.CreateMap<Dic, ChkResultDto>().ForMember(t => t.PoluTypeName,
                    opt => opt.MapFrom(src => src.Name));
                //cfg.CreateMap<Dic, ChkResultDto>().ForMember(t => t.Id,
                // opt => opt.Ignore());
            }).CreateMapper();

            var query = from result in Repository.GetAll()
                        join company in companyInfoRepository.GetAll() on result.CompanyId equals company.Id
                        join poluDic in dicRepository.GetAll() on result.PoluTypeId equals poluDic.Id
                        join chkPoint in dicRepository.GetAll() on result.ChkPointId equals chkPoint.Id
                        select result.MapTo<ChkResultDto>(chkResultMapper)
                                .MapFrom(poluDic, poluTypeNameMapper)
                                .MapFrom(company, companyMapper)
                                .MapFrom(chkPoint, chkPointNameMapper);

            // 过滤
            query = query.WhereIf(input.CompanyId.HasValue, t => t.CompanyId == input.CompanyId)
               .WhereIf(input.ChkPointId.HasValue, t => t.ChkPointId == input.ChkPointId)
               .WhereIf(input.PoluTypeId.HasValue, t => t.PoluTypeId == input.PoluTypeId)
               .WhereIf(input.StartChkDate.HasValue, t => t.ChkDate >= input.StartChkDate)
               .WhereIf(input.EndChkDate.HasValue, t => t.ChkDate <= input.EndChkDate)
               .WhereIf(input.StartConcentration.HasValue, t => t.Concentration >= input.StartConcentration)
               .WhereIf(input.EndConcentration.HasValue, t => t.Concentration <= input.EndConcentration)
               .WhereIf(!input.ChkBatch.IsNullOrEmpty(), t => t.ChkBatch != null && t.ChkBatch.Contains(input.ChkBatch))
               ;

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = query.OrderByDescending(t => t.Id).PageBy(input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            return new PagedResultDto<ChkResultDto>(totalCount, query.ToList());

        }



        public async Task SaveForEdit(ChkResultSaveInput input)
        {
            // 构建多条数据
            var list = input.PoluTypeList.Select(t =>
            {
                var entity = t.MapTo<ChkResult>();
                input.MapTo(entity);
                return entity;
            }).ToList();

            // 保存数据
            await Repository.InsertOrUpdateAsync(list);

        }
    }
}
