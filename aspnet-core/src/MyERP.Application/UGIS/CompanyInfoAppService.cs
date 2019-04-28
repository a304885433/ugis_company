using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using MyERP.UGIS.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MyERP.UGIS
{
    /// <summary>
    /// 企业信息应用服务
    /// </summary>
    public class CompanyInfoAppService : AsyncCrudAppService<CompanyInfo,
        CompanyInfoDto, int, CompanyInfoGetAllDto>
    {
        IRepository<CompanyPoluType, int> companyPoluTypeRepository;
        IRepository<CompanyMedcineType, int> companyMedcineTypeRepository;

        public CompanyInfoAppService(IRepository<CompanyInfo, int> repository,
            IRepository<CompanyPoluType, int> companyPoluTypeRepository,
            IRepository<CompanyMedcineType, int> companyMedcineTypeRepository) : base(repository)
        {
            LocalizationSourceName = MyERPConsts.LocalizationSourceName;
            this.companyPoluTypeRepository = companyPoluTypeRepository;
            this.companyMedcineTypeRepository = companyMedcineTypeRepository;
        }


        /// <summary>
        /// 保存企业信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<int> SaveForEdit(CompanyInfoSaveInput input)
        {
            var companyInfo = input.CompanyInfo.Id != 0 ? await GetEntityByIdAsync(input.CompanyInfo.Id)
                : input.CompanyInfo;

            var id = companyInfo.Id;
            if (id != 0)
            {
                // 更新实体
                var entity = input.CompanyInfo.MapTo<CompanyInfo>();
                await Repository.UpdateAsync(input.CompanyInfo);
            }
            else
            {
                // 保存子表信息
                id = await Repository.InsertAndGetIdAsync(input.CompanyInfo);
            }

            // 保存子实体
            await companyMedcineTypeRepository.DeleteAsync(t => t.CompanyId == id);
            input.CompanyMedcineTypeList.ForEach(async t =>
            {
                var entity = t.MapTo<CompanyMedcineType>();
                await companyMedcineTypeRepository.InsertAsync(entity);
            });

            await companyPoluTypeRepository.DeleteAsync(t => t.CompanyId == id);
            input.CompanyPoluTypeList.ForEach(async t =>
            {
                var entity = t.MapTo<CompanyPoluType>();
                await companyPoluTypeRepository.InsertAsync(entity);
            });

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

            var poluTypeList = await companyPoluTypeRepository.GetAllListAsync(t => t.CompanyId == id);

            var medTypeList = await companyMedcineTypeRepository.GetAllListAsync(t => t.CompanyId == id);

            return new CompanyInfoSaveDto()
            {
                CompanyInfo = companyInfo.MapTo<CompanyInfoDto>(),
                CompanyMedcineTypeList = medTypeList.MapTo<List<CompanyMedcineTypeDto>>(),
                CompanyPoluTypeList = poluTypeList.MapTo<List<CompanyPoluTypeDto>>(),
            };


        }

    }
}
