using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.UGIS.Dto
{
    /// <summary>
    /// 报表数据返回值Dto
    /// </summary>
    public class ReportDataDto
    {
        /// <summary>
        /// 报表列
        /// </summary>
        public List<ReportColDto> ColList { get; set; }

        /// <summary>
        /// 报表数据
        /// </summary>
        public List<Dictionary<string, object>> Data { get; set; }
    }
}
