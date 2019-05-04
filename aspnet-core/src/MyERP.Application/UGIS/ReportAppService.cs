using Abp.Domain.Repositories;
using MyERP.UGIS.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq;
using MyERP.Base;
using MyERP.Common.Extend;

namespace MyERP.UGIS
{
    public class ReportAppService : MyERPAppServiceBase
    {
        IRepository<CompanyInfo, int> companyInfoRepository;
        IRepository<CompanyPoluType, int> companyPoluTypeRepository;
        IRepository<Dic, int> dicRepository;
        IRepository<ChkResult, int> chkResultRepository;

        public ReportAppService(IRepository<ChkResult, int> repository,
            IRepository<CompanyInfo, int> companyInfoRepository,
        IRepository<CompanyPoluType, int> companyPoluTypeRepository,
             IRepository<Dic, int> dicRepository)
        {
            this.companyInfoRepository = companyInfoRepository;
            this.dicRepository = dicRepository;
            this.chkResultRepository = repository;
            this.companyPoluTypeRepository = companyPoluTypeRepository;
        }

        public async Task<ReportDataDto> GetReport1(GetReportInput input)
        {
            var company = await companyInfoRepository.FirstOrDefaultAsync(t => t.Id == input.CompanyId);
            var companyChkPositionId = company.ChkPointIdList.AsIntList();
            var companyPoluTypeList = await companyPoluTypeRepository.GetAllListAsync(t => t.CompanyId == input.CompanyId);
            var companyPoluTypeIdList = companyPoluTypeList.Select(t => t.PoluTypeId).ToList();
            var chkResultList = await chkResultRepository.GetAllListAsync(t => t.CompanyId == input.CompanyId
             && t.ChkDate >= input.StartTime && (input.EndTime == null || t.ChkDate <= input.EndTime));
            var dicData = await dicRepository.GetAllListAsync(t => t.TypeCode == "yinzixinxi" || t.TypeCode == "dianweixinxi");

            var poluTypeList = dicData.Where(t => t.TypeCode == "yinzixinxi" && companyPoluTypeIdList.Contains(t.Id))
                .OrderBy(t => t.OrderId).ToList();
            var chkPointList = dicData.Where(t => t.TypeCode == "dianweixinxi" && companyChkPositionId.Contains(t.Id)
             ).OrderBy(t => t.OrderId).ToList();

            var report = new ReportDataDto();

            // 列
            report.ColList = new List<ReportColDto>() {
                 new ReportColDto(){
                      ColId ="chkPointName",
                       Name = "排查点"
                 },
                 new ReportColDto(){
                     ColId = "chkDate",
                      Name  ="排查日期"
                 },
            };
            report.ColList.AddRange(poluTypeList.Select(t => new ReportColDto()
            {
                Name = t.Name,
                ColId = "col" + t.Id
            }));

            // 数据
            report.Data = chkPointList.SelectMany(chkPoint =>
             {
                 var chkPointData = chkResultList.Where(t => t.ChkPointId == chkPoint.Id).ToList();

                 return chkPointData.GroupBy(t => t.ChkDate.Date).OrderBy(t => t.Key)
                        .Select(t =>
                        {
                            var dic = new Dictionary<string, object>();
                            dic["chkPointName"] = chkPoint.Name;
                            dic["chkDate"] = t.Key.ToString("yyyy-MM-dd");
                            foreach (var poluType in poluTypeList)
                            {
                                var currData = t.Where(c => c.PoluTypeId == poluType.Id).ToList();
                                var val = currData.Count > 0 ? currData.Average(c => c.Concentration) : 0;
                                dic[$"col{poluType.Id}"] = val;
                            }
                            return dic;
                        }).ToList();

             }).ToList();

            return report;
        }

        public async Task<ReportDataDto> GetReport2(GetReportInput input)
        {
            var chkResultList = await chkResultRepository.GetAllListAsync(t => t.CompanyId == input.CompanyId
            && t.PoluTypeId == input.PoluTypeId
            && t.ChkDate >= input.StartTime && (input.EndTime == null || t.ChkDate <= input.EndTime));

            var report = new ReportDataDto();
            // 列
            report.ColList = new List<ReportColDto>() {
                 new ReportColDto(){
                      ColId ="time",
                       Name = "时间"
                 },
                 new ReportColDto(){
                     ColId = "val",
                      Name  ="浓度"
                 },
            };
            report.Data = chkResultList.GroupBy(t => t.ChkDate.Date).OrderBy(t => t.Key).Select(t =>
              {
                  var dic = new Dictionary<string, object>();
                  dic["time"] = t.Key.ToString("yyyy-MM-dd");
                  dic["val"] = t.Average(c => c.Concentration);
                  return dic;
              }).ToList();
            return report;
        }

