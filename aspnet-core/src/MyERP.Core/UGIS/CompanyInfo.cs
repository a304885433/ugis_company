using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyERP.UGIS
{
    /// <summary>
    /// 企业信息
    /// </summary>
    public class CompanyInfo : AuditedEntity
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 企业地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 排污许可证
        /// </summary>
        public string LicenseFile { get; set; }

        /// <summary>
        /// 废水类型Id
        /// </summary>
        public int WaterTypeID { get; set; }

        /// <summary>
        /// 废水月处理量（吨）
        /// </summary>
        public int WaterAmount { get; set; }

        /// <summary>
        /// 废水月处理量（批复）
        /// </summary>
        public decimal PlanWaterAmount { get; set; }

        /// <summary>
        /// 废水月处理量（实际）
        /// </summary>
        public decimal FactWaterAmount { get; set; }

        /// <summary>
        /// 废水设施处理能力
        /// </summary>
        public decimal WaterAmountBz { get; set; }

        /// <summary>
        /// 污水处理工艺流程图
        /// </summary>
        public string CraftFile { get; set; }

        /// <summary>
        /// 工艺流程说明（要说明污水分流情况）
        /// </summary>
        public string CraftDes { get; set; }

        /// <summary>
        /// 管道图片
        /// </summary>
        public string PipeFile { get; set; }

        /// <summary>
        /// 环境污染险保单复印件
        /// </summary>
        public string IssSheetFile { get; set; }

        /// <summary>
        /// 环评公司名称
        /// </summary>
        public string EnvCompany { get; set; }

        /// <summary>
        /// 环责险保额
        /// </summary>
        public decimal EnvInsuredAmount { get; set; }

        /// <summary>
        /// 环责险期限
        /// </summary>
        public string EnvDeadline { get; set; }

        /// <summary>
        /// 企业危险评级
        /// </summary>
        public int RiskBand { get; set; }

        /// <summary>
        /// 污水排放记录日期
        /// </summary>
        public DateTime DischargeDate { get; set; }

        /// <summary>
        /// 污水排放频次(每月)
        /// </summary>
        public int MonthResTimes { get; set; }

        /// <summary>
        /// 单次排放量(吨)
        /// </summary>
        public decimal MonthResAmount { get; set; }

        /// <summary>
        /// 月排放量(吨)
        /// </summary>
        public decimal MonthTotalAmount { get; set; }

        /// <summary>
        /// 排放方式Id
        /// </summary>
        public int EmissionTypeID { get; set; }

        /// <summary>
        /// 自流方式Id
        /// </summary>
        public int CollTypeID { get; set; }

        /// <summary>
        /// 排查点位信息
        /// </summary>
        public string ChkPointIdList { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [Column(TypeName = "decimal(18,10)")]

        public decimal X { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [Column(TypeName = "decimal(18,10)")]
        public decimal Y { get; set; }

        /// <summary>
        /// 原材料采购清单
        /// </summary>
        public string PurchaseFile { get; set; }
    }
}
