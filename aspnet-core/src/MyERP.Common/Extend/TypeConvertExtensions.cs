using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace MyERP.Common.Extend
{
    /// <summary>
    /// 提供类型扩展方法
    /// </summary>
    public static class TypeConvertExtensions
    {
        /// <summary>
        /// 字符串转decimal
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static decimal AsDecimal(this string str, decimal defaultVal = 0)
        {
            decimal d;
            return decimal.TryParse(str, out d) ? d : defaultVal;
        }

        /// <summary>
        /// 字符串转int
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static int AsInt(this string str, int defaultVal = 0)
        {
            int d;
            return int.TryParse(str, out d) ? d : defaultVal;
        }

        /// <summary>
        /// 将字符串转换为时间
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static DateTime AsDateTime(this string str)
        {
            DateTime time;
            return DateTime.TryParse(str, out time) ? time : DateTime.MinValue;
        }

        /// <summary>
        /// 将字符串转换为GUID
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static Guid AsGUID(this string str)
        {
            Guid valGuid;
            return Guid.TryParse(str, out valGuid) ? valGuid : Guid.Empty;
        }

        /// <summary>
        /// 将字符串转换为时间
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static DateTime? AsDateTime2(this string str)
        {
            DateTime dt;
            if (DateTime.TryParse(str, out dt))
            {
                return dt;
            }

            // 格式：43478.3333333333
            double d;
            if (double.TryParse(str, out d))
            {
                return DateTime.FromOADate(d);
            }

            // 格式：/OADate(43477)/
            var rg = new Regex(@"(?<=\()[^\(\)]+(?=\))", RegexOptions.Multiline | RegexOptions.Singleline);
            if (double.TryParse(rg.Match(str).Value, out d))
            {
                return DateTime.FromOADate(d);
            }
            return null;
        }

        /// <summary>
        /// 将字符串转换成int数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<int> AsIntList(this string str)
        {
            return str.AsIntList(",");
        }

        /// <summary>
        /// 将字符串转换成int数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sep"></param>
        /// <returns></returns>
        public static List<int> AsIntList(this string str, string sep)
        {
            return str.Split(sep).Select(t => t.AsInt()).ToList();
        }

        /// <summary>
        /// 将字符串转换成Guid数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<Guid> AsGuidList(this string str)
        {
            return str.AsGuidList(",");
        }

        /// <summary>
        /// 将字符串转换成Guid数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sep"></param>
        /// <returns></returns>
        public static List<Guid> AsGuidList(this string str, string sep)
        {
            return str.Split(sep).Select(t => t.AsGUID()).ToList();
        }
    }
}
