using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    /// <summary>
    /// 企业信息保存输入参数
    /// </summary>
    public class CompanyInfoSaveDto
    {
        /// <summary>
        /// 企业信息
        /// </summary>
        public CompanyInfoDto CompanyInfo { get; set; }

        /// <summary>
        /// 企业药品信息
        /// </summary>
        public List<CompanyPoluTypeDto> CompanyPoluTypeList { get; set; }

        /// <summary>
        /// 企业排放信息
        /// </summary>
        public List<CompanyMedcineTypeDto> CompanyMedcineTypeList { get; set; }
    }
}
