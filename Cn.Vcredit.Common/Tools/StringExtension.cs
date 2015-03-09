using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.Common.Tools
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:对string的扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 将字符串转为DateTime?
        /// </summary>
        /// <param name="s"></param>
        /// <returns>如果可以被成功转换则返回DateTime，否则返回null</returns>
        public static DateTime? ToDateTime(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            DateTime v;
            if (!DateTime.TryParse(s, out v)) return null;

            return v;
        }

        /// <summary>
        /// 将字符串转为DateTime，如无法转换则返回默认值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue">无法转换时的默认值</param>
        /// <returns>如果可以被成功转换则返回DateTime，否则返回null</returns>
        public static DateTime ToDateTime(this string s, DateTime defaultValue)
        {
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;

            DateTime v;
            if (!DateTime.TryParse(s, out v)) return defaultValue;

            return v;
        }

        /// <summary>
        /// 将字符串转为decimal?
        /// </summary>
        /// <param name="s"></param>
        /// <returns>如果可以被成功转换则返回decimal，否则返回null</returns>
        public static decimal? ToDecimal(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            decimal v;
            if (!decimal.TryParse(s, out v)) return null;

            return v;
        }

        /// <summary>
        /// 将字符串转为decimal，如无法转换则返回默认值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue">无法转换时的默认值</param>
        /// <returns>如果可以被成功转换则返回decimal，否则返回null</returns>
        public static decimal ToDecimal(this string s, decimal defaultValue)
        {
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;

            decimal v;
            if (!decimal.TryParse(s, out v)) return defaultValue;

            return v;
        }

        /// <summary>
        /// 将字符串转为long?
        /// </summary>
        /// <param name="s"></param>
        /// <returns>如果可以被成功转换则返回long，否则返回null</returns>
        public static long? ToLong(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            long v;
            if (!long.TryParse(s, out v)) return null;

            return v;
        }

        /// <summary>
        /// 将字符串转为long，如无法转换则返回默认值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue">无法转换时的默认值</param>
        /// <returns>如果可以被成功转换则返回long，否则返回null</returns>
        public static long ToLong(this string s, long defaultValue)
        {
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;

            long v;
            if (!long.TryParse(s, out v)) return defaultValue;

            return v;
        }

        /// <summary>
        /// 将字符串转为int?
        /// </summary>
        /// <param name="s"></param>
        /// <returns>如果可以被成功转换则返回int，否则返回null</returns>
        public static int? ToInt(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            int v;
            if (!int.TryParse(s, out v)) return null;

            return v;
        }

        /// <summary>
        /// 将字符串转为int，如无法转换则返回默认值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue">无法转换时的默认值</param>
        /// <returns>如果可以被成功转换则返回int，否则返回null</returns>
        public static int ToInt(this string s, int defaultValue)
        {
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;

            int v;
            if (!int.TryParse(s, out v)) return defaultValue;

            return v;
        }

        /// <summary>
        /// 将字符串转为short?
        /// </summary>
        /// <param name="s"></param>
        /// <returns>如果可以被成功转换则返回short，否则返回null</returns>
        public static short? ToShort(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            short v;
            if (!short.TryParse(s, out v)) return null;

            return v;
        }

        /// <summary>
        /// 将字符串转为short，如无法转换则返回默认值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue">无法转换时的默认值</param>
        /// <returns>如果可以被成功转换则返回short，否则返回null</returns>
        public static short ToShort(this string s, short defaultValue)
        {
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;

            short v;
            if (!short.TryParse(s, out v)) return defaultValue;

            return v;
        }

        /// <summary>
        /// 将字符串转为byte?
        /// </summary>
        /// <param name="s"></param>
        /// <returns>如果可以被成功转换则返回byte，否则返回null</returns>
        public static byte? ToByte(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            byte v;
            if (!byte.TryParse(s, out v)) return null;

            return v;
        }

        /// <summary>
        /// 将字符串转为Byte，如无法转换则返回默认值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue">无法转换时的默认值</param>
        /// <returns>如果可以被成功转换则返回Byte，否则返回null</returns>
        public static byte ToByte(this string s, byte defaultValue)
        {
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;

            byte v;
            if (!byte.TryParse(s, out v)) return defaultValue;

            return v;
        }

        /// <summary>
        /// 将字符串转为bool?
        /// </summary>
        /// <param name="s"></param>
        /// <returns>如果可以被成功转换则返回bool，否则返回null</returns>
        public static bool? ToBool(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            bool v;
            if (!bool.TryParse(s, out v)) return null;

            return v;
        }

        /// <summary>
        /// 将字符串转为bool，如无法转换则返回默认值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue">无法转换时的默认值</param>
        /// <returns>如果可以被成功转换则返回bool，否则返回null</returns>
        public static bool ToBool(this string s, bool defaultValue)
        {
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;

            bool v;
            if (!bool.TryParse(s, out v)) return defaultValue;

            return v;
        }

        /// <summary>
        /// 将字符串转为枚举并返回枚举的整型值
        /// </summary>
        /// <typeparam name="T">必须为enum类型</typeparam>
        /// <param name="s"></param>
        /// <returns>如果可以被成功转换则返回int，否则返回null</returns>
        public static int? ToEnumValue<T>(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            try
            {
                return (int)Enum.Parse(typeof(T), s);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        /// <summary>
        /// 将字符串转为枚举并返回枚举的整型值
        /// </summary>
        /// <typeparam name="T">必须为enum类型</typeparam>
        /// <param name="s"></param>
        /// <param name="defaultValue">无法转换时的默认值</param>
        /// <returns>如果可以被成功转换则返回int，否则返回null</returns>
        public static int ToEnumValue<T>(this string s, int defaultValue)
        {
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;
            try
            {
                return (int)Enum.Parse(typeof(T), s);
            }
            catch (ArgumentException)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将字符串转为枚举并返回枚举的整型值
        /// </summary>
        /// <typeparam name="T">必须为enum类型</typeparam>
        /// <param name="s"></param>
        /// <param name="ignoreCase">true 为忽略大小写；false 为考虑大小写。</param>
        /// <returns>如果可以被成功转换则返回int，否则返回null</returns>
        public static int? ToEnumValue<T>(this string s, bool ignoreCase)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            try
            {
                return (int)Enum.Parse(typeof(T), s, ignoreCase);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        /// <summary>
        /// 将字符串转为枚举并返回枚举的整型值
        /// </summary>
        /// <typeparam name="T">必须为enum类型</typeparam>
        /// <param name="s"></param>
        /// <param name="ignoreCase">true 为忽略大小写；false 为考虑大小写。</param>
        /// <param name="defaultValue">无法转换时的默认值</param>
        /// <returns>如果可以被成功转换则返回int，否则返回null</returns>
        public static int ToEnumValue<T>(this string s, bool ignoreCase, int defaultValue)
        {
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;
            try
            {
                return (int)Enum.Parse(typeof(T), s, ignoreCase);
            }
            catch (ArgumentException)
            {
                return defaultValue;
            }
        }
    }
}
