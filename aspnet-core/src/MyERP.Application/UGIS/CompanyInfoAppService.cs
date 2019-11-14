using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using MyERP.UGIS.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MyERP.Base;
using Abp.Linq.Extensions;
using Abp.Application.Services.Dto;
using MyERP.Common.Extend;

namespace MyERP.UGIS
{
    /// <summary>
    /// 企业信息应用服务
    /// </summary>
    public class CompanyInfoAppService : MyAsyncCrudAppService<CompanyInfo,
        CompanyInfoDto, int, CompanyInfoGetAllDto>
    {
        IRepository<CompanyPoluType, int> _companyPoluTypeRepository;
        IRepository<CompanyMedcineType, int> _companyMedcineTypeRepository;
        IRepository<CompanyContaminants, int> _companyContaminantsRepository;
        IRepository<Dic, int> _dicRepository;

        public CompanyInfoAppService(IRepository<CompanyInfo, int> repository,
            IRepository<CompanyPoluType, int> companyPoluTypeRepository,
            IRepository<CompanyMedcineType, int> companyMedcineTypeRepository,
            IRepository<CompanyContaminants, int> companyContaminantsRepository,
             IRepository<Dic, int> dicRepository) : base(repository)
        {
            LocalizationSourceName = MyERPConsts.LocalizationSourceName;
            this._companyPoluTypeRepository = companyPoluTypeRepository;
            this._companyMedcineTypeRepository = companyMedcineTypeRepository;
            this._companyContaminantsRepository = companyContaminantsRepository;
            this._dicRepository = dicRepository;
        }

        protected override IQueryable<CompanyInfo> CreateFilteredQuery(CompanyInfoGetAllDto input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(!string.IsNullOrEmpty(input.Address), t => t.Address.Contains(input.Address))
                .WhereIf(!string.IsNullOrEmpty(input.Contact), t => t.Contact.Contains(input.Contact))
                .WhereIf(!string.IsNullOrEmpty(input.Tel), t => t.Tel.Contains(input.Tel))
                .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name));
        }


        /// <summary>
        /// 保存企业信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<int> SaveForEdit(CompanyInfoSaveInput input)
        {
            var companyInfo = input.CompanyInfo.Id != 0 ? await GetEntityByIdAsync(input.CompanyInfo.Id)
                : input.CompanyInfo.MapTo<CompanyInfo>();

            var id = companyInfo.Id;
            if (id != 0)
            {
                // 更新实体
                var entity = await GetEntityByIdAsync(id);
                MapToEntity(input.CompanyInfo, entity);
            }
            else
            {
                // 保存子表信息
                id = await Repository.InsertAndGetIdAsync(companyInfo);
            }

            // 更新子实体外键
            if (input.CompanyMedcineTypeList != null)
            {
                input.CompanyMedcineTypeList.ForEach(t =>
                {
                    t.CompanyId = id;
                });
                var idList = input.CompanyMedcineTypeList.Where(t => t.Id != 0).Select(t => t.Id);
                // 删除不存在的实体
                await _companyMedcineTypeRepository.DeleteAsync(t => t.CompanyId == id && !idList.Contains(t.Id));
                // 保存变更的实体
                var changed = input.CompanyMedcineTypeList.MapTo<List<CompanyMedcineType>>();
                await _companyMedcineTypeRepository.InsertOrUpdateAsync(changed);
            }

            if (input.CompanyPoluTypeList != null)
            {
                input.CompanyPoluTypeList.ForEach(t =>
                {
                    t.CompanyId = id;
                });
                var idList = input.CompanyPoluTypeList.Where(t => t.Id != 0).Select(t => t.Id);
                // 删除不存在的实体
                await _companyPoluTypeRepository.DeleteAsync(t => t.CompanyId == id && !idList.Contains(t.Id));
                var changed = input.CompanyPoluTypeList.MapTo<List<CompanyPoluType>>();
                await _companyPoluTypeRepository.InsertOrUpdateAsync(changed);
            }

            if (input.CompanyContaminantsList != null)
            {
                input.CompanyContaminantsList.ForEach(t =>
                {
                    t.CompanyId = id;
                });
                var idList = input.CompanyContaminantsList.Where(t => t.Id != 0).Select(t => t.Id);
                // 删除不存在的实体
                await _companyContaminantsRepository.DeleteAsync(t => t.CompanyId == id && !idList.Contains(t.Id));
                var changed = input.CompanyContaminantsList.MapTo<List<CompanyContaminants>>();
                await _companyContaminantsRepository.InsertOrUpdateAsync(changed);
            }

            return id;
        }

        /// <summary>
        /// 获取企业信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<CompanyInfoSaveDto> GetForEdit(int id)
        {
            var companyInfo = await GetEntityByIdAsync(id);

            var poluTypeList = await _companyPoluTypeRepository.GetAllListAsync(t => t.CompanyId == id);

            var medTypeList = await _companyMedcineTypeRepository.GetAllListAsync(t => t.CompanyId == id);

            var companyContaminantsList = await _companyContaminantsRepository.GetAllListAsync(t => t.CompanyId == id);

            return new CompanyInfoSaveDto()
            {
                CompanyInfo = companyInfo.MapTo<CompanyInfoDto>(),
                CompanyMedcineTypeList = medTypeList.MapTo<List<CompanyMedcineTypeDto>>(),
                CompanyPoluTypeList = poluTypeList.MapTo<List<CompanyPoluTypeDto>>(),
                CompanyContaminantsList = companyContaminantsList.MapTo<List<CompanyContaminantsDto>>(),
            };

        }

        /// <summary>
        /// 读取企业因子
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public virtual List<DicDto> GetPoluType(NullableIdDto dto)
        {
            // 获取公司信息
            var query = from company in Repository.GetAll()
                        join poluType in _companyPoluTypeRepository.GetAll() on company.Id equals poluType.CompanyId
                        join dic in _dicRepository.GetAll() on poluType.PoluTypeId equals dic.Id
                        where company.Id == dto.Id
                        select dic;

            var list = query.OrderBy(t => t.OrderId).MapTo<List<DicDto>>();

            return list;
        }

        /// <summary>
        /// 删除企业数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task Delete(EntityDto<int> input)
        {
            await Repository.DeleteAsync(input.Id);
            await _companyPoluTypeRepository.DeleteAsync(t=> t.CompanyId == input.Id);
            await _companyMedcineTypeRepository.DeleteAsync(t=> t.CompanyId == input.Id);
            await _companyContaminantsRepository.DeleteAsync(t=> t.CompanyId == input.Id);
        }

    }
}
