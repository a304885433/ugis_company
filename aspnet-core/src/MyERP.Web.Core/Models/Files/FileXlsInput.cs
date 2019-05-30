using System;
using System.Collections.Generic;
using System.Text;
using MyERP.UGIS.Dto;

namespace MyERP.Models.Files
{
   public class FileXlsInput
    {
        /// <summary>
        /// 报表列
        /// </summary>
        public List<ReportColDto> ColList { get; set; }

        /// <summary>
        /// 报表数据
        /// </summary>
        public List<Dictionary<string, object>> Data { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }
    }
}