        public async Task<ReportDataDto> GetReport3(GetReportInput input)
        {
            var query = from company in companyInfoRepository.GetAll()
                        join result in chkResultRepository.GetAll() on company.Id equals result.CompanyId
                        where result.PoluTypeId == input.PoluTypeId && result.ChkDate >= input.StartTime
                        && (input.EndTime == null || result.ChkDate <= input.EndTime)
                        orderby company.CreationTime ascending
                        select new
                        {
                            company.Name,
                            company.Id,
                            val = result.Concentration
                        };


            var report = new ReportDataDto();
            // 列
            report.ColList = new List<ReportColDto>() {
                 new ReportColDto(){
                      ColId ="name",
                       Name = "企业"
                 },
                 new ReportColDto(){
                     ColId = "val",
                      Name  ="浓度(平均值)"
                 },
            };
            report.Data = query.ToList().GroupBy(t => t.Id).Select(t =>
            {
                var dic = new Dictionary<string, object>();
                dic["name"] = t.First().Name;
                dic["val"] = t.Average(c => c.val);
                return dic;
            }).ToList();

            return report;

        }

        public async Task<ReportDataDto> GetReport4(GetReportInput input)
        {
            var company = await companyInfoRepository.FirstOrDefaultAsync(t => t.Id == input.CompanyId);
            var companyPoluTypeList = await companyPoluTypeRepository.GetAllListAsync(t => t.CompanyId == input.CompanyId);
            var companyPoluTypeIdList = companyPoluTypeList.Select(t => t.PoluTypeId).ToList();
            var chkResultList = await chkResultRepository.GetAllListAsync(t => t.CompanyId == input.CompanyId
             && t.ChkDate >= input.StartTime && (input.EndTime == null || t.ChkDate <= input.EndTime));
            var poluTypeList = await dicRepository.GetAllListAsync(t => t.TypeCode == "yinzixinxi");
            var poluTypeOrderList = poluTypeList.OrderBy(t => t.OrderId).ToList();

            var report = new ReportDataDto();
            report.ColList = new List<ReportColDto>() {
                 new ReportColDto(){
                      ColId ="name",
                       Name = "因子"
                 },
            };
            report.Data = chkResultList.GroupBy(t => t.ChkDate.Date)
                .SelectMany(t =>
                {

                    var colId = $"col{t.Key.ToString("yyyyMMdd")}";

                    // 增加列
                    report.ColList.Add(new ReportColDto()
                    {
                        ColId = colId,
                        Name = t.Key.ToString("yyyy-MM-dd")
                    });

                    // 返回数据
                    return poluTypeOrderList.Select(poluType =>
                    {
                        var dic = new Dictionary<string, object>();
                        dic["name"] = poluType.Name;
                        var currData = t.Where(c => c.PoluTypeId == poluType.Id);
                        dic[colId] = currData.Any() ? currData.Average(c => c.Concentration) : 0;
                        return dic;
                    }).ToList();
                }).ToList();

            return report;
        }

        public async Task<ReportDataDto> GetReport5(GetReportInput input)
        {
            var companyList = await companyInfoRepository.GetAllListAsync();
            var companyOrderList = companyList.OrderByDescending(t => t.CreationTime);

            var poluTypeList = await dicRepository.GetAllListAsync(t => t.TypeCode == "yinzixinxi");
            var poluTypeOrderList = poluTypeList.OrderBy(t => t.OrderId).ToList();

            var chkResultList = await chkResultRepository.GetAllListAsync(t => t.ChkPointId == input.ChkPointId
           && t.ChkDate >= input.StartTime && (input.EndTime == null || t.ChkDate <= input.EndTime));

            var report = new ReportDataDto();
            report.ColList = new List<ReportColDto>() {
                 new ReportColDto(){
                      ColId ="name",
                       Name = "因子"
                 },
            };
            report.ColList.AddRange(poluTypeOrderList.Select(t => new ReportColDto()
            {
                ColId = $"col{t.Id}",
                Name = t.Name
            }));
            report.Data = companyOrderList.Select(t =>
            {
                var dic = new Dictionary<string, object>();
                dic["name"] = t.Name;

                poluTypeOrderList.ForEach(poluType =>
                {
                    var currData = chkResultList.Where(c => c.CompanyId == t.Id && c.PoluTypeId == poluType.Id).ToList();
                    dic[$"col{poluType.Id}"] = currData.Count == 0 ? 0 : currData.Average(c => c.Concentration);
                });

                return dic;

            }).ToList();


            return report;
        }
    }
}
