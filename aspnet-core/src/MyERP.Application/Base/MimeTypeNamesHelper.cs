using Castle.Core.Internal;
using MyERP.Net.MimeTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Base
{
  public  class MimeTypeNamesHelper
    {
        /// <summary>
        /// 获取文件的类型
        /// </summary>
        /// <param name="ext"></param>
        public static string GetMimeType(string ext)
        {
            if (ext.IsNullOrEmpty())
            {
                return string.Empty;
            }
            var t = ext[0] == '.' ? ext.Substring(1) : ext;

            if (t == "jpg" || t == "png" || t == "bmp")
            {
                return MimeTypeNames.ImagePng;
            }

            return MimeTypeNames.ApplicationOctetStream;
        }
    }
}
