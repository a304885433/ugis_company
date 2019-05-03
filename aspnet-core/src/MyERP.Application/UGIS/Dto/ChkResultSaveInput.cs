using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    [AutoMapTo(typeof(ChkResult))]
    public class ChkResultSaveInput
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 点位Id
        /// </summary>
        public int ChkPointId { get; set; }

        /// <summary>
        /// 记录员
        /// </summary>
        public string ChkUserName { get; set; }

        /// <summary>
        /// 检查日期
        /// </summary>
        public DateTime ChkDate { get; set; }

        /// <summary>
        /// 检查批次
        /// </summary>
        public string ChkBatch { get; set; }


        /// <summary>
        /// 排查因子数据
        /// </summary>
        public List<ChkResultSavePoluTypeInput> PoluTypeList { get; set; }

    }
}
