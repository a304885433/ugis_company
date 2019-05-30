using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Models
{
    public class FileUploadDto 
    {
        /// <summary>
        /// 大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件Id
        /// </summary>
        [JsonProperty("uid")]
        public string Id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Url { get; set; }
    }
}
